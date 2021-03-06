// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;

using Stride.Core;

namespace Stride.Graphics
{
    /// <summary>
    ///   Represents a class that manages the allocation, usage tracking, and recycling of <see cref="GraphicsResource"/>s.
    /// </summary>
    /// <remarks>
    ///   The graphics resource allocator allows to allocate graphics resources. Also tracks usage reference and allows
    ///   to recycle unused resources based on a recycle policy.
    ///   <para/>
    ///   This class is threadsafe. Accessing a member will lock globally this instance.
    /// </remarks>
    public class GraphicsResourceAllocator : ComponentBase
    {
        // TODO: Check if we should introduce an enum for the kind of scope (per DrawCore, per Frame, etc)
        // TODO: Add statistics method (number of objects allocated, etc)

        private readonly object thisLock = new object();

        private readonly Dictionary<TextureDescription, List<GraphicsResourceLink>> textureCache = new Dictionary<TextureDescription, List<GraphicsResourceLink>>();
        private readonly Dictionary<BufferDescription, List<GraphicsResourceLink>> bufferCache = new Dictionary<BufferDescription, List<GraphicsResourceLink>>();
        private readonly Dictionary<QueryPoolDescription, List<GraphicsResourceLink>> queryPoolCache = new Dictionary<QueryPoolDescription, List<GraphicsResourceLink>>();

        private readonly Func<Texture, TextureDescription> getTextureDefinitionDelegate;
        private readonly Func<Buffer, BufferDescription> getBufferDescriptionDelegate;
        private readonly Func<QueryPool, QueryPoolDescription> getQueryPoolDescriptionDelegate;
        private readonly Func<TextureDescription, PixelFormat, Texture> createTextureDelegate;
        private readonly Func<BufferDescription, PixelFormat, Buffer> createBufferDelegate;
        private readonly Func<QueryPoolDescription, PixelFormat, QueryPool> createQueryPoolDelegate;


        /// <summary>
        ///   Initializes a new instance of the <see cref="GraphicsResourceAllocator" /> class.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public GraphicsResourceAllocator(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));

            getTextureDefinitionDelegate = GetTextureDefinition;
            getBufferDescriptionDelegate = GetBufferDescription;
            getQueryPoolDescriptionDelegate = GetQueryPoolDefinition;
            createTextureDelegate = CreateTexture;
            createBufferDelegate = CreateBuffer;
            createQueryPoolDelegate = CreateQueryPool;
            RecyclePolicy = DefaultRecyclePolicy;
        }

        /// <summary>
        ///   Gets or sets the graphics device.
        /// </summary>
        /// <value>The graphics device.</value>
        private GraphicsDevice GraphicsDevice { get; set; }

        /// <summary>
        ///   Gets the services registry.
        /// </summary>
        /// <value>The services registry.</value>
        public IServiceRegistry Services { get; private set; }

        /// <summary>
        ///   Gets or sets the default recycle policy.
        /// </summary>
        /// <value>The default recycle policy. Default is to always recycle no matter the state of the resources.</value>
        public GraphicsResourceRecyclePolicyDelegate RecyclePolicy { get; set; }

        /// <summary>
        ///   Recycles unused resources (those with <see cref="GraphicsResourceLink.ReferenceCount"/> == 0) with
        ///   the <see cref="RecyclePolicy"/>. By default there is no recycle policy installed.
        /// </summary>
        public void Recycle()
        {
            if (RecyclePolicy != null)
                Recycle(RecyclePolicy);
        }

        /// <summary>
        ///   Recycles unused resources with the specified recycle policy.
        /// </summary>
        /// <param name="recyclePolicy">The recycle policy.</param>
        /// <exception cref="ArgumentNullException"><paramref name="recyclePolicy"/> is a <c>null</c> reference.</exception>
        public void Recycle(GraphicsResourceRecyclePolicyDelegate recyclePolicy)
        {
            if (recyclePolicy is null)
                throw new ArgumentNullException(nameof(recyclePolicy));

            // Global lock to be threadsafe
            lock (thisLock)
            {
                Recycle(textureCache, recyclePolicy);
                Recycle(bufferCache, recyclePolicy);
            }
        }

        /// <summary>
        ///   Gets a texture for the specified description.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns>A texture</returns>
        public Texture GetTemporaryTexture(TextureDescription description)
        {
            // Global lock to be threadsafe
            lock (thisLock)
            {
                return GetTemporaryResource(
                    textureCache,
                    description,
                    createTextureDelegate,
                    getTextureDefinitionDelegate,
                    PixelFormat.None);
            }
        }

