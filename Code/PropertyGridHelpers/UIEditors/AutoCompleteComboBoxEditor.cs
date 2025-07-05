using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.Controls;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// Provides a <see cref="UITypeEditor"/> that shows an auto-complete-enabled drop-down list using an 
    /// <see cref="AutoCompleteComboBox"/>, allowing quick selection of string values within a <see cref="PropertyGrid"/>.
    /// </summary>
    /// <remarks>
    /// This editor supports auto-complete behavior based on a configurable
    /// <see cref="AutoCompleteSource"/> and <see cref="AutoCompleteMode"/> as defined by the
    /// <see cref="AutoCompleteSetupAttribute"/>.
    ///
    /// For <see cref="AutoCompleteSource.CustomSource"/>, the list of suggestions can be provided:
    /// <list type="bullet">
    /// <item><description>Directly via string array</description></item>
    /// <item><description>From an enum's names</description></item>
    /// <item><description>From a public static <c>string[] Values</c> property on a class</description></item>
    /// </list>
    /// 
    /// When configured with an enum type, the editor displays the enum's names, and returns the
    /// selected enum value rather than a string. Otherwise, the selected string is returned.
    /// </remarks>
    /// <example>
    /// Apply this editor to a string property like this:
    /// <code>
    /// [AutoCompleteSetup(AutoCompleteSource.FileSystem)]
    /// [Editor(typeof(AutoCompleteComboBoxEditor), typeof(UITypeEditor))]
    /// public string FilePath { get; set; }
    ///
    /// [AutoCompleteSetup(typeof(ConsoleColor))]
    /// [Editor(typeof(AutoCompleteComboBoxEditor), typeof(UITypeEditor))]
    /// public string FavoriteColor { get; set; }
    ///
    /// [AutoCompleteSetup("Red", "Green", "Blue")]
    /// [Editor(typeof(AutoCompleteComboBoxEditor), typeof(UITypeEditor))]
    /// public string CustomColor { get; set; }
    /// </code>
    /// </example>
    /// <seealso cref="DropDownVisualizer{T}"/>
    /// <seealso cref="AutoCompleteComboBox"/>
    /// <seealso cref="AutoCompleteSetupAttribute"/>
    public class AutoCompleteComboBoxEditor
        : DropDownVisualizer<AutoCompleteComboBox>
    {
        /// <summary>
        /// Gets or sets the <see cref="EnumConverter"/> used to convert between
        /// the displayed text in the combo box and the corresponding enum value.
        /// </summary>
        /// <remarks>
        /// This converter supports mapping from the display name to the actual enum constant,
        /// ensuring that the editor returns the correct enum value even when the display text differs.
        /// </remarks>
        public EnumConverter Converter
        {
            get; set;
        }

        /// <summary>
        /// Displays an <see cref="AutoCompleteComboBox"/> editor in a dropdown and returns the
        /// updated value after editing completes. Supports dynamic item lists and
        /// enum mapping when configured.
        /// </summary>
        /// <param name="context">Provides context information about the design-time environment.</param>
        /// <param name="provider">A service provider that can provide an <see cref="IWindowsFormsEditorService"/>.</param>
        /// <param name="value">The current value of the property being edited.</param>
        /// <returns>
        /// The edited value as returned by the control. This is typically a string,
        /// but may be the corresponding enum value if the editor is configured with
        /// an enum type. If editing is canceled or fails, returns the original value.
        /// </returns>
        public override object EditValue(
            ITypeDescriptorContext context,
            IServiceProvider provider,
            object value)
        {
            // Check for the attribute and apply the source before showing the dropdown
            if (context?.PropertyDescriptor != null)
            {
                var sourceAttr = (AutoCompleteSetupAttribute)context.PropertyDescriptor.Attributes[typeof(AutoCompleteSetupAttribute)];

                if (sourceAttr != null)
                {
                    ValidateSetup(sourceAttr, context);
                    DropDownControl.AutoCompleteMode = sourceAttr.AutoCompleteMode;
                    DropDownControl.AutoCompleteSource = sourceAttr.AutoCompleteSource;
                    DropDownControl.DropDownStyle = sourceAttr.DropDownStyle;

                    if (sourceAttr.AutoCompleteSource == AutoCompleteSource.CustomSource)
                    {
                        DropDownControl.AutoCompleteCustomSource.Clear();
                        DropDownControl.Items.Clear();

                        object[] items;

                        var providerType = sourceAttr.ProviderType ?? context.PropertyDescriptor.PropertyType;

                        if (Converter != null && providerType.IsEnum)
                        {
#if NET5_0_OR_GREATER
                            items = [.. Enum.GetValues(providerType)
                                .Cast<object>()
                                .Select(e => new ItemWrapper<object>(
                                    Converter.ConvertToString(context, CultureInfo.CurrentCulture, e), e))];
#else
                            items = Enum.GetValues(providerType)
                                .Cast<object>()
                                .Select(e => new ItemWrapper<object>(
                                    Converter.ConvertToString(context, CultureInfo.CurrentCulture, e), e))
                                .ToArray();
#endif
                        }
                        else
                            items = ResolveValues(sourceAttr, context.PropertyDescriptor);

#if NET5_0_OR_GREATER
                        if (items[0] is ItemWrapper<object>)
                            DropDownControl.AutoCompleteCustomSource.AddRange(
                                [.. items.Cast<ItemWrapper<object>>().Select(i => i.DisplayText)]);
                        else
                            DropDownControl.AutoCompleteCustomSource.AddRange(
                                [.. items.Select(i => i.ToString())]);
#else
                        if (items[0] is ItemWrapper<object>)
                            DropDownControl.AutoCompleteCustomSource.AddRange(
                                items.Cast<ItemWrapper<object>>().Select(i => i.DisplayText).ToArray());
                        else
                            DropDownControl.AutoCompleteCustomSource.AddRange(
                                items.Select(i => i.ToString()).ToArray());
#endif

                        DropDownControl.Items.AddRange(items);
                    }
                }
            }

            return base.EditValue(context, provider, value);
        }

        /// <summary>
        /// Resolves the values.
        /// </summary>
        /// <param name="setup">The setup.</param>
        /// <param name="propDesc">The property desc.</param>
        /// <returns></returns>
        private static string[] ResolveValues(AutoCompleteSetupAttribute setup, PropertyDescriptor propDesc)
        {
            // If the AutoCompleteSetupAttribute has Values set, use them directly
            if (setup.Values?.Length > 0)
                return setup.Values;

            // If the ProviderType is set, try to get the static string[] Values property
            var type = setup.ProviderType ?? propDesc.PropertyType;

            if (type.IsEnum)
                return Enum.GetNames(type);

            const string propName = "Values";
            var prop = type.GetProperty(propName, BindingFlags.Public | BindingFlags.Static);
#if NET35
            return (string[])prop.GetValue(null, null);
#else
            return (string[])prop.GetValue(null);
#endif
        }

        /// <summary>
        /// Validates the setup.
        /// </summary>
        /// <param name="setup">The setup.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">setup - AutoCompleteSetupAttribute must be assigned to the property where AutoCompleteComboBoxEditor is used.</exception>
        /// <exception cref="InvalidOperationException">
        /// At least one value must be specified when using AutoCompleteSourceMode.Values.
        /// or
        /// ProviderType could not be determined.
        /// or
        /// The enum '{providerType.Name}' does not define any members.
        /// or
        /// The type '{providerType.FullName}' must define a public static property named '{propName}'.
        /// or
        /// The '{propName}' property on '{providerType.FullName}' must be of type string[].
        /// or
        /// The '{propName}' property on '{providerType.FullName}' returned no items.
        /// </exception>
        private static void ValidateSetup(AutoCompleteSetupAttribute setup, ITypeDescriptorContext context)
        {
            // If somehow setup is null, add the code in here to throw an exception.  Currently this routine 
            // is called after a check to see if the setup is null, so this should never happen.
            if (setup.AutoCompleteSource == AutoCompleteSource.CustomSource)
                switch (setup.Mode)
                {
                    case AutoCompleteSetupAttribute.SourceMode.Values:
                        if (setup.Values == null || setup.Values.Length == 0)
                            throw new InvalidOperationException("At least one value must be specified when using AutoCompleteSetupAttribute with a value list.");
                        break;

                    case AutoCompleteSetupAttribute.SourceMode.Provider:
                        var providerType = setup.ProviderType ?? context.PropertyDescriptor.PropertyType;
                        if (providerType.IsEnum)
                        {
                            var names = Enum.GetNames(providerType);
                            if (names.Length == 0)
                                throw new InvalidOperationException($"The enum '{providerType.Name}' does not define any members.");
                        }
                        else
                        {
                            const string propName = "Values";
                            var prop = providerType.GetProperty(propName, BindingFlags.Public | BindingFlags.Static)
                                ?? throw new InvalidOperationException($"The type '{providerType.FullName}' must define a public static property named '{propName}'.");

                            if (prop.PropertyType != typeof(string[]))
                                throw new InvalidOperationException($"The '{propName}' property on '{providerType.FullName}' must be of type string[].");

#if NET35
                            var values = (string[])prop.GetValue(null, null);
#else
                            var values = (string[])prop.GetValue(null);
#endif
                            if (values == null || values.Length == 0)
                                throw new InvalidOperationException($"The '{propName}' property on '{providerType.FullName}' returned no items.");
                        }

                        break;
                }
        }
    }
}
