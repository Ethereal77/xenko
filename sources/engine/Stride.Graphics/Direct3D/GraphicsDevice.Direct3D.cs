// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

#if STRIDE_GRAPHICS_API_DIRECT3D11

using System;
using System.Collections.Generic;

using SharpDX;
using SharpDX.Direct3D11;
using Stride.Core;

namespace Stride.Graphics
{
    public partial class GraphicsDevice
    {
        internal readonly int ConstantBufferDataPlacementAlignment = 16;

        private const GraphicsPlatform GraphicPlatform = GraphicsPlatform.Direct3D11;

        private bool simulateReset = false;
        private string rendererName;

        private Device nativeDevice;
        private DeviceContext nativeDeviceContext;
        private readonly Queue<Query> disjointQueries = new Queue<Query>(4);
        private readonly Stack<Query> currentDisjointQueries = new Stack<Query>(2);

        internal GraphicsProfile RequestedProfile;

        private SharpDX.Direct3D11.DeviceCreationFlags creationFlags;

        /// <summary>
        ///   Gets the tick frquency of timestamp queries, in hertz.
        /// </summary>
        public long TimestampFrequency { get; private set; }

        /// <summary>
        ///   Gets the status of this device.
        /// </summary>
        /// <value>The graphics device status.</value>
        public GraphicsDeviceStatus GraphicsDeviceStatus
        {
            get
            {
                if (simulateReset)
                {
                    simulateReset = false;
                    return GraphicsDeviceStatus.Reset;
                }

                var result = NativeDevice.DeviceRemovedReason;
                if (result == SharpDX.DXGI.ResultCode.DeviceRemoved)
                {
                    return GraphicsDeviceStatus.Removed;
                }

                if (result == SharpDX.DXGI.ResultCode.DeviceReset)
                {
                    return GraphicsDeviceStatus.Reset;
                }

                if (result == SharpDX.DXGI.ResultCode.DeviceHung)
                {
                    return GraphicsDeviceStatus.Hung;
                }

                if (result == SharpDX.DXGI.ResultCode.DriverInternalError)
                {
                    return GraphicsDeviceStatus.InternalError;
                }

                if (result == SharpDX.DXGI.ResultCode.InvalidCall)
                {
                    return GraphicsDeviceStatus.InvalidCall;
                }

                if (result.Code < 0)
                {
                    return GraphicsDeviceStatus.Reset;
                }

                return GraphicsDeviceStatus.Normal;
            }
        }

        /// <summary>
        ///   Gets the underlying native device.
        /// </summary>
        /// <value>The native device.</value>
        internal Device NativeDevice => nativeDevice;

        /// <summary>
        ///   Gets the underlying native device context.
        /// </summary>
        /// <value>The native device context.</value>
        internal DeviceContext NativeDeviceContext => nativeDeviceContext;

        /// <summary>
        ///   Marks the context as active on the current thread.
        /// </summary>
        public void Begin()
        {
            FrameTriangleCount = 0;
            FrameDrawCalls = 0;

            Query currentDisjointQuery;

            // Try to read back the oldest disjoint query and reuse it. If not ready, create a new one.
            if (disjointQueries.Count > 0 && NativeDeviceContext.GetData(disjointQueries.Peek(), out QueryDataTimestampDisjoint result))
            {
                TimestampFrequency = result.Frequency;
                currentDisjointQuery = disjointQueries.Dequeue();
            }
            else
            {
                var disjointQueryDiscription = new QueryDescription { Type = SharpDX.Direct3D11.QueryType.TimestampDisjoint };
                currentDisjointQuery = new Query(NativeDevice, disjointQueryDiscription);
            }

            currentDisjointQueries.Push(currentDisjointQuery);
            NativeDeviceContext.Begin(currentDisjointQuery);
        }

        /// <summary>
        ///   Enables profiling.
        /// </summary>
        /// <param name="enabledFlag"><c>true</c> to enable profiling; <c>false</c> to disable.</param>
        public void EnableProfile(bool enabledFlag) { }

        /// <summary>
        ///   Unmarks context as active on the current thread.
        /// </summary>
        public void End()
        {
            // If this fails, it means Begin() / End() don't match and something is very wrong
            var currentDisjointQuery = currentDisjointQueries.Pop();
            NativeDeviceContext.End(currentDisjointQuery);
            disjointQueries.Enqueue(currentDisjointQuery);
        }

