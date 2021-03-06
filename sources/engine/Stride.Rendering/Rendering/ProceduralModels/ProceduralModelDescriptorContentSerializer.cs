// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System;

using Stride.Core;
using Stride.Core.Serialization;
using Stride.Core.Serialization.Contents;
using Stride.Graphics;

namespace Stride.Rendering.ProceduralModels
{
    internal class ProceduralModelDescriptorContentSerializer : ContentSerializerBase<Model>
    {
        private static readonly DataContentSerializerHelper<ProceduralModelDescriptor> DataSerializerHelper = new DataContentSerializerHelper<ProceduralModelDescriptor>();

        public override Type SerializationType
        {
            get { return typeof(ProceduralModelDescriptor); }
        }

        public override void Serialize(ContentSerializerContext context, SerializationStream stream, Model model)
        {
            var proceduralModel = new ProceduralModelDescriptor();
            DataSerializerHelper.Serialize(context, stream, proceduralModel);

            var services = stream.Context.Tags.Get(ServiceRegistry.ServiceRegistryKey);

            proceduralModel.GenerateModel(services, model);
        }
    }
}
