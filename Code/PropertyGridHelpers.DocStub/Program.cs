#if NET461_OR_GREATER || NET5_0_OR_GREATER
using XmlDocMarkdown.Core;
#else
using System;
#endif

namespace PropertyGridHelpers.DocStub
{
    /// <summary>
    /// This is a stub for the PropertyGridHelpers project.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args) =>
#if NET461_OR_GREATER || NET5_0_OR_GREATER
            XmlDocMarkdownApp.Run(args);
#else
            throw new NotImplementedException("This is a stub for the PropertyGridHelpers project.");
#endif

    }
}
