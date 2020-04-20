// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;

using Stride.Core.Assets.Editor.ViewModel;

namespace Stride.Core.Assets.Editor.Services
{
    public interface IAssetsPluginService
    {
        IReadOnlyCollection<AssetsPlugin> Plugins { get; }

        bool HasImagesForEnum(SessionViewModel session, Type enumType);

        object GetImageForEnum(SessionViewModel session, object value);

        IEnumerable<Type> GetPrimitiveTypes(SessionViewModel session);

        IEditorView ConstructEditionView(AssetViewModel asset);

        bool HasEditorView(SessionViewModel session, Type assetType);
    }
}
