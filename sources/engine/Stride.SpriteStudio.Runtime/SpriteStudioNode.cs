// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core;
using Stride.Core.Mathematics;

namespace Stride.SpriteStudio.Runtime
{
    public enum SpriteStudioBlending
    {
        Mix,
        Multiplication,
        Addition,
        Subtraction
    }

    [DataContract]
    public class SpriteStudioNode
    {
        public SpriteStudioNode()
        {
            BaseState = new SpriteStudioNodeState
            {
                Position = Vector2.Zero,
                RotationZ = 0.0f,
                Priority = 0,
                Scale = Vector2.One,
                Transparency = 1.0f,
                Hide = 1,
                SpriteId = -1,
                BlendColor = Color.White,
                BlendType = SpriteStudioBlending.Mix,
                BlendFactor = 0.0f
            };
        }

        public string Name;
        public int Id = -1;
        public int ParentId;
        public bool IsNull;
        public SpriteStudioBlending AlphaBlending;
        public bool AlphaInheritance;
        public bool FlphInheritance;
        public bool FlpvInheritance;
        public bool HideInheritance;
        public bool NoInheritance;

        public SpriteStudioNodeState BaseState;
    }
}
