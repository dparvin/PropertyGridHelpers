using System;
using System.Windows.Forms.Design;

namespace PropertyGridHelpersTest.Support
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Test Service Provider
    /// </summary>
    /// <seealso cref="IServiceProvider" />
    public partial class TestServiceProvider(IWindowsFormsEditorService editorService) : IServiceProvider
#else
    /// <summary>
    /// Test Service Provider
    /// </summary>
    /// <seealso cref="IServiceProvider" />
    public partial class TestServiceProvider : IServiceProvider
#endif
    {

#if NET8_0_OR_GREATER
        private readonly IWindowsFormsEditorService _editorService = editorService;
#else
        private readonly IWindowsFormsEditorService _editorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestServiceProvider"/> class.
        /// </summary>
        /// <param name="editorService">The editor service.</param>
        public TestServiceProvider(IWindowsFormsEditorService editorService) => _editorService = editorService;
#endif
        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public object GetService(Type serviceType) => serviceType == typeof(IWindowsFormsEditorService) ? _editorService : (object)null;
    }
}
