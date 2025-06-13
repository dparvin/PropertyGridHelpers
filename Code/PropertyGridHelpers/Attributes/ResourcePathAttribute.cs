using System;
using System.ComponentModel;
using System.Reflection;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies a resource path for use with the 
    /// <see cref="UIEditors.ImageTextUIEditor"/> class.
    /// </summary>
    /// <param name="resourcePath">The path to the resource.</param>
    /// <param name="resourceAssembly">The resource assembly.</param>
    /// <remarks>
    /// This attribute is applied to properties or enum values to specify 
    /// the resource path that the <see cref="UIEditors.ImageTextUIEditor"/> 
    /// should use when displaying images.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Class, AllowMultiple = false)]
    public class ResourcePathAttribute(string resourcePath, string resourceAssembly = null) : Attribute
#else
    /// <summary>
    /// Specifies a resource path for use with the 
    /// <see cref="UIEditors.ImageTextUIEditor"/> class.
    /// </summary>
    /// <remarks>
    /// This attribute is applied to properties or enum values to specify 
    /// the resource path that the <see cref="UIEditors.ImageTextUIEditor"/> 
    /// should use when displaying images.  It can also be applied to classes
    /// for use with the <see cref="TypeDescriptionProviders.LocalizedTypeDescriptionProvider"/> to 
    /// specify the resource path for localized text for the Categories, Descriptions and Display names.
    /// </remarks>
    /// <example>
    ///   <code language="csharp">
    ///       [ResourcePath(nameof(TestControl))]
    ///       [TypeDescriptionProvider(typeof(LocalizedTypeDescriptionProvider))]
    ///       public partial class TestControl : UserControl
    ///       {
    ///           [LocalizedCategory("Category_Layout")]
    ///           [LocalizedDescription("Description_Scrollbar")]
    ///           [LocalizedDisplayName("DisplayName_Scrollbar")]
    ///           [Editor(typeof(FlagEnumUIEditor&lt;EnumTextConverter&lt;ScrollBars&gt;&gt;), typeof(UITypeEditor))]
    ///           [TypeConverter(typeof(EnumTextConverter&lt;ScrollBars&gt;))]
    ///           [DefaultValue(ScrollBars.None)]
    ///           [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    ///           [EditorBrowsable(EditorBrowsableState.Always)]
    ///           [Bindable(true)]
    ///           public ScrollBars Scrollbars
    ///           {
    ///               get => _Scrollbars;
    ///               set => _Scrollbars = value;
    ///           }
    ///       }
    ///   </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Class, AllowMultiple = false)]
    public class ResourcePathAttribute : Attribute
#endif
    {
        /// <summary>
        /// Gets the resource path associated with the property or enum value.
        /// </summary>
        /// <value>
        /// A string representing the resource path.
        /// </value>
        public string ResourcePath
        {
            get;
#if NET8_0_OR_GREATER
        } = resourcePath;
#else
        }
#endif

        /// <summary>
        /// Gets the resource assembly.
        /// </summary>
        /// <value>
        /// The resource assembly.
        /// </value>
        public string ResourceAssembly
        {
            get;
#if NET8_0_OR_GREATER
        } = resourceAssembly;
#else
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePathAttribute" />
        /// class with the specified resource path.
        /// </summary>
        /// <param name="resourcePath">The path to the resource.</param>
        /// <param name="resourceAssembly">The resource assembly.</param>
        public ResourcePathAttribute(string resourcePath, string resourceAssembly = null)
        {
            ResourcePath = resourcePath;
            ResourceAssembly = resourceAssembly;
        }
#endif

        /// <summary>
        /// Resolves the assembly object from the stored assembly name.
        /// </summary>
        public Assembly GetAssembly() => string.IsNullOrEmpty(ResourceAssembly) ?
                                         Assembly.GetCallingAssembly() :
                                         Assembly.Load(ResourceAssembly);

        /// <summary>
        /// Gets the <see cref="ResourcePathAttribute" /> for the specified Enum value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static ResourcePathAttribute Get(ITypeDescriptorContext context)
        {
            if (context == null)
                return null;

            // 1. Look for attribute on the property
            if (context.PropertyDescriptor != null)
            {
                if (context.PropertyDescriptor.Attributes[typeof(ResourcePathAttribute)] is ResourcePathAttribute attr)
                    return attr;
            }

            // 3. Look for attribute on the class itself
            return context.Instance == null ? null : Support.Support.GetFirstCustomAttribute<ResourcePathAttribute>(context.Instance.GetType());
        }
    }
}
