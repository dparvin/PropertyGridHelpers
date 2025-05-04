using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.Converters;
using PropertyGridHelpers.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PropertyGridHelpers.UIEditors
{
    /// <summary>
    /// Provides a <see cref="UITypeEditor"/> that enables selection of embedded resource paths
    /// from a dropdown list in the PropertyGrid.
    /// </summary>
    /// <remarks>
    /// This editor dynamically retrieves available resource paths from the owning assembly and
    /// presents them as selectable values. It is commonly used in conjunction with the
    /// <see cref="OnlySelectableTypeConverter"/> to restrict editing to valid, predefined options
    /// and to prevent users from manually typing arbitrary values.
    /// </remarks>
    /// <example>
    /// <code language="csharp">
    /// [AllowBlank(includeItem: true, resourceItem: "Blank_ResourcePath")]
    /// [LocalizedCategory("Category_TestItems")]
    /// [LocalizedDescription("Description_ResourcePath")]
    /// [LocalizedDisplayName("DisplayName_ResourcePath")]
    /// [Editor(typeof(ResourcePathEditor), typeof(UITypeEditor))]
    /// [TypeConverter(typeof(OnlySelectableTypeConverter))]
    /// public string ResourcePath { get; set; }
    /// </code>
    /// </example>
    /// <seealso cref="UITypeEditor"/>
    /// <seealso cref="OnlySelectableTypeConverter"/>
    /// <seealso cref="AllowBlankAttribute"/>
    public class ResourcePathEditor : UITypeEditor
    {
        private readonly IResourceBaseNameExtractor _extractor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePathEditor"/> class.
        /// </summary>
        public ResourcePathEditor() : this(
#if NET8_0_OR_GREATER
            new RangeBasedBaseNameExtractor()
#else
            new SubstringBasedBaseNameExtractor()
#endif
        )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePathEditor"/> class.
        /// </summary>
        /// <param name="extractor">The extractor.</param>
        /// <exception cref="System.ArgumentNullException">extractor</exception>
        private ResourcePathEditor(IResourceBaseNameExtractor extractor) =>
            _extractor = extractor ?? throw new ArgumentNullException(nameof(extractor));

        /// <summary>
        /// Edits the value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override object EditValue(
            ITypeDescriptorContext context,
            IServiceProvider provider,
            object value)
        {
            var newValue = value;
            if (context != null &&
                context.Instance != null &&
                provider != null)
            {
                if ((provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService edSvc))
                {
                    var assembly = context.Instance.GetType().Assembly;

                    // Get the embedded resource names
                    var baseNames = _extractor.ExtractBaseNames(assembly.GetName().Name, assembly.GetManifestResourceNames());

                    if (baseNames.Count > 0)
                    {
                        // Build dropdown
                        var allowBlank = AllowBlankAttribute.IsBlankAllowed(context);
                        var blankLabel = allowBlank ? AllowBlankAttribute.GetBlankLabel(context) : String.Empty;
                        var ResourceListBox = CreateListBox(baseNames, allowBlank, blankLabel, newValue);

                        ResourceListBox.SelectedIndexChanged += (s, e) => edSvc.CloseDropDown();

                        edSvc.DropDownControl(ResourceListBox);

                        if (ResourceListBox.SelectedItem is string selectedItem)
                            newValue = allowBlank && string.Equals(selectedItem, blankLabel, StringComparison.Ordinal) ? string.Empty : (object)selectedItem;
                    }
                }
            }

            return newValue;
        }

        /// <summary>
        /// Gets the edit style.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(
            ITypeDescriptorContext context) => UITypeEditorEditStyle.DropDown;

        /// <summary>
        /// Gets the paint value supported.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override bool GetPaintValueSupported(
            ITypeDescriptorContext context) => false;

        /// <summary>
        /// Gets a value indicating whether this instance is drop down resizable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is drop down resizable; otherwise, <c>false</c>.
        /// </value>
        public override bool IsDropDownResizable => false;

        /// <summary>
        /// Creates the ListBox.
        /// </summary>
        /// <param name="baseNames">The base names.</param>
        /// <param name="allowBlank">if set to <c>true</c> [allow blank].</param>
        /// <param name="blankLabel">The blank label.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static ListBox CreateListBox(
            IList<string> baseNames,
            bool allowBlank,
            string blankLabel,
            object value)
        {
            var listBox = new ListBox
            {
                BorderStyle = BorderStyle.None,
                SelectionMode = SelectionMode.One,
                IntegralHeight = false,
                Height = Math.Min(200, baseNames.Count * 16)
            };

            listBox.BeginUpdate();
            if (allowBlank)
                _ = listBox.Items.Add(blankLabel);
            foreach (var name in baseNames)
                _ = listBox.Items.Add(name);

            if (value is string selected && listBox.Items.Contains(selected))
                listBox.SelectedItem = selected;
            else if (listBox.Items.Count > 0)
                listBox.SelectedIndex = 0;

            listBox.EndUpdate();
            return listBox;
        }
    }
}