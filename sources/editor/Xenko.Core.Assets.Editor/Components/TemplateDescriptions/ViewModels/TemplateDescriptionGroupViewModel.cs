// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

using Xenko.Core.Extensions;
using Xenko.Core.Presentation.Collections;
using Xenko.Core.Presentation.ViewModel;

namespace Xenko.Core.Assets.Editor.Components.TemplateDescriptions.ViewModels
{
    public class TemplateDescriptionGroupViewModel : DispatcherViewModel, IComparable<TemplateDescriptionGroupViewModel>
    {
        public const string Separator = "/";

        private readonly SortedObservableCollection<TemplateDescriptionGroupViewModel> subGroups = new SortedObservableCollection<TemplateDescriptionGroupViewModel>();

        public TemplateDescriptionGroupViewModel(TemplateDescriptionGroupViewModel parent, string name)
            : this(parent.SafeArgument(nameof(parent)).ServiceProvider, name)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            Parent = parent;
            Parent.subGroups.Add(this);
        }

        public TemplateDescriptionGroupViewModel(IViewModelServiceProvider serviceProvider, string name)
            : base(serviceProvider)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            Name = name;
            Templates = new ObservableList<ITemplateDescriptionViewModel>();
            DependentProperties.Add(nameof(Parent), new[] { nameof(Path) });
        }

        public string Name { get; }

        public string Path => IsRoot ? "" : Parent.Path + Name + (Name.Length > 0 ? Separator : "");

        public TemplateDescriptionGroupViewModel Parent { get; }

        public bool IsRoot => Parent == null;

        public IReadOnlyObservableCollection<TemplateDescriptionGroupViewModel> SubGroups => subGroups;

        public ObservableList<ITemplateDescriptionViewModel> Templates { get; }

        public IEnumerable<ITemplateDescriptionViewModel> GetTemplatesRecursively()
        {
            var allGroup = SubGroups.SelectDeep(x => x.SubGroups);
            return Templates.Concat(allGroup.SelectMany(x => x.Templates));
        }

        public void Clear()
        {
            subGroups.Clear();
            Templates.Clear();
        }

        /// <inheritdoc/>
        public int CompareTo(TemplateDescriptionGroupViewModel other)
        {
            return other != null ? string.Compare(Name, other.Name, StringComparison.InvariantCultureIgnoreCase) : -1;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "TemplateDescriptionGroupViewModel [" + Path + "]";
        }
    }
}
