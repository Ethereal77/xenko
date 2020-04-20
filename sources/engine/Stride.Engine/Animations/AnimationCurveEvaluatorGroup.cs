// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

using Stride.Updater;

namespace Stride.Animations
{
    public abstract class AnimationCurveEvaluatorGroup
    {
        public abstract Type ElementType { get; }

        public abstract void Evaluate(CompressedTimeSpan newTime, IntPtr data, UpdateObjectData[] objects);

        public virtual void Cleanup()
        {
        }
    }
}
