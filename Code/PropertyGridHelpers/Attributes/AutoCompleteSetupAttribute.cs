using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace PropertyGridHelpers.Attributes
{
    /// <summary>
    /// Specifies auto-complete behavior for a string property in a <see cref="PropertyGrid"/>,
    /// providing suggestions based on built-in sources, a custom string list, an enum, or a static provider.
    /// </summary>
    /// <remarks>
    /// This attribute is used in conjunction with a custom <see cref="UITypeEditor"/> (e.g., <c>AutoCompleteComboBoxEditor</c>)
    /// to configure how suggestions are shown to the user at design-time in a property grid editor.
    ///
    /// Auto-complete behavior includes:
    /// <list type="bullet">
    /// <item>
    /// <term><see cref="AutoCompleteSource"/></term>
    /// <description>
    /// The source of the suggestions, such as file system, history list, or custom-defined values.
    /// </description>
    /// </item>
    /// <item>
    /// <term><see cref="AutoCompleteMode"/></term>
    /// <description>
    /// Defines how the suggestions are shown—inline (Append), in a dropdown (Suggest), or both.
    /// </description>
    /// </item>
    /// <item>
    /// <term><see cref="DropDownStyle"/></term>
    /// <description>
    /// Controls whether the editor is editable (DropDown), fixed (DropDownList), or simple.
    /// </description>
    /// </item>
    /// <item>
    /// <term><see cref="Values"/></term>
    /// <description>
    /// If <see cref="AutoCompleteSource"/> is <c>CustomSource</c>, this holds the actual list of values used for suggestions.
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    /// <example>
    /// Use with built-in file system suggestions:
    /// <code>
    /// [AutoCompleteSetup(AutoCompleteSource.FileSystem)]
    /// public string FilePath { get; set; }
    /// </code>
    /// Use with custom string values:
    /// <code>
    /// [AutoCompleteSetup("Red", "Green", "Blue")]
    /// public string ColorName { get; set; }
    /// </code>
    /// Use with enum names:
    /// <code>
    /// [AutoCompleteSetup(typeof(KnownFruits))]
    /// public string FavoriteFruit { get; set; }
    /// </code>
    /// Use with static string provider:
    /// <code>
    /// public static class KnownUsers
    /// {
    ///     public static string[] Values => new[] { "Admin", "Editor", "Viewer" };
    /// }
    ///
    /// [AutoCompleteSetup(typeof(KnownUsers))]
    /// public string Role { get; set; }
    /// </code>
    /// </example>
    /// <seealso cref="System.Windows.Forms.AutoCompleteSource"/>
    /// <seealso cref="System.Windows.Forms.AutoCompleteMode"/>
    /// <seealso cref="ComboBoxStyle"/>
    /// <seealso cref="UITypeEditor"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AutoCompleteSetupAttribute : Attribute
    {
        /// <summary>
        /// Defines the mode that determines how auto-complete suggestions are provided.
        /// </summary>
        public enum SourceMode
        {
            /// <summary>
            /// Indicates that the auto-complete suggestions are supplied directly via a values array.
            /// </summary>
            Values,
            /// <summary>
            /// Indicates that the auto-complete suggestions are retrieved from a provider type,
            /// such as an enum or a class with a static string array property.
            /// </summary>
            Provider
        }

        /// <summary>
        /// Gets the source from which auto-complete suggestions are retrieved.
        /// </summary>
        /// <remarks>
        /// Controls where the combo box obtains its suggestions:
        /// <list type="bullet">
        ///   <item><description><see cref="AutoCompleteSource.FileSystem"/> – File and directory paths.</description></item>
        ///   <item><description><see cref="AutoCompleteSource.HistoryList"/> – URL history.</description></item>
        ///   <item><description><see cref="AutoCompleteSource.CustomSource"/> – User-supplied values via <see cref="Values"/>.</description></item>
        /// </list>
        /// </remarks>
        /// <seealso cref="System.Windows.Forms.AutoCompleteSource"/>
        public AutoCompleteSource AutoCompleteSource
        {
            get;
        }

        /// <summary>
        /// Gets the mode that determines how suggestions appear in the UI as the user types.
        /// </summary>
        /// <remarks>
        /// Controls the behavior of suggestion display:
        /// <list type="bullet">
        ///   <item><description><see cref="AutoCompleteMode.Suggest"/> – Shows a list of matches.</description></item>
        ///   <item><description><see cref="AutoCompleteMode.Append"/> – Auto-completes the typed text with the closest match.</description></item>
        ///   <item><description><see cref="AutoCompleteMode.SuggestAppend"/> – Combines suggest and append modes.</description></item>
        /// </list>
        /// </remarks>
        /// <seealso cref="System.Windows.Forms.AutoCompleteMode"/>
        public AutoCompleteMode AutoCompleteMode
        {
            get;
        }

        /// <summary>
        /// Gets the style of the drop-down editor used in the UI.
        /// </summary>
        /// <remarks>
        /// Controls how the user can edit and interact with the drop-down:
        /// <list type="bullet">
        ///   <item><description><see cref="ComboBoxStyle.DropDown"/> – Editable text box with a drop-down list.</description></item>
        ///   <item><description><see cref="ComboBoxStyle.DropDownList"/> – Drop-down list only, no free-form input allowed.</description></item>
        ///   <item><description><see cref="ComboBoxStyle.Simple"/> – Always visible list with an editable text box.</description></item>
        /// </list>
        /// </remarks>
        /// <seealso cref="ComboBoxStyle"/>
        public ComboBoxStyle DropDownStyle
        {
            get;
        }

        /// <summary>
        /// Gets the set of custom string values used for auto-complete suggestions
        /// when <see cref="AutoCompleteSource"/> is set to <c>CustomSource</c>.
        /// </summary>
        /// <remarks>
        /// These values can be specified:
        /// <list type="bullet">
        ///   <item><description>Directly via the attribute constructor.</description></item>
        ///   <item><description>Derived from an <c>enum</c> type (using its names).</description></item>
        ///   <item><description>Loaded from a class with a public static <c>string[] Values</c> property.</description></item>
        /// </list>
        /// </remarks>

        public string[] Values
        {
            get;
        }

        /// <summary>
        /// Gets the type that provides the source of auto-complete values when
        /// <see cref="AutoCompleteSource"/> is set to <c>CustomSource</c>.
        /// Typically, this can be an enum type or a class with a static <c>string[] Values</c> property.
        /// </summary>
        /// <value>
        /// A <see cref="Type"/> representing the provider of suggestions for the editor,
        /// or <c>null</c> if not specified.
        /// </value>
        public Type ProviderType
        {
            get;
        }

        /// <summary>
        /// Gets the source mode used to determine how auto-complete values were initialized
        /// for this attribute.
        /// </summary>
        /// <value>
        /// A <see cref="SourceMode"/> enumeration value indicating how the values
        /// were resolved (e.g., via direct array, enum, or provider type).
        /// </value>
        public SourceMode Mode
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="autoCompleteSource">Sets <see cref="AutoCompleteSource"/>.</param>
        public AutoCompleteSetupAttribute(
            AutoCompleteSource autoCompleteSource = AutoCompleteSource.None)
            : this(AutoCompleteMode.SuggestAppend, autoCompleteSource, ComboBoxStyle.DropDown) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="autoCompleteSource">Sets <see cref="AutoCompleteSource"/>.</param>
        /// <param name="dropDownStyle">Sets <see cref="DropDownStyle"/>.</param>
        public AutoCompleteSetupAttribute(
            AutoCompleteSource autoCompleteSource,
            ComboBoxStyle dropDownStyle)
            : this(AutoCompleteMode.SuggestAppend, autoCompleteSource, dropDownStyle) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="autoCompleteMode">Sets <see cref="AutoCompleteMode"/>.</param>
        /// <param name="autoCompleteSource">Sets <see cref="AutoCompleteSource"/>.</param>
        /// <param name="dropDownStyle">Sets <see cref="DropDownStyle"/>.</param>
        public AutoCompleteSetupAttribute(
            AutoCompleteMode autoCompleteMode,
            AutoCompleteSource autoCompleteSource,
            ComboBoxStyle dropDownStyle)
#if NET35 || NET452
            : this(autoCompleteMode, autoCompleteSource, dropDownStyle, new string[0]) { }
#elif NET5_0_OR_GREATER
            : this(autoCompleteMode, autoCompleteSource, dropDownStyle, []) { }
#else
            : this(autoCompleteMode, autoCompleteSource, dropDownStyle, Array.Empty<string>()) { }
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="values">Sets <see cref="Values"/>.</param>
        public AutoCompleteSetupAttribute(
            params string[] values)
            : this(AutoCompleteMode.SuggestAppend, ComboBoxStyle.DropDown, values) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="dropDownStyle">Sets <see cref="DropDownStyle"/>.</param>
        /// <param name="values">Sets <see cref="Values"/>.</param>
        public AutoCompleteSetupAttribute(
            ComboBoxStyle dropDownStyle,
            params string[] values)
            : this(AutoCompleteMode.SuggestAppend, dropDownStyle, values) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="autoCompleteSource">Sets <see cref="AutoCompleteSource"/>.</param>
        /// <param name="autoCompleteMode">Sets <see cref="AutoCompleteMode"/>.</param>
        /// <param name="values">Sets <see cref="Values"/>.</param>
        public AutoCompleteSetupAttribute(
            AutoCompleteSource autoCompleteSource,
            AutoCompleteMode autoCompleteMode,
            params string[] values)
            : this(autoCompleteMode, autoCompleteSource, ComboBoxStyle.DropDown, values) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="autoCompleteMode">Sets <see cref="AutoCompleteMode"/>.</param>
        /// <param name="values">Sets <see cref="Values"/>.</param>
        public AutoCompleteSetupAttribute(
            AutoCompleteMode autoCompleteMode,
            params string[] values)
            : this(autoCompleteMode, ComboBoxStyle.DropDown, values) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="autoCompleteMode">Sets <see cref="AutoCompleteMode"/>.</param>
        /// <param name="dropDownStyle">Sets <see cref="DropDownStyle"/>.</param>
        /// <param name="values">Sets <see cref="Values"/>.</param>
        public AutoCompleteSetupAttribute(
            AutoCompleteMode autoCompleteMode,
            ComboBoxStyle dropDownStyle,
            params string[] values)
            : this(autoCompleteMode, AutoCompleteSource.CustomSource, dropDownStyle, values) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="autoCompleteMode">Sets <see cref="AutoCompleteMode"/>.</param>
        /// <param name="autoCompleteSource">Sets <see cref="AutoCompleteSource"/>.</param>
        /// <param name="dropDownStyle">Sets <see cref="DropDownStyle"/>.</param>
        /// <param name="values">Sets <see cref="Values"/>.</param>
        /// <exception cref="ArgumentException">At least one auto-complete value must be provided.</exception>
        public AutoCompleteSetupAttribute(
            AutoCompleteMode autoCompleteMode,
            AutoCompleteSource autoCompleteSource,
            ComboBoxStyle dropDownStyle,
            params string[] values)
        {
            Console.WriteLine("🚨 AutoCompleteSetupAttribute values ctor called!");
            AutoCompleteMode = autoCompleteMode;
            DropDownStyle = dropDownStyle;
            AutoCompleteSource = autoCompleteSource;
            Values = values;

            Mode = SourceMode.Values;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute"/> class.
        /// </summary>
        /// <param name="providerType">Sets <see cref="ProviderType"/>.</param>
        public AutoCompleteSetupAttribute(
        Type providerType)
            : this(AutoCompleteMode.SuggestAppend, ComboBoxStyle.DropDown, providerType) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="autoCompleteMode">Sets <see cref="AutoCompleteMode"/>.</param>
        /// <param name="providerType">Sets <see cref="ProviderType"/>.</param>
        public AutoCompleteSetupAttribute(
            AutoCompleteMode autoCompleteMode,
            Type providerType)
            : this(autoCompleteMode, ComboBoxStyle.DropDown, providerType) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="dropDownStyle">Sets <see cref="DropDownStyle"/>.</param>
        /// <param name="providerType">Sets <see cref="ProviderType"/>.</param>
        public AutoCompleteSetupAttribute(
            ComboBoxStyle dropDownStyle,
            Type providerType)
            : this(AutoCompleteMode.SuggestAppend, dropDownStyle, providerType) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="autoCompleteMode">Sets <see cref="AutoCompleteMode"/>.</param>
        /// <param name="dropDownStyle">Sets <see cref="DropDownStyle"/>.</param>
        /// <param name="providerType">Sets <see cref="ProviderType"/>.</param>
        /// <exception cref="ArgumentNullException">providerType</exception>
        /// <exception cref="ArgumentException">
        /// The type '{providerType.FullName}' must define a public static property named '{requiredPropertyName}'.
        /// or
        /// The '{requiredPropertyName}' property on '{providerType.FullName}' must be of type string[].
        /// </exception>
        /// <exception cref="ArgumentNullException">providerType</exception>
        /// <exception cref="ArgumentException">The type '{providerType.FullName}' must define a public static property named 'Values'.
        /// or
        /// The 'Values' property on '{providerType.FullName}' must be of type string[].</exception>
        public AutoCompleteSetupAttribute(
            AutoCompleteMode autoCompleteMode,
            ComboBoxStyle dropDownStyle,
            Type providerType)
        {
            Console.WriteLine("🚨 AutoCompleteSetupAttribute providerType ctor called!");
            AutoCompleteMode = autoCompleteMode;
            DropDownStyle = dropDownStyle;
            AutoCompleteSource = AutoCompleteSource.CustomSource;
            ProviderType = providerType;

            Mode = SourceMode.Provider;
        }

        /// <summary>
        /// Get the <see cref="AutoCompleteSetupAttribute"/> for the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static AutoCompleteSetupAttribute Get(ITypeDescriptorContext context) =>
            context == null || context.Instance == null || context.PropertyDescriptor == null
                ? null
                : Support.Support.GetFirstCustomAttribute<AutoCompleteSetupAttribute>(
                    Support.Support.GetPropertyInfo(context));

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString() =>
            AutoCompleteSource == AutoCompleteSource.CustomSource && (Mode == SourceMode.Values && Values != null)
                ? $"{nameof(AutoCompleteSetupAttribute)}: {nameof(AutoCompleteMode)}={AutoCompleteMode}, {nameof(AutoCompleteSource)}={AutoCompleteSource}, {nameof(DropDownStyle)}={DropDownStyle}, {nameof(Values)}=[{string.Join(", ", Values)}]"
                : AutoCompleteSource == AutoCompleteSource.CustomSource && (Mode == SourceMode.Provider && ProviderType != null) ?
                    $"{nameof(AutoCompleteSetupAttribute)}: {nameof(AutoCompleteMode)}={AutoCompleteMode}, {nameof(AutoCompleteSource)}={AutoCompleteSource}, {nameof(DropDownStyle)}={DropDownStyle}, {nameof(ProviderType)}={ProviderType.Name}" :
                    $"{nameof(AutoCompleteSetupAttribute)}: {nameof(AutoCompleteMode)}={AutoCompleteMode}, {nameof(AutoCompleteSource)}={AutoCompleteSource}, {nameof(DropDownStyle)}={DropDownStyle}";
    }
}
