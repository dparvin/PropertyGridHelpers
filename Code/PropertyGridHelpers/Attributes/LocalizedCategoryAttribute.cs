using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Attribute for specifying a localized category name for a property or event.
    /// </summary>
    /// <param name="resourceKey">The key used to retrieve the localized category name from the resource file.</param>
    /// <remarks>
    /// This attribute allows category names displayed in property grids to be localized
    /// by retrieving the category name from a resource file.  If your class has a <see cref="ResourcePathAttribute" />
    /// applied to it, the resource key will be looked up in the resource file specified by that attribute.
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
    /// <seealso cref="LocalizedTextAttribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method, AllowMultiple = false)]
    public class LocalizedCategoryAttribute(string resourceKey) : LocalizedTextAttribute(resourceKey)
    {
    }
#else
    /// <summary>
    /// Attribute for specifying a localized category name for a property or event.
    /// </summary>
    /// <remarks>
    /// This attribute allows category names displayed in property grids to be localized
    /// by retrieving the category name from a resource file.  If your class has a <see cref="ResourcePathAttribute" />
    /// applied to it, the resource key will be looked up in the resource file specified by that attribute.
    /// </remarks>
    /// <seealso cref="LocalizedTextAttribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method, AllowMultiple = false)]
    public class LocalizedCategoryAttribute : LocalizedTextAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedCategoryAttribute" /> class.
        /// </summary>
        /// <param name="resourceKey">The key used to retrieve the localized category name from the resource file.</param>
        /// <remarks>
        /// The constructor fetches the localized category name using the specified resource key.
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
        public LocalizedCategoryAttribute(string resourceKey) : base(resourceKey)
        {
        }
    }
#endif
}