        /// <summary>
        ///   Executes a deferred command list.
        /// </summary>
        /// <param name="commandList">The deferred command list.</param>
        public void ExecuteCommandList(CompiledCommandList commandList) => throw new NotImplementedException();

        /// <summary>
        ///   Executes multiple deferred command lists.
        /// </summary>
        /// <param name="commandLists">The deferred command lists.</param>
        public void ExecuteCommandLists(int count, CompiledCommandList[] commandLists) => throw new NotImplementedException();

        /// <summary>
        ///   Initiates a simulated device lost / reset status.
        /// </summary>
        public void SimulateReset()
        {
            simulateReset = true;
        }

        private void InitializePostFeatures()
        {
            // Create the main command list
            InternalMainCommandList = new CommandList(this);
        }

        private string GetRendererName() => rendererName;

        /// <summary>
        ///   Initializes the device for the specified profile.
        /// </summary>
        /// <param name="graphicsProfiles">The graphics profiles.</param>
        /// <param name="deviceCreationFlags">The device creation flags.</param>
        /// <param name="windowHandle">The window handle.</param>
        private void InitializePlatformDevice(GraphicsProfile[] graphicsProfiles, DeviceCreationFlags deviceCreationFlags, object windowHandle)
        {
            if (nativeDevice != null)
            {
                // Destroy previous device
                ReleaseDevice();
            }

            rendererName = Adapter.NativeAdapter.Description.Description;

            // Profiling is supported through PIX markers
            IsProfilingSupported = true;

            // Map GraphicsProfile to D3D11 FeatureLevel
            creationFlags = (SharpDX.Direct3D11.DeviceCreationFlags) deviceCreationFlags;

            // Create Direct3D 11 Device with feature Level based on profile
            for (int index = 0; index < graphicsProfiles.Length; index++)
            {
                var graphicsProfile = graphicsProfiles[index];
                try
                {
                    // Direct3D 12 supports only feature level 11+
                    var level = graphicsProfile.ToFeatureLevel();

                    nativeDevice = new Device(Adapter.NativeAdapter, creationFlags, level);

                    RequestedProfile = graphicsProfile;
                    break;
                }
                catch
                {
                    if (index == graphicsProfiles.Length - 1)
                        throw;
                }
            }

            nativeDeviceContext = nativeDevice.ImmediateContext;

            // We keep one reference so that it doesn't disappear with InternalMainCommandList
            ((IUnknown) nativeDeviceContext).AddReference();
            if (IsDebugMode)
                GraphicsResourceBase.SetDebugName(this, nativeDeviceContext, "ImmediateContext");
        }

        private void AdjustDefaultPipelineStateDescription(ref PipelineStateDescription pipelineStateDescription)
        {
            // On Direct3D, the default state is Less instead of our LessEqual
            // Let's update default pipeline state so that it correspond to D3D state after a "ClearState()"
            pipelineStateDescription.DepthStencilState.DepthBufferFunction = CompareFunction.Less;
        }

        protected void DestroyPlatformDevice()
        {
            ReleaseDevice();
        }

        private void ReleaseDevice()
        {
            foreach (var query in disjointQueries)
                query.Dispose();
            disjointQueries.Clear();

            // Display D3D11 ref counting info
            NativeDevice.ImmediateContext.ClearState();
            NativeDevice.ImmediateContext.Flush();

            if (IsDebugMode)
            {
                var debugDevice = NativeDevice.QueryInterfaceOrNull<DeviceDebug>();
                if (debugDevice != null)
                {
                    debugDevice.ReportLiveDeviceObjects(ReportingLevel.Detail);
                    debugDevice.Dispose();
                }
            }

            nativeDevice.Dispose();
            nativeDevice = null;
        }

        internal void OnDestroyed() { }

        /// <summary>
        ///   Tags a resource to be discarded (and possibly renamed) as it is not goind to be used anymore.
        /// </summary>
        /// <param name="resourceLink">The resource to discard.</param>
        internal void TagResource(GraphicsResourceLink resourceLink)
        {
            if (resourceLink.Resource is GraphicsResource resource)
                resource.DiscardNextMap = true;
        }
    }
}

#endif
