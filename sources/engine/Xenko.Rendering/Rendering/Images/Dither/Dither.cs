// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Core;

namespace Xenko.Rendering.Images.Dither
{
    [DataContract("Dither")]
    public class Dither : ColorTransform
    {
        public Dither() : base("Dither")
        {
        }

        public override void UpdateParameters(ColorTransformContext context)
        {
            base.UpdateParameters(context);

            Parameters.Set(DitherKeys.Time, (float)(context.RenderContext.Time.Total.TotalSeconds));
        }
    }
}
