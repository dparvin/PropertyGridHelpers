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
        /// Defines the mode for auto-complete suggestions.
        /// </summary>
        public enum SourceMode
        {
            /// <summary>
            /// The values
            /// </summary>
            Values,
            /// <summary>
            /// The provider
            /// </summary>
            Provider
        }

        /// <summary>
        /// Gets the source of auto-complete suggestions.
        /// </summary>
        /// <remarks>
        /// This determines where the combo box will look to find suggestions, such as:
        /// <list type="bullet">
        /// <item><description><see cref="AutoCompleteSource.FileSystem"/> – File and directory paths.</description></item>
        /// <item><description><see cref="AutoCompleteSource.HistoryList"/> – URL history.</description></item>
        /// <item><description><see cref="AutoCompleteSource.CustomSource"/> – Values provided via <see cref="Values"/>.</description></item>
        /// </list>
        /// </remarks>
        /// <seealso cref="System.Windows.Forms.AutoCompleteSource"/>
        public AutoCompleteSource AutoCompleteSource
        {
            get;
        }

        /// <summary>
        /// Gets the mode that controls how suggestions are shown in the UI.
        /// </summary>
        /// <remarks>
        /// This controls how suggestions are presented as the user types:
        /// <list type="bullet">
        /// <item><description><see cref="AutoCompleteMode.Suggest"/> – Shows a dropdown of matches.</description></item>
        /// <item><description><see cref="AutoCompleteMode.Append"/> – Appends the closest match to the typed value.</description></item>
        /// <item><description><see cref="AutoCompleteMode.SuggestAppend"/> – Combines both behaviors.</description></item>
        /// </list>
        /// </remarks>
        /// <seealso cref="System.Windows.Forms.AutoCompleteMode"/>
        public AutoCompleteMode AutoCompleteMode
        {
            get;
        }

        /// <summary>
        /// Gets the style of the drop-down editor.
        /// </summary>
        /// <remarks>
        /// Controls how the user can interact with the drop-down:
        /// <list type="bullet">
        /// <item><description><see cref="ComboBoxStyle.DropDown"/> – Editable text box with drop-down list.</description></item>
        /// <item><description><see cref="ComboBoxStyle.DropDownList"/> – Drop-down list only (no free-form input).</description></item>
        /// <item><description><see cref="ComboBoxStyle.Simple"/> – Always visible list and editable text box.</description></item>
        /// </list>
        /// </remarks>
        /// <seealso cref="ComboBoxStyle"/>
        public ComboBoxStyle DropDownStyle
        {
            get;
        }

        /// <summary>
        /// Gets the set of custom string values used for auto-complete suggestions.
        /// </summary>
        /// <remarks>
        /// This is only used when <see cref="AutoCompleteSource"/> is set to <c>CustomSource</c>.
        /// The values can be supplied:
        /// <list type="bullet">
        /// <item><description>Directly via the attribute constructor.</description></item>
        /// <item><description>From an <c>enum</c> (uses enum names).</description></item>
        /// <item><description>From a <c>public static string[] Values</c> property on a provider type.</description></item>
        /// </list>
        /// </remarks>
        public string[] Values
        {
            get;
        }

        /// <summary>
        /// Gets the type of the provider.
        /// </summary>
        /// <value>
        /// The type of the provider.
        /// </value>
        public Type ProviderType
        {
            get;
        }

        /// <summary>
        /// Gets the mode that the attribute was created in.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        public SourceMode Mode
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="autoCompleteSource">The automatic complete source.</param>
        public AutoCompleteSetupAttribute(
            AutoCompleteSource autoCompleteSource = AutoCompleteSource.None)
            : this(AutoCompleteMode.SuggestAppend, autoCompleteSource, ComboBoxStyle.DropDown) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="autoCompleteSource">The automatic complete source.</param>
        /// <param name="dropDownStyle">The drop down style.</param>
        public AutoCompleteSetupAttribute(
            AutoCompleteSource autoCompleteSource,
            ComboBoxStyle dropDownStyle)
            : this(AutoCompleteMode.SuggestAppend, autoCompleteSource, dropDownStyle) { }

        /// <summary>
        /// Initializes a new instance using a predefined source and mode.
        /// </summary>
        /// <param name="autoCompleteMode">The automatic complete mode.</param>
        /// <param name="autoCompleteSource">The automatic complete source.</param>
        /// <param name="dropDownStyle">The drop down style.</param>
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
        /// <param name="values">The values.</param>
        public AutoCompleteSetupAttribute(
            params string[] values)
            : this(AutoCompleteMode.SuggestAppend, ComboBoxStyle.DropDown, values) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="dropDownStyle">The drop down style.</param>
        /// <param name="values">The values.</param>
        public AutoCompleteSetupAttribute(
            ComboBoxStyle dropDownStyle,
            params string[] values)
            : this(AutoCompleteMode.SuggestAppend, dropDownStyle, values) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="autoCompleteSource">The automatic complete source.</param>
        /// <param name="autoCompleteMode">The automatic complete mode.</param>
        /// <param name="values">The values.</param>
        public AutoCompleteSetupAttribute(
            AutoCompleteSource autoCompleteSource,
            AutoCompleteMode autoCompleteMode,
            params string[] values)
            : this(autoCompleteMode, autoCompleteSource, ComboBoxStyle.DropDown, values) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="autoCompleteMode">The automatic complete mode.</param>
        /// <param name="values">The values.</param>
        public AutoCompleteSetupAttribute(
            AutoCompleteMode autoCompleteMode,
            params string[] values)
            : this(autoCompleteMode, ComboBoxStyle.DropDown, values) { }

        /// <summary>
        /// Initializes a new instance using a list of values and an optional mode.
        /// </summary>
        /// <param name="autoCompleteMode">The automatic complete mode.</param>
        /// <param name="dropDownStyle">The drop down style.</param>
        /// <param name="values">The values.</param>
        public AutoCompleteSetupAttribute(
            AutoCompleteMode autoCompleteMode,
            ComboBoxStyle dropDownStyle,
            params string[] values)
            : this(autoCompleteMode, AutoCompleteSource.CustomSource, dropDownStyle, values) { }

        /// <summary>
        /// Initializes a new instance using a list of values and an optional mode.
        /// </summary>
        /// <param name="autoCompleteMode">The automatic complete mode.</param>
        /// <param name="autoCompleteSource">The automatic complete source.</param>
        /// <param name="dropDownStyle">The drop down style.</param>
        /// <param name="values">The values.</param>
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
        /// <param name="providerType">Type of the provider.</param>
        public AutoCompleteSetupAttribute(
            Type providerType)
            : this(AutoCompleteMode.SuggestAppend, ComboBoxStyle.DropDown, providerType) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="autoCompleteMode">The automatic complete mode.</param>
        /// <param name="providerType">Type of the provider.</param>
        public AutoCompleteSetupAttribute(
            AutoCompleteMode autoCompleteMode,
            Type providerType)
            : this(autoCompleteMode, ComboBoxStyle.DropDown, providerType) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteSetupAttribute" /> class.
        /// </summary>
        /// <param name="dropDownStyle">The drop down style.</param>
        /// <param name="providerType">Type of the provider.</param>
        public AutoCompleteSetupAttribute(
            ComboBoxStyle dropDownStyle,
            Type providerType)
            : this(AutoCompleteMode.SuggestAppend, dropDownStyle, providerType) { }

        /// <summary>
        /// Initializes a new instance using a type provider.
        /// </summary>
        /// <param name="autoCompleteMode">The mode.</param>
        /// <param name="dropDownStyle">The drop down style.</param>
        /// <param name="providerType">Type of the provider.</param>
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
