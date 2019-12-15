// Copyright (c) Xenko contributors (https://xenko.com) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Core;

namespace Xenko.Navigation
{
    internal static class NativeInvoke
    {
        internal const string Library = "libxenkonavigation";

        internal static void PreLoad()
        {
#if XENKO_PLATFORM_WINDOWS
            NativeLibrary.PreloadLibrary(Library + ".dll", typeof(NativeInvoke));
#else
            NativeLibrary.PreloadLibrary(Library + ".so", typeof(NativeInvoke));
#endif
        }

        static NativeInvoke()
        {
            PreLoad();
        }
    }
}
