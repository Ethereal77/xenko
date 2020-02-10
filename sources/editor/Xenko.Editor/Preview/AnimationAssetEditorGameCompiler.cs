// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Core.Assets;
using Xenko.Core.Assets.Compiler;
using Xenko.Animations;
using Xenko.Assets.Models;

namespace Xenko.Editor.Preview
{
    [AssetCompiler(typeof(AnimationAsset), typeof(EditorGameCompilationContext))]
    public class AnimationAssetEditorGameCompiler : AssetCompilerBase
    {
        protected override void Prepare(AssetCompilerContext context, AssetItem assetItem, string targetUrlInStorage, AssetCompilerResult result)
        {
            result.BuildSteps.Add(new DummyAssetCommand<AnimationAsset, AnimationClip>(assetItem));
        }
    }
}
