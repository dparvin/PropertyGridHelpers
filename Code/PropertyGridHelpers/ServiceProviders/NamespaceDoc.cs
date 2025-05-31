using System;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Windows.Forms.Design;

namespace PropertyGridHelpers.ServiceProviders
{
    /// <summary>
    /// A lightweight implementation of <see cref="IServiceProvider"/> that allows manual registration 
    /// and retrieval of service instances by type.
    /// </summary>
    /// <remarks>
    /// This is particularly useful for testing scenarios involving components that depend on service injection, 
    /// such as editors implementing <see cref="UITypeEditor"/> or services using <see cref="IWindowsFormsEditorService"/>.
    /// <para>
    /// It can also be used at runtime to programmatically supply services that PropertyGridHelpers components rely on, 
    /// especially when not using attributes for service resolution.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var serviceProvider = new CustomServiceProvider();
    /// var instance = new TestClassWithAttribute();
    /// var propDesc = TypeDescriptor.GetProperties(instance)["PropertyWithoutAttribute"];
    ///
    /// var editor = new ResourcePathEditor();
    /// var context = new CustomTypeDescriptorContext(propDesc, instance);
    /// serviceProvider.AddService(typeof(IWindowsFormsEditorService), new FakeEditorService());
    ///
    /// var result = editor.EditValue(context, serviceProvider, "(none)");
    /// </code>
    /// </example>
    [CompilerGenerated]
    public class NamespaceDoc
    {
    }
}
