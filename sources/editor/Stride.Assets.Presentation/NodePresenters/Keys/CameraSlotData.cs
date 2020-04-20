// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;

using Stride.Core;
using Stride.Rendering.Compositing;

namespace Stride.Assets.Presentation.NodePresenters.Keys
{
    public static class CameraSlotData
    {
        public const string SceneCameraSlots = nameof(SceneCameraSlots);
        public const string UpdateCameraSlotIndex = nameof(UpdateCameraSlotIndex);

        public static readonly PropertyKey<List<SceneCameraSlot>> CameraSlotsKey = new PropertyKey<List<SceneCameraSlot>>(SceneCameraSlots, typeof(CameraSlotData));
    }
}
