// Copyright (c) 2018-2020 Xenko and its contributors (https://xenko.com)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Xml;
using System.Xml.Linq;

using Xenko.Core.VisualStudio;

namespace Xenko.ProjectGenerator
{
    public class ProjectProcessorContext
    {
        public bool Modified { get; set; }
        public Solution Solution { get; private set; }
        public Project Project { get; private set; }
        public XDocument Document { get; internal set; }
        public XmlNamespaceManager NamespaceManager { get; private set; }

        public ProjectProcessorContext(Solution solution, Project project, XDocument document, XmlNamespaceManager namespaceManager)
        {
            Solution = solution;
            Project = project;
            Document = document;
            NamespaceManager = namespaceManager;
        }
    }
}
