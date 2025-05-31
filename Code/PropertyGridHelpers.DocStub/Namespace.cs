using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

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

        private string summary;
        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public string Summary
        {
            get => summary;
            set => summary = SanitizeMultilineText(value);
        }

        private string remarks;
        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks
        {
            get => remarks;
            set => remarks = SanitizeMultilineText(value);
        }

        /// <summary>
        /// Gets or sets the example.
        /// </summary>
        /// <value>
        /// The example.
        /// </value>
        public string Examples
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

                    var example = ConvertExampleElementToMarkdown();
                    if (!String.IsNullOrEmpty(example))
                    {
                        insertLines.Add("");
                        insertLines.Add("## Examples");
                        insertLines.Add(example.Trim());
                    }

                    insertLines.Add(""); // Ensure spacing before the table

                    // Insert new lines after header
                    lines.InsertRange(insertIndex, insertLines);

                    File.WriteAllLines(markdownFile, lines);
                    Console.WriteLine($"✔ Updated {markdownFile}\n\n");
                }
            }
        }

        /// <summary>
        /// Sanitizes the multi-line text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private static string SanitizeMultilineText(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            var lines = text
                .Replace(Environment.NewLine, "\n") // Normalize line endings
                .Split('\n')
                .Select(line => line.Trim());

            var result = new List<string>();
            var paragraphBuilder = new List<string>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    // End of paragraph
                    if (paragraphBuilder.Count > 0)
                    {
                        result.Add(string.Join(" ", paragraphBuilder));
                        paragraphBuilder.Clear();
                    }

                    result.Add(string.Empty); // Preserve blank line
                }
                else
                    paragraphBuilder.Add(line);
            }

            // Flush final paragraph
            if (paragraphBuilder.Count > 0)
                result.Add(string.Join(" ", paragraphBuilder));

            return string.Join("\n", result);
        }

        /// <summary>
        /// Converts the example element to markdown.
        /// </summary>
        /// <returns></returns>
        public string ConvertExampleElementToMarkdown()
        {
            if (string.IsNullOrEmpty(Examples)) return string.Empty;

            var sb = new StringBuilder();
            var exampleElement = XElement.Parse(Examples);

            foreach (var node in exampleElement.Nodes())
            {
                switch (node)
                {
                    case XText text:
                        _ = sb.Append(text.Value.TrimEnd());
                        break;

                    case XElement el when el.Name == "c":
#if NET8_0_OR_GREATER
                        _ = sb.Append(CultureInfo.CurrentCulture, $"`{el.Value.Trim()}`");
#else
                        _ = sb.Append($"`{el.Value.Trim()}`");
#endif
                        break;

                    case XElement el when el.Name == "code":
                        _ = sb.AppendLine();
                        _ = sb.AppendLine();

                        var language = el.Attribute("language")?.Value ?? "csharp";

                        // Normalize
                        var lines = el.Value
                            .Replace(Environment.NewLine, "\n")
                            .Replace("\r", "\n")
                            .Split('\n')
                            .Select(line => line.TrimEnd())
                            .ToList();

                        while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[0]))
                            lines.RemoveAt(0);

                        var minIndent = lines
                            .Where(l => !string.IsNullOrWhiteSpace(l))
                            .Select(l => l.TakeWhile(char.IsWhiteSpace).Count())
                            .DefaultIfEmpty(0)
                            .Min();

#if NET8_0_OR_GREATER
                        lines = [.. lines.Select(line => line.Length >= minIndent ? line[minIndent..] : line)];
                        _ = sb.AppendLine(CultureInfo.CurrentCulture, $"```{language}");
#else
                        lines = lines.Select(line => line.Length >= minIndent ? line.Substring(minIndent) : line).ToList();
                        _ = sb.AppendLine($"```{language}");
#endif
                        foreach (var line in lines)
                            _ = sb.AppendLine(line);
                        _ = sb.AppendLine("```");

                        _ = sb.AppendLine();
                        break;

                    default:
                        // Fallback for any unknown XML nodes
                        sb.Append(((XElement)node)?.Value);
                        break;
                }

                _ = sb.Append(' '); // space between nodes
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// Gets a string representation of the class.
        /// </summary>
        /// <returns>A string indicating the class name.</returns>
        public override string ToString() =>
            $"{nameof(NamespaceName)} = {NamespaceName}";
    }
}
