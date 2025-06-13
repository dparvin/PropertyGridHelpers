using System;
using System.ComponentModel;

namespace PropertyGridHelpers.Attributes
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Specifies a localized display name for a property, event, or other
    /// member in a class.
    /// </summary>
    /// <seealso cref="LocalizedTextAttribute" />
    /// <param name="resourceKey">The key identifying the localized string in the resource file.</param>
    /// <remarks>
    /// Initializes a new instance of the <see cref="LocalizedDisplayNameAttribute" /> class.
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
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method, AllowMultiple = false)]
    public class LocalizedDisplayNameAttribute(string resourceKey) : LocalizedTextAttribute(resourceKey)
    {
#else
    /// <summary>
    /// Specifies a localized display name for a property, event, or other
    /// member in a class.
    /// </summary>
    /// <seealso cref="LocalizedTextAttribute" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="LocalizedDisplayNameAttribute" /> class.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method, AllowMultiple = false)]
    public class LocalizedDisplayNameAttribute : LocalizedTextAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedDisplayNameAttribute" /> class.
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
        public LocalizedDisplayNameAttribute(string resourceKey) : base(resourceKey)
        {
        }
#endif

        /// <summary>
        /// Gets the <see cref="LocalizedDisplayNameAttribute"/> from the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static new LocalizedDisplayNameAttribute Get(ITypeDescriptorContext context) =>
            context == null || context.Instance == null || context.PropertyDescriptor == null
                ? null
                : Support.Support.GetFirstCustomAttribute<LocalizedDisplayNameAttribute>(
                    Support.Support.GetPropertyInfo(context));
    }
}