// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Stride.Core.Annotations;
using Stride.Core.Reflection;

namespace Stride.Editor.EditorGame.Game
{
    public sealed class EditorGameServiceRegistry : IAsyncDisposable
    {
        public List<IEditorGameService> Services { get; } = new List<IEditorGameService>();

        [CanBeNull]
        public T Get<T>()
        {
            return Services.OfType<T>().FirstOrDefault();
        }

        [CanBeNull]
        public IEditorGameService Get([NotNull] Type serviceType)
        {
            if (serviceType is null)
                throw new ArgumentNullException(nameof(serviceType));
            if (!serviceType.HasInterface(typeof(IEditorGameService)))
                throw new ArgumentException($@"The given type must be a type that implements {nameof(IEditorGameService)}.", nameof(serviceType));

            return Services.FirstOrDefault(serviceType.IsInstanceOfType);
        }

        public void Add<T>([NotNull] T service) where T : IEditorGameService
        {
            if (service is null)
                throw new ArgumentNullException(nameof(service));

            Services.Add(service);
        }

        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        {
            for (var index = Services.Count - 1; index >= 0; index--)
            {
                var service = Services[index];
                await service.DisposeAsync();
            }
            Services.Clear();
        }
    }
}

