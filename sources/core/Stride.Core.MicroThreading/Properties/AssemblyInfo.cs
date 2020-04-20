// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Reflection;
using System.Runtime.CompilerServices;

#pragma warning disable 436 // Stride.PublicKeys is defined in multiple assemblies

// Make internals Stride.Framework.visible to all Stride.Framework.assemblies
[assembly: InternalsVisibleTo("Stride.Core.MicroThreading.Serializers" + Stride.PublicKeys.Default)]
[assembly: InternalsVisibleTo("Stride.Engine" + Stride.PublicKeys.Default)]
[assembly: InternalsVisibleTo("Stride.Debugger" + Stride.PublicKeys.Default)]
