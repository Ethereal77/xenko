// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace Stride.Animations
{
    public class AnimationCurveEvaluatorOptimizedIntGroup : AnimationCurveEvaluatorOptimizedBlittableGroupBase<int>
    {
        protected unsafe override void ProcessChannel(ref Channel channel, CompressedTimeSpan currentTime, IntPtr location, float factor)
        {
            *(int*)(location + channel.Offset) = channel.ValueStart.Value;
        }
    }
}
