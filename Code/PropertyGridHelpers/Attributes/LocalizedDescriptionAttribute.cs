using System;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies a localized description for a property, event, or other member in a class.
    /// </summary>
    /// <remarks>
    /// This attribute holds the key to the resource entry to get the description text from a resource file, allowing 
    /// descriptions to be localized. Apply this attribute to a member, providing the resource key.  It is expected
    /// to work together with the <see cref="ResourcePathAttribute"/> to figure out where to get the resource string
    /// from
    /// </remarks>
    /// <seealso cref="Attribute" />
    /// <param name="resourceKey">The key identifying the localized string in the resource file.</param>
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
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method, AllowMultiple = false)]
    public class LocalizedDescriptionAttribute(string resourceKey) : LocalizedTextAttribute(resourceKey)
    {
    }
#else
    /// <summary>
    /// Specifies a localized description for a property, event, or other member in a class.
    /// </summary>
    /// <remarks>
    /// This attribute retrieves the description text from a resource file, allowing descriptions to be localized.
    /// Apply this attribute to a member, providing the resource key and the resource source type containing the localization.
    /// </remarks>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method, AllowMultiple = false)]
    public class LocalizedDescriptionAttribute : LocalizedTextAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedDescriptionAttribute" /> class.
        /// </summary>
        /// <param name="resourceKey">The key identifying the localized string in the resource file.</param>
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
        public LocalizedDescriptionAttribute(string resourceKey) : base(resourceKey)
        {
        }
    }
#endif
}
