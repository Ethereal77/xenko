// Copyright (c) 2018-2020 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Stride.ProjectGenerator
{
    class SynchronizeProjectProcessor : IProjectProcessor
    {
        private ProjectType projectType;

        public SynchronizeProjectProcessor(ProjectType projectType)
        {
            this.projectType = projectType;
        }

        public void Process(ProjectProcessorContext context)
        {
            var doc = context.Document;

            var fullPath = context.Project.FullPath;
            
            // Remove .Windows (if any)
            fullPath = fullPath.Replace(".Windows", string.Empty);

            // Add current platform instead
            fullPath = Path.Combine(Path.GetDirectoryName(fullPath), Path.GetFileNameWithoutExtension(fullPath) + "." + projectType + Path.GetExtension(fullPath));

            if (File.Exists(fullPath))
            {
                // Already exists, let's synchronize!
                context.Modified = true;

                // First, let's load project
                var newDoc = XDocument.Load(fullPath);
                var ns = newDoc.Root.Name.Namespace;
                var mgr = new XmlNamespaceManager(new NameTable());
                mgr.AddNamespace("x", ns.NamespaceName);

                // Let's load ItemGroup items from previous and new items
                var oldItemGroups = doc.XPathSelectElements("/x:Project/x:ItemGroup", context.NamespaceManager).ToArray();
                var newItemGroups = newDoc.XPathSelectElements("/x:Project/x:ItemGroup", mgr).ToArray();

                // Remove non-tagged item from new document
                foreach (var itemGroup in newItemGroups)
                {
                    var nonTaggedElements = GetUserElements(itemGroup);
                    foreach (var nonTaggedElement in nonTaggedElements)
                    {
                        nonTaggedElement.Remove();
                    }
                }

                // Copy back non-tagged item from old document
                // Try to insert in second ItemGroup (usually first one is Reference)
                var insertionItemGroup = newItemGroups.Length >= 2 ? newItemGroups[1] : newItemGroups[0];

                foreach (var itemGroup in oldItemGroups)
                {
                    var nonTaggedElements = GetUserElements(itemGroup);
                    foreach (var nonTaggedElement in nonTaggedElements)
                    {
                        var element = new XElement(nonTaggedElement);
                        // fixup namespace (https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/how-to-change-the-namespace-for-an-entire-xml-tree)
                        foreach (var el in element.DescendantsAndSelf())
                        {
                            el.Name = ns.GetName(el.Name.LocalName);
                            var atList = el.Attributes().ToList();
                        }
                        insertionItemGroup.Add(element);
                    }
                }

                // Clear empty item groups
                foreach (var itemGroup in newItemGroups.ToArray())
                {
                    if (!itemGroup.HasElements)
                        itemGroup.Remove();
                }

                // Set newly generated document
                context.Document = newDoc;
            }
        }

        /// <summary>
        /// Gets the user elements (not tagged with AutoGenerated).
        /// </summary>
        /// <param name="itemGroup">The old item group.</param>
        /// <returns></returns>
        private static XElement[] GetUserElements(XElement itemGroup)
        {
            return itemGroup
                .Elements()
                .Where(x => !x.Attributes().Any(y => y.Name == "Label" && y.Value == "Stride.DoNotSync"))
                .ToArray();
        }

        private static void GenerateInfoFile(string infoFile, string bundleDisplayName, string bundleIdentifier)
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XDocumentType("plist", "-//Apple//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null),
                new XElement("plist", new XAttribute("version", "1.0"),
                    new XElement("dict",
                        new XElement("key", "UIDeviceFamily"),
                        new XElement("array",
                            new XElement("integer", "1"), // 1 = app runs on iPhone and iPod Touch
                            new XElement("integer", "2") //  2 = app runs on iPad (3 = app runs on Apple TV)
                            ),
                        new XElement("key", "UISupportedInterfaceOrientations"),
                        new XElement("array",
                            new XElement("string", "UIInterfaceOrientationPortrait"),
                            new XElement("string", "UIInterfaceOrientationLandscapeLeft"),
                            new XElement("string", "UIInterfaceOrientationLandscapeRight")
                            ),
                        new XElement("key", "UISupportedInterfaceOrientations~ipad"),
                        new XElement("array",
                            new XElement("string", "UIInterfaceOrientationPortrait"),
                            new XElement("string", "UIInterfaceOrientationPortraitUpsideDown"),
                            new XElement("string", "UIInterfaceOrientationLandscapeRight"),
                            new XElement("string", "UIInterfaceOrientationLandscapeRight")
                            ),
                        new XElement("key", "MinimumOSVersion"),
                        new XElement("string", "3.2"),
                        new XElement("key", "CFBundleDisplayName"),
                        new XElement("string", bundleDisplayName),
                        new XElement("key", "CFBundleIdentifier"),
                        new XElement("string", bundleIdentifier)
                        )
                    )
                );

            using (var fileStream = new StreamWriter(infoFile))
            {
                fileStream.Write(doc.ToString());
            }
        }
    }
}
