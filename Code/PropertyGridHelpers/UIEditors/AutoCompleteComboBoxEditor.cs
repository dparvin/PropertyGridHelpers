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
    /// Provides a <see cref="UITypeEditor"/> that displays an <see cref="AutoCompleteComboBox"/>
    /// in a drop-down editor within the <see cref="PropertyGrid"/>.
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
        /// Gets or sets the converter.
        /// </summary>
        /// <value>
        /// The converter.
        /// </value>
        public EnumConverter Converter
        {
            get; set;
        }

        /// <summary>
        /// Displays the editor control in a dropdown and returns the updated
        /// value after editing completes.
        /// </summary>
        /// <param name="context">Provides context information about the design-time environment.</param>
        /// <param name="provider">A service provider that can provide an <see cref="IWindowsFormsEditorService"/>.</param>
        /// <param name="value">The current value of the property being edited.</param>
        /// <returns>
        /// The edited value as returned by the control, or the original value
        /// if editing is canceled or fails.
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
                    if (!sourceAttr.IsValid)
                        throw new InvalidOperationException(
                            $"The AutoCompleteSetupAttribute on property '{context.PropertyDescriptor.Name}' could not be initialized.",
                            sourceAttr.InitializationException);
                    DropDownControl.AutoCompleteMode = sourceAttr.AutoCompleteMode;
                    DropDownControl.AutoCompleteSource = sourceAttr.AutoCompleteSource;
                    DropDownControl.DropDownStyle = sourceAttr.DropDownStyle;

                    if (sourceAttr.AutoCompleteSource == AutoCompleteSource.CustomSource)
                    {
                        DropDownControl.AutoCompleteCustomSource.Clear();
                        DropDownControl.Items.Clear();

                        object[] items;

                        if (Converter != null && sourceAttr.ProviderType.IsEnum)
                        {
#if NET5_0_OR_GREATER
                            items = [.. Enum.GetValues(sourceAttr.ProviderType)
                                .Cast<object>()
                                .Select(e => new ItemWrapper<object>(
                                    Converter.ConvertToString(context, CultureInfo.CurrentCulture, e), e))];
#else
                            items = Enum.GetValues(sourceAttr.ProviderType)
                                .Cast<object>()
                                .Select(e => new ItemWrapper<object>(
                                    Converter.ConvertToString(context, CultureInfo.CurrentCulture, e), e))
                                .ToArray();
#endif
                        }
                        else
                        {
                            items = ResolveValues(sourceAttr, context.PropertyDescriptor);
                        }

#if NET5_0_OR_GREATER
                        if (items.Length > 0 && items[0] is ItemWrapper<object>)
                            DropDownControl.AutoCompleteCustomSource.AddRange(
                                [.. items.Cast<ItemWrapper<object>>().Select(i => i.DisplayText)]);
                        else
                            DropDownControl.AutoCompleteCustomSource.AddRange(
                                [.. items.Select(i => i.ToString())]);
#else
                        if (items.Length > 0 && items[0] is ItemWrapper<object>)
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

        private static string[] ResolveValues(AutoCompleteSetupAttribute setup, PropertyDescriptor propDesc)
        {
            // If the AutoCompleteSetupAttribute has Values set, use them directly
            if (setup.Values?.Length > 0)
                return setup.Values;

            // If the ProviderType is set, try to get the static string[] Values property
            var type = setup.ProviderType ?? propDesc?.PropertyType;
            if (type == null)
#if NET35 || NET452
                return new string[0];
#elif NET5_0_OR_GREATER
                return [];
#else
                return Array.Empty<string>();
#endif
            // 
            const string propName = "Values";
            var prop = type.GetProperty(propName, BindingFlags.Public | BindingFlags.Static);
            if (prop != null && prop.PropertyType == typeof(string[]))
            {
#if NET35
                return (string[])prop.GetValue(null, null);
#else
                return (string[])prop.GetValue(null);
#endif
            }

#if NET35 || NET452
            return new string[0];
#elif NET5_0_OR_GREATER
            return [];
#else
                return Array.Empty<string>();
#endif
        }
    }
}
