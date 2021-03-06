// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using Stride.Core.Assets;
using Stride.Physics;

namespace Stride.Assets.Physics
{
    public class ColliderShapeBoxFactory : AssetFactory<ColliderShapeAsset>
    {
        public static ColliderShapeAsset Create()
        {
            return new ColliderShapeAsset { ColliderShapes = { new BoxColliderShapeDesc() } };
        }

        public override ColliderShapeAsset New()
        {
            return Create();
        }
    }

    public class ColliderShapeCapsuleFactory : AssetFactory<ColliderShapeAsset>
    {
        public static ColliderShapeAsset Create()
        {
            return new ColliderShapeAsset { ColliderShapes = { new CapsuleColliderShapeDesc() } };
        }

        public override ColliderShapeAsset New()
        {
            return Create();
        }
    }

    public class ColliderShapeConvexHullFactory : AssetFactory<ColliderShapeAsset>
    {
        public static ColliderShapeAsset Create()
        {
            return new ColliderShapeAsset { ColliderShapes = { new ConvexHullColliderShapeDesc() } };
        }

        public override ColliderShapeAsset New()
        {
            return Create();
        }
    }

    public class ColliderShapeCylinderFactory : AssetFactory<ColliderShapeAsset>
    {
        public static ColliderShapeAsset Create()
        {
            return new ColliderShapeAsset { ColliderShapes = { new CylinderColliderShapeDesc() } };
        }

        public override ColliderShapeAsset New()
        {
            return Create();
        }
    }

    public class ColliderShapeConeFactory : AssetFactory<ColliderShapeAsset>
    {
        public static ColliderShapeAsset Create()
        {
            return new ColliderShapeAsset { ColliderShapes = { new ConeColliderShapeDesc() } };
        }

        public override ColliderShapeAsset New()
        {
            return Create();
        }
    }

    public class ColliderShapePlaneFactory : AssetFactory<ColliderShapeAsset>
    {
        public static ColliderShapeAsset Create()
        {
            return new ColliderShapeAsset { ColliderShapes = { new StaticPlaneColliderShapeDesc() } };
        }

        public override ColliderShapeAsset New()
        {
            return Create();
        }
    }

    public class ColliderShapeSphereFactory : AssetFactory<ColliderShapeAsset>
    {
        public static ColliderShapeAsset Create()
        {
            return new ColliderShapeAsset { ColliderShapes = { new SphereColliderShapeDesc() } };
        }

        public override ColliderShapeAsset New()
        {
            return Create();
        }
    }

    public class ColliderShapeHeightfieldFactory : AssetFactory<ColliderShapeAsset>
    {
        public static ColliderShapeAsset Create()
        {
            return new ColliderShapeAsset { ColliderShapes = { new HeightfieldColliderShapeDesc() } };
        }

        public override ColliderShapeAsset New()
        {
            return Create();
        }
    }
}
