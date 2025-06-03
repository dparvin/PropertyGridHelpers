using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.Controls;
using System;
using System.ComponentModel;
using System.Drawing.Design;
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
                        DropDownControl.AutoCompleteCustomSource.AddRange(sourceAttr.Values);
                    }
                }
            }

            return base.EditValue(context, provider, value);
        }
    }
}
