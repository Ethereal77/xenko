﻿// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

// PkgCmdID.cs
// MUST match PkgCmdID.h

using System;
using System.ComponentModel.Design;

using Microsoft.VisualStudio.Shell;

namespace Stride.VisualStudio
{
    static class StridePackageCmdIdList
    {
        public const uint cmdStridePlatformSelect =        0x100;
        public const uint cmdStrideOpenWithGameStudio = 0x101;
        public const uint cmdStridePlatformSelectList = 0x102;
        public const uint cmdStrideCleanIntermediateAssetsSolutionCommand = 0x103;
        public const uint cmdStrideCleanIntermediateAssetsProjectCommand = 0x104;
    }
}
