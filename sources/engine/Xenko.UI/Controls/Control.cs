// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Diagnostics;

using Xenko.Core;

namespace Xenko.UI.Controls
{
    /// <summary>
    /// Represents the base class for user interface (UI) controls. 
    /// </summary>
    [DataContract(nameof(Control))]
    [DebuggerDisplay("Control - Name={Name}")]
    public abstract class Control : UIElement
    {
        protected Thickness padding = Thickness.UniformCuboid(0);

        /// <summary>
        /// Gets or sets the padding inside a control.
        /// </summary>
        /// <userdoc>The padding inside a control.</userdoc>
        [DataMember]
        [Display(category: LayoutCategory)]
        public Thickness Padding
        {
            get { return padding; }
            set
            {
                padding = value;
                InvalidateMeasure();
            }
        }
    }
}
