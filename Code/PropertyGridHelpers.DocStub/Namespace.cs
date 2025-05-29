using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PropertyGridHelpers.DocStub
{
    /// <summary>
    /// This class is used to fix the namespaces in the documentation.
    /// </summary>
    internal sealed class Namespace
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocStub.Namespace"/> class.
        /// </summary>
        public Namespace()
        {
        }

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>
        /// The namespace.
        /// </value>
        public string NamespaceName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the documentation path.
        /// </summary>
        /// <value>
        /// The documentation path.
        /// </value>
        public string DocumentationPath
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public string Summary
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks
        {
            get; set;
        }

        /// <summary>
        /// Updates the documentation files.
        /// </summary>
        /// <returns></returns>
        public void UpdateNamespaceDocumentationFile()
        {
            if (NamespaceName == null)
                throw new ArgumentNullException(nameof(NamespaceName), "Namespace cannot be null.");
            if (DocumentationPath == null)
                throw new ArgumentNullException(nameof(DocumentationPath), "DocumentationPath cannot be null.");

            var markdownFile = Path.Combine(DocumentationPath, $"{NamespaceName}Namespace.md");

            if (File.Exists(markdownFile))
            {
                var lines = File.ReadAllLines(markdownFile).ToList();

                var insertIndex = -1;

                // Find the line index for the header
                for (var i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Trim().StartsWith("## ", StringComparison.OrdinalIgnoreCase) &&
                        lines[i].Contains($"{NamespaceName} namespace"))
                    {
                        insertIndex = i + 1;
                        break;
                    }
                }

                if (insertIndex == -1)
                {
                    Console.WriteLine($"⚠ Could not find header for namespace '{NamespaceName}' in {markdownFile}");
                }
                else
                {
                    // Prepare new content to insert
                    var insertLines = new List<string>();

                    if (!string.IsNullOrWhiteSpace(Summary))
                    {
                        insertLines.Add("");
                        insertLines.Add("## Summary");
                        insertLines.Add(Summary.Trim());
                    }

                    if (!string.IsNullOrWhiteSpace(Remarks))
                    {
                        insertLines.Add("");
                        insertLines.Add("## Remarks");
                        insertLines.Add(Remarks.Trim());
                    }

                    insertLines.Add(""); // Ensure spacing before the table

                    // Insert new lines after header
                    lines.InsertRange(insertIndex, insertLines);

                    File.WriteAllLines(markdownFile, lines);
                    Console.WriteLine($"✔ Updated {markdownFile}");
                }
            }
        }

        /// <summary>
        /// Gets a string representation of the class.
        /// </summary>
        /// <returns>A string indicating the class name.</returns>
        public override string ToString() =>
            $"{nameof(NamespaceName)} = {NamespaceName}";
    }
}
