using PropertyGridHelpers.TypeDescriptors;
using System;
using System.ComponentModel;

namespace PropertyGridHelpers.TypeDescriptionProviders
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Localized Type Description Provider
    /// </summary>
    /// <param name="type">The type.</param>
    /// <seealso cref="TypeDescriptionProvider" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="LocalizedTypeDescriptionProvider"/> class.
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
    public class LocalizedTypeDescriptionProvider(Type type) :
        TypeDescriptionProvider(TypeDescriptor.GetProvider(type))
    {
        private readonly TypeDescriptionProvider baseProvider = TypeDescriptor.GetProvider(type);

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedTypeDescriptionProvider"/> class.
        /// </summary>
        public LocalizedTypeDescriptionProvider()
            : this(typeof(object)) { } // Provide a fallback for dynamic creation
#else
    /// <summary>
    /// Localized Type Description Provider
    /// </summary>
    /// <seealso cref="TypeDescriptionProvider" />
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
    public class LocalizedTypeDescriptionProvider :
        TypeDescriptionProvider
    {
        private readonly TypeDescriptionProvider baseProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedTypeDescriptionProvider"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public LocalizedTypeDescriptionProvider(Type type)
            : base(TypeDescriptor.GetProvider(type)) =>
            baseProvider = TypeDescriptor.GetProvider(type);

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedTypeDescriptionProvider"/> class.
        /// </summary>
        public LocalizedTypeDescriptionProvider()
            : this(typeof(object)) { } // Provide a fallback for dynamic creation
#endif

        /// <summary>
        /// Returns a custom type descriptor for the specified type and instance, enabling localization or
        /// other metadata customization.
        /// </summary>
        /// <param name="objectType">
        /// The <see cref="Type"/> of the object for which to retrieve the type descriptor.
        /// Cannot be <c>null</c>.
        /// </param>
        /// <param name="instance">
        /// An optional instance of the object whose type descriptor is being requested. May be <c>null</c>.
        /// </param>
        /// <returns>
        /// A custom <see cref="ICustomTypeDescriptor"/> for the specified type, wrapping the base descriptor
        /// to provide additional metadata support.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="objectType"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// This override decorates the standard type descriptor returned by the base provider
        /// with a <see cref="LocalizedTypeDescriptor"/> to enable localization of property and event metadata.
        /// </remarks>
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
#if NET8_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(objectType);
#else
            if (objectType == null)
                throw new ArgumentNullException(nameof(objectType));
#endif

            var baseDescriptor = baseProvider.GetTypeDescriptor(objectType, instance);
            return new LocalizedTypeDescriptor(baseDescriptor);
        }
    }
}