        /// <summary>
        ///   Gets a buffer for the specified description.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="viewFormat">The pixel format seen by the shader</param>
        /// <returns>A texture</returns>
        public Buffer GetTemporaryBuffer(BufferDescription description, PixelFormat viewFormat = PixelFormat.None)
        {
            // Global lock to be threadsafe
            lock (thisLock)
            {
                return GetTemporaryResource(
                    bufferCache,
                    description,
                    createBufferDelegate,
                    getBufferDescriptionDelegate,
                    viewFormat);
            }
        }

        public QueryPool GetQueryPool(QueryType queryType, int queryCount)
        {
            // Global lock to be threadsafe
            lock (thisLock)
            {
                return GetTemporaryResource(
                    queryPoolCache,
                    new QueryPoolDescription(queryType, queryCount),
                    createQueryPoolDelegate,
                    getQueryPoolDescriptionDelegate,
                    PixelFormat.None);
            }
        }

        /// <summary>
        ///   Increments the reference to a temporary resource.
        /// </summary>
        /// <param name="resource">The resource whose reference count is to be incremented.</param>
        public void AddReference(GraphicsResource resource)
        {
            // Global lock to be threadsafe
            lock (thisLock)
            {
                UpdateReference(resource, referenceDelta: 1);
            }
        }

        /// <summary>
        ///   Decrements the reference to a temporary resource.
        /// </summary>
        /// <param name="resource">The resource whose reference count is to be decremented.</param>
        public void ReleaseReference(GraphicsResourceBase resource)
        {
            // Global lock to be threadsafe
            lock (thisLock)
            {
                UpdateReference(resource, referenceDelta: -1);
            }
        }

        /// <summary>
        ///   Creates a texture.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="viewFormat">The pixel format seen by the shader.</param>
        /// <returns>A new texture.</returns>
        protected virtual Texture CreateTexture(TextureDescription description, PixelFormat viewFormat)
        {
            return Texture.New(GraphicsDevice, description);
        }

        /// <summary>
        ///   Creates a buffer.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="viewFormat">The shader view format on the buffer.</param>
        /// <returns>A new buffer.</returns>
        protected virtual Buffer CreateBuffer(BufferDescription description, PixelFormat viewFormat)
        {
            return Buffer.New(GraphicsDevice, description, viewFormat);
        }

        protected virtual QueryPool CreateQueryPool(QueryPoolDescription description, PixelFormat viewFormat)
        {
            return QueryPool.New(GraphicsDevice, description.QueryType, description.QueryCount);
        }

        protected override void Destroy()
        {
            lock (thisLock)
            {
                DisposeCache(textureCache, true);
                DisposeCache(bufferCache, true);
                DisposeCache(queryPoolCache, true);
            }

            base.Destroy();
        }

        private void DisposeCache<TKey>(Dictionary<TKey, List<GraphicsResourceLink>> cache, bool clearKeys)
        {
            foreach (var resourceList in cache.Values)
            {
                foreach (var resource in resourceList)
                {
                    resource.Resource.Dispose();
                }
                if (!clearKeys)
                {
                    resourceList.Clear();
                }
            }
            if (clearKeys)
            {
                cache.Clear();
            }
        }

        private BufferDescription GetBufferDescription(Buffer buffer)
        {
            return buffer.Description;
        }

        private TextureDescription GetTextureDefinition(Texture texture)
        {
            return texture.Description;
        }

        private QueryPoolDescription GetQueryPoolDefinition(QueryPool queryPool)
        {
            return new QueryPoolDescription(queryPool.QueryType, queryPool.QueryCount);
        }

        private TResource GetTemporaryResource<TResource, TKey>(Dictionary<TKey, List<GraphicsResourceLink>> cache, TKey description, Func<TKey, PixelFormat, TResource> creator, Func<TResource, TKey> getDefinition, PixelFormat viewFormat)
            where TResource : GraphicsResourceBase
            where TKey : struct
        {
            // For a specific description, get allocated textures
            if (!cache.TryGetValue(description, out List<GraphicsResourceLink> resourceLinks))
            {
                resourceLinks = new List<GraphicsResourceLink>();
                cache.Add(description, resourceLinks);
            }

            // Find a texture available
            foreach (var resourceLink in resourceLinks)
            {
                if (resourceLink.ReferenceCount == 0)
                {
                    UpdateCounter(resourceLink, deltaCount: 1);
                    return (TResource) resourceLink.Resource;
                }
            }

            // If no texture available, then create a new one
            var newResource = creator(description, viewFormat);
            newResource.Name = string.Format("{0}{1}-{2}", Name is null ? string.Empty : string.Format("{0}-", Name), newResource.Name is null ? newResource.GetType().Name : Name, resourceLinks.Count);

            // Description may be altered when creating a resource (based on HW limitations, etc) so we get the actual description
            var realDescription = getDefinition(newResource);

            // For a specific description, get allocated textures
            if (!cache.TryGetValue(realDescription, out resourceLinks))
            {
                resourceLinks = new List<GraphicsResourceLink>();
                cache.Add(description, resourceLinks);
            }

            // Add the texture to the allocated textures
            // Start RefCount == 1, because we don't want this texture to be available if a post-fx Processor is calling
            // several times this GetTemporaryTexture method
            var newResourceLink = new GraphicsResourceLink(newResource) { ReferenceCount = 1 };
            resourceLinks.Add(newResourceLink);

            return newResource;
        }

