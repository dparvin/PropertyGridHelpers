#if NET461_OR_GREATER || NET5_0_OR_GREATER
using XmlDocMarkdown.Core;
#else
using System;
using static System.Net.WebRequestMethods;
#endif

namespace PropertyGridHelpers.DocStub
{
    /// <summary>
    /// This is a stub for the PropertyGridHelpers project.
    /// </summary>
    /// <remarks>
    /// This project is used in DevOps to generate documentation for the PropertyGridHelpers project.
    /// The process that generates the documentation is in the PropertyGridHelpers documentation Release pipeline.
    /// 
    /// If you are reading this on the GitHub repository, I use Microsoft's Azure DevOps to build the code, 
    /// run the tests, and generate the documentation. The documentation is generated using the xmlDocMarkdown tools.
    /// <a href="https://ejball.com/XmlDocMarkdown/">click here for the documentation on the xmlDocMarkdown tools</a>.
    /// </remarks>
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
