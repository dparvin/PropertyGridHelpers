using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace PropertyGridHelpers.DocStub
{
    internal sealed class UpdateDocumentation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDocumentation"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public UpdateDocumentation(string[] args)
        {
            if (args.Length < 2)
            {
                Console.Error.WriteLine("Usage: <ToolName> <AssemblyPath.dll> <OutputFolder> [options]");
                Environment.Exit(1);
            }

            // 1. Path to the DLL
            DllPath = Path.GetFullPath(args[0]);

            if (!File.Exists(DllPath))
            {
                Console.Error.WriteLine($"Error: Assembly not found: {DllPath}");
                Environment.Exit(1);
            }

            // 2. Path to the XML (same folder, same base name)
            XmlPath = Path.ChangeExtension(DllPath, ".xml");

            if (!File.Exists(XmlPath))
            {
                Console.Error.WriteLine($"Warning: XML documentation file not found: {XmlPath}");
            }

            // 3. Output directory for generated markdown files
            OutputDir = Path.GetFullPath(args[1]);

            Console.WriteLine($"✔ DLL Path:     {DllPath}");
            Console.WriteLine($"✔ XML Path:     {XmlPath}");
            Console.WriteLine($"✔ Output Path:  {OutputDir}");
        }

        /// <summary>
        /// Updates the files.
        /// </summary>
        public void UpdateFiles()
        {
            var doc = XDocument.Load(XmlPath);

            var namespaceDocs = doc.Descendants("member")
                .Where(m => (string)m.Attribute("name") is string name &&
                            name.StartsWith("T:", StringComparison.OrdinalIgnoreCase) &&
                            name.EndsWith(".NamespaceDoc", StringComparison.OrdinalIgnoreCase))
                .Select(m =>
                {
                    var fullTypeName = (string)m.Attribute("name");  // e.g., "T:PropertyGridHelpers.Attributes.NamespaceDoc"
                    var ns = fullTypeName.Substring(2, fullTypeName.Length - "T:NamespaceDoc".Length - 1);

                    return new Namespace
                    {
                        NamespaceName = ns,
                        Summary = m.Element("summary")?.Value.Trim(),
                        Remarks = m.Element("remarks")?.Value.Trim(),
                        Examples = m.Element("example")?.ToString(),
                        DocumentationPath = OutputDir
                    };
                })
                .OrderBy(n => n.NamespaceName) // Alphabetical sort
                .ToList();

            // Print for verification
            foreach (var nsDoc in namespaceDocs)
            {
                Console.WriteLine($"✔ Namespace: {nsDoc.NamespaceName}");
                Console.WriteLine($"  Summary: {nsDoc.Summary}");
                Console.WriteLine($"  Remarks: {nsDoc.Remarks}\n");
                if (!string.IsNullOrWhiteSpace(nsDoc.Examples))
                    Console.WriteLine($"  Examples: {nsDoc.Examples}");
                if (!string.Equals(nsDoc.NamespaceName, DllName, StringComparison.OrdinalIgnoreCase))
                    nsDoc.UpdateNamespaceDocumentationFile();
                else
                    MakeMainPage(namespaceDocs, nsDoc);
            }
        }

        /// <summary>
        /// Makes the main page.
        /// </summary>
        /// <param name="namespaceDocs">The namespace docs.</param>
        /// <param name="MainItem">The main item.</param>
        private void MakeMainPage(List<Namespace> namespaceDocs, Namespace MainItem)
        {
            var markdownFile = Path.Combine(MainItem.DocumentationPath, $"{MainItem.NamespaceName}.md");

            // Prepare new content to insert
            var insertLines = new List<string>
            {
                $"# {MainItem.NamespaceName} assembly"
            };

            if (!string.IsNullOrWhiteSpace(MainItem.Summary))
            {
                insertLines.Add("");
                insertLines.Add("## Summary");
                insertLines.Add(MainItem.Summary.Trim());
            }

            if (!string.IsNullOrWhiteSpace(MainItem.Remarks))
            {
                insertLines.Add("");
                insertLines.Add("## Remarks");
                insertLines.Add(MainItem.Remarks.Trim());
            }

            insertLines.Add(""); // Ensure spacing before the table
            insertLines.Add("| Namespace Name | description |");
            insertLines.Add("| --- | --- |");
            foreach (var nsDoc in namespaceDocs
                .Where(n => !string.Equals(n.NamespaceName, DllName, StringComparison.OrdinalIgnoreCase))
                .OrderBy(n => n.NamespaceName))
            {
                // Escape pipe characters for markdown
                var summaryEscaped = nsDoc.Summary?.Replace("|", "\\|").Trim() ?? "";
                insertLines.Add($"| [{nsDoc.NamespaceName}]({nsDoc.NamespaceName}Namespace.md) | {summaryEscaped} |");
            }

            insertLines.Add(""); // Final newline

            File.WriteAllLines(markdownFile, insertLines);
            Console.WriteLine($"✔ Updated {markdownFile}");
        }

        /// <summary>
        /// Gets the DLL path.
        /// </summary>
        /// <value>
        /// The DLL path.
        /// </value>
        public string DllPath
        {
            get; private set;
        }

        /// <summary>
        /// Gets the name of the DLL.
        /// </summary>
        /// <value>
        /// The name of the DLL.
        /// </value>
        public string DllName => Path.GetFileNameWithoutExtension(DllPath);

        /// <summary>
        /// Gets the XML path.
        /// </summary>
        /// <value>
        /// The XML path.
        /// </value>
        public string XmlPath
        {
            get; private set;
        }

        /// <summary>
        /// Gets the output dir.
        /// </summary>
        /// <value>
        /// The output dir.
        /// </value>
        public string OutputDir
        {
            get; private set;
        }
    }
}