        /// <summary>
        ///   Recycles the specified cache.
        /// </summary>
        /// <typeparam name="TKey">The type of resource description.</typeparam>
        /// <param name="cache">The cache to recycle.</param>
        /// <param name="recyclePolicy">The recycle policy.</param>
        private void Recycle<TKey>(Dictionary<TKey, List<GraphicsResourceLink>> cache, GraphicsResourceRecyclePolicyDelegate recyclePolicy)
        {
            foreach (var resourceList in cache.Values)
            {
                for (int i = resourceList.Count - 1; i >= 0; i--)
                {
                    var resourceLink = resourceList[i];
                    if (resourceLink.ReferenceCount == 0)
                    {
                        if (recyclePolicy(resourceLink))
                        {
                            resourceLink.Resource.Dispose();
                            resourceList.RemoveAt(i);
                        }
                        // Reset the access count
                        resourceLink.AccessCountSinceLastRecycle = 0;
                    }
                }
            }
        }

        private void UpdateReference(GraphicsResourceBase resource, int referenceDelta)
        {
            if (resource is null)
                return;

            bool resourceFound = false;

            switch (resource)
            {
                case Texture texture:
                    resourceFound = UpdateReferenceCount(textureCache, texture, getTextureDefinitionDelegate, referenceDelta);
                    break;

                case Buffer buffer:
                    resourceFound = UpdateReferenceCount(bufferCache, buffer, getBufferDescriptionDelegate, referenceDelta);
                    break;

                case QueryPool queryPool:
                    resourceFound = UpdateReferenceCount(queryPoolCache, queryPool, getQueryPoolDescriptionDelegate, referenceDelta);
                    break;

                default:
                    throw new ArgumentException("Unsupported graphics resource. Only Textures, Buffers and QueryPools are supported.", nameof(resource));
            }

            if (!resourceFound)
                throw new ArgumentException("The resource was not allocated by this allocator.");
        }

        private bool UpdateReferenceCount<TKey, TResource>(Dictionary<TKey, List<GraphicsResourceLink>> cache, TResource resource, Func<TResource, TKey> getDefinition, int deltaCount)
            where TResource : GraphicsResourceBase
            where TKey : struct
        {
            if (resource is null)
                return false;

            if (cache.TryGetValue(getDefinition(resource), out List<GraphicsResourceLink> resourceLinks))
            {
                foreach (var resourceLink in resourceLinks)
                {
                    if (resourceLink.Resource == resource)
                    {
                        UpdateCounter(resourceLink, deltaCount);
                        return true;
                    }
                }
            }
            return false;
        }

        private void UpdateCounter(GraphicsResourceLink resourceLink, int deltaCount)
        {
            if ((resourceLink.ReferenceCount + deltaCount) < 0)
                throw new InvalidOperationException($"Invalid decrement on reference count (must be >=0 after decrement). Current reference count: [{resourceLink.ReferenceCount}] Decrement: [{deltaCount}].");

            resourceLink.ReferenceCount += deltaCount;
            resourceLink.AccessTotalCount++;
            resourceLink.AccessCountSinceLastRecycle++;
            resourceLink.LastAccessTime = DateTime.Now;

            if (resourceLink.ReferenceCount == 0)
                GraphicsDevice.TagResource(resourceLink);
        }

        /// <summary>
        ///   Default recycle policy. Always remove all allocated resources.
        /// </summary>
        /// <param name="resourceLink">The resource link.</param>
        private static bool DefaultRecyclePolicy(GraphicsResourceLink resourceLink) => true;

        protected struct QueryPoolDescription : IEquatable<QueryPoolDescription>
        {
            public QueryType QueryType;
            public int       QueryCount;

            public QueryPoolDescription(QueryType queryType, int queryCount)
            {
                QueryType = queryType;
                QueryCount = queryCount;
            }

            public bool Equals(QueryPoolDescription other)
            {
                return QueryType  == other.QueryType &&
                       QueryCount == other.QueryCount;
            }

            public override bool Equals(object obj)
            {
                if (obj is null)
                    return false;

                return obj is QueryPoolDescription description && Equals(description);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((int)QueryType * 397) ^ QueryCount;
                }
            }

            public static bool operator ==(QueryPoolDescription left, QueryPoolDescription right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(QueryPoolDescription left, QueryPoolDescription right)
            {
                return !left.Equals(right);
            }
        }
    }
}
