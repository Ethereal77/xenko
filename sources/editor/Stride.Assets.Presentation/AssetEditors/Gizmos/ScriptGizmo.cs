// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Stride.Engine;

namespace Stride.Assets.Presentation.AssetEditors.Gizmos
{
    [GizmoComponent(typeof(ScriptComponent), true)]
    public class ScriptGizmo : BillboardingGizmo<ScriptComponent>
    {
        public ScriptGizmo(EntityComponent component)
            : base(component, "Script", GizmoResources.ScriptGizmo)
        {
        }
    }
    [GizmoComponent(typeof(UIComponent), true)]
    public class UIGizmo : BillboardingGizmo<UIComponent>
    {
        public UIGizmo(EntityComponent component)
            : base(component, "UI", GizmoResources.UIGizmo)
        {
        }
    }
}
