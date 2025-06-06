﻿using PropertyGridHelpers.TypeDescriptors;
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
        /// Gets the type descriptor.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">objectType</exception>
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
