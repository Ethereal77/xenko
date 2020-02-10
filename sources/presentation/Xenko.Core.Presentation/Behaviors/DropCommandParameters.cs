// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Core;
using Xenko.Core.Serialization;

namespace Xenko.Core.Presentation.Behaviors
{
    /// <summary>
    /// Information about a drag & drop command.
    /// </summary>
    // TODO: Move this in a ViewModel-dedicated assembly
    [DataContract]
    public class DropCommandParameters
    {
        public string DataType { get; set; }
        public object Data { get; set; }
        public object Parent { get; set; }
        public int SourceIndex { get; set; }
        public int TargetIndex { get; set; }
        public object Sender { get; set; }
    }
}
