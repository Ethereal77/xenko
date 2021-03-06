// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;

using Stride.Core.Presentation.Collections;
using Stride.Core.Presentation.ViewModel;

namespace Stride.Core.Assets.Editor.Components.TemplateDescriptions.ViewModels
{
    public abstract class TemplateDescriptionCollectionViewModel : DispatcherViewModel
    {
        private readonly ObservableList<ITemplateDescriptionViewModel> templates = new ObservableList<ITemplateDescriptionViewModel>();

        private TemplateDescriptionGroupViewModel selectedGroup;
        private ITemplateDescriptionViewModel selectedTemplate;

        private string name;
        private bool usedDefinedName;


        protected TemplateDescriptionCollectionViewModel(IViewModelServiceProvider serviceProvider)
            : base(serviceProvider)
        { }


        public IReadOnlyObservableCollection<ITemplateDescriptionViewModel> Templates => templates;

        public abstract IEnumerable<TemplateDescriptionGroupViewModel> RootGroups { get; }

        public TemplateDescriptionGroupViewModel SelectedGroup
        {
            get => selectedGroup;
            set => SetValue(ref selectedGroup, value, UpdateTemplateList);
        }

        public ITemplateDescriptionViewModel SelectedTemplate
        {
            get => selectedTemplate;
            set
            {
                SetValue(ref selectedTemplate, value);
                UpdateName();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                usedDefinedName = !string.IsNullOrEmpty(value) && name != value;
                SetValue(ref name, value);
            }
        }


        public abstract bool ValidateProperties(out string error);

        protected static TemplateDescriptionGroupViewModel ProcessGroup(TemplateDescriptionGroupViewModel rootGroup, string groupPath)
        {
            if (string.IsNullOrWhiteSpace(groupPath))
                return null;

            var groupDirectories = groupPath.Split("/\\".ToCharArray());
            return groupDirectories.Aggregate(
                rootGroup,
                (current, groupDirectory) => current.SubGroups.FirstOrDefault(group => group.Name == groupDirectory)
                ?? new TemplateDescriptionGroupViewModel(current, groupDirectory));
        }

        protected abstract string UpdateNameFromSelectedTemplate();

        protected void UpdateTemplateList()
        {
            templates.Clear();
            if (SelectedGroup != null)
            {
                templates.AddRange(SelectedGroup.GetTemplatesRecursively());
            }
        }

        private void UpdateName()
        {
            if (usedDefinedName)
                return;

            Name = SelectedTemplate != null ? UpdateNameFromSelectedTemplate() : string.Empty;

            usedDefinedName = false;
        }
    }
}
