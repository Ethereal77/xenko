// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.Collections.Generic;

using Stride.Core.Extensions;
using Stride.Core.Presentation.ViewModel;

namespace Stride.Core.Assets.Editor.Components.TemplateDescriptions.ViewModels
{
    public class AddItemTemplateCollectionViewModel : TemplateDescriptionCollectionViewModel
    {
        private string searchToken;

        public AddItemTemplateCollectionViewModel(IViewModelServiceProvider serviceProvider) : base(serviceProvider)
        {
            RootGroup = new TemplateDescriptionGroupViewModel(serviceProvider, "All templates");
        }

        public override IEnumerable<TemplateDescriptionGroupViewModel> RootGroups => RootGroup.Yield();

        public TemplateDescriptionGroupViewModel RootGroup { get; }

        public string SearchToken { get { return searchToken; } set { SetValue(ref searchToken, value, () => SelectedGroup = RootGroup); } }

        public override bool ValidateProperties(out string error)
        {
            error = "";
            return true;
        }

        protected override string UpdateNameFromSelectedTemplate()
        {
            return null;
        }

    }
}
