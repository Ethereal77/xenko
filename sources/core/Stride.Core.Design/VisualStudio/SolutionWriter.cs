// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Copyright (c) 2009 SLNTools - Christian Warren
// See the LICENSE.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Stride.Core.Annotations;

namespace Stride.Core.VisualStudio
{
    internal class SolutionWriter : IDisposable
    {
        private StreamWriter writer;

        public SolutionWriter(string solutionFullPath) : this(new FileStream(solutionFullPath, FileMode.Create, FileAccess.Write))
        {
        }

        public SolutionWriter([NotNull] Stream writer)
        {
            this.writer = new StreamWriter(writer, Encoding.UTF8);
        }

        public void Dispose()
        {
            if (writer != null)
            {
                writer.Dispose();
                writer = null;
            }
        }

        public void Flush()
        {
            writer.Flush();
        }

        public void WriteSolutionFile([NotNull] Solution solution)
        {
            lock (writer)
            {
                WriteHeader(solution);
                WriteProjects(solution);
                WriteGlobal(solution);
            }
        }

        private void WriteGlobal([NotNull] Solution solution)
        {
            writer.WriteLine("Global");
            WriteGlobalSections(solution);
            writer.WriteLine("EndGlobal");
        }

        private void WriteGlobalSections([NotNull] Solution solution)
        {
            foreach (var globalSection in solution.GlobalSections)
            {
                var propertyLines = new List<PropertyItem>(globalSection.Properties);
                switch (globalSection.Name)
                {
                    case "NestedProjects":
                        foreach (var project in solution.Projects)
                        {
                            if (project.ParentGuid != Guid.Empty)
                            {
                                propertyLines.Add(new PropertyItem(project.Guid.ToString("B").ToUpperInvariant(), project.ParentGuid.ToString("B").ToUpperInvariant()));
                            }
                        }
                        break;

                    case "ProjectConfigurationPlatforms":
                        foreach (var project in solution.Projects)
                        {
                            foreach (var propertyLine in project.PlatformProperties)
                            {
                                propertyLines.Add(
                                    new PropertyItem(
                                        $"{project.Guid.ToString("B").ToUpperInvariant()}.{propertyLine.Name}",
                                        propertyLine.Value));
                            }
                        }
                        break;

                    default:
                        if (globalSection.Name.EndsWith("Control", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var index = 1;
                            foreach (var project in solution.Projects)
                            {
                                if (project.VersionControlProperties.Count > 0)
                                {
                                    foreach (var propertyLine in project.VersionControlProperties)
                                    {
                                        propertyLines.Add(
                                            new PropertyItem(
                                                $"{propertyLine.Name}{index}",
                                                propertyLine.Value));
                                    }
                                    index++;
                                }
                            }

                            propertyLines.Insert(0, new PropertyItem("SccNumberOfProjects", index.ToString()));
                        }
                        break;
                }

                WriteSection(globalSection, propertyLines);
            }
        }

        private void WriteHeader([NotNull] Solution solution)
        {
            // If the header doesn't start with an empty line, add one
            // (The first line of sln files saved as UTF-8 with BOM must be blank, otherwise Visual Studio Version Selector will not detect VS version correctly.)
            if (solution.Headers.Count == 0 || solution.Headers[0].Trim().Length > 0)
            {
                writer.WriteLine();
            }

            foreach (var line in solution.Headers)
            {
                writer.WriteLine(line);
            }

            foreach (var propertyLine in solution.Properties)
            {
                writer.WriteLine("{0} = {1}", propertyLine.Name, propertyLine.Value);
            }
        }

        private void WriteProjects([NotNull] Solution solution)
        {
            foreach (var project in solution.Projects)
            {
                writer.WriteLine("Project(\"{0}\") = \"{1}\", \"{2}\", \"{3}\"",
                    project.TypeGuid.ToString("B").ToUpperInvariant(),
                    project.Name,
                    project.GetRelativePath(solution),
                    project.Guid.ToString("B").ToUpperInvariant());
                foreach (var projectSection in project.Sections)
                {
                    WriteSection(projectSection, projectSection.Properties);
                }
                writer.WriteLine("EndProject");
            }
        }

        private void WriteSection([NotNull] Section section, [NotNull] IEnumerable<PropertyItem> propertyLines)
        {
            writer.WriteLine("\t{0}({1}) = {2}", section.SectionType, section.Name, section.Step);
            foreach (var propertyLine in propertyLines)
            {
                writer.WriteLine("\t\t{0} = {1}", propertyLine.Name, propertyLine.Value);
            }
            writer.WriteLine("\tEnd{0}", section.SectionType);
        }
    }
}
