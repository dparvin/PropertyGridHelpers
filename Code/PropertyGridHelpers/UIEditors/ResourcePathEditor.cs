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
        /// Initializes a new instance of the <see cref="ResourcePathEditor"/> class,
        /// using the appropriate resource base name extractor for the current target framework.
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
        /// Initializes a new instance of the <see cref="ResourcePathEditor"/> class with a custom 
        /// resource base name extractor.
        /// </summary>
        /// <param name="extractor">
        /// The extractor used to identify resource base names in the assembly. Must not be <c>null</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="extractor"/> is <c>null</c>.
        /// </exception>
        private ResourcePathEditor(IResourceBaseNameExtractor extractor) =>
            _extractor = extractor;

        /// <summary>
        /// Edits the specified value using a dropdown list of resource base names.
        /// </summary>
        /// <param name="context">
        /// The editing context, typically provided by the PropertyGrid. May be <c>null</c>.
        /// </param>
        /// <param name="provider">
        /// A service provider that can supply an <see cref="IWindowsFormsEditorService"/> for managing
        /// the dropdown UI. May be <c>null</c>.
        /// </param>
        /// <param name="value">
        /// The current property value. May be updated by the user’s selection.
        /// </param>
        /// <returns>
        /// The edited value, or the original value if editing was canceled or no selection was made.
        /// </returns>
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
        /// Returns the editing style of the editor, which is <see cref="UITypeEditorEditStyle.DropDown"/>.
        /// </summary>
        /// <param name="context">The editing context.</param>
        /// <returns>
        /// Always <see cref="UITypeEditorEditStyle.DropDown"/>.
        /// </returns>
        public override UITypeEditorEditStyle GetEditStyle(
            ITypeDescriptorContext context) => UITypeEditorEditStyle.DropDown;

        /// <summary>
        /// Indicates whether this editor supports painting a visual representation of a value.
        /// </summary>
        /// <param name="context">The editing context.</param>
        /// <returns>
        /// Always <c>false</c>, since this editor does not support paint previews.
        /// </returns>
        public override bool GetPaintValueSupported(
            ITypeDescriptorContext context) => false;

        /// <summary>
        /// Gets a value indicating whether the dropdown UI is resizable.
        /// </summary>
        /// <value>
        /// Always <c>false</c>.
        /// </value>
        public override bool IsDropDownResizable => false;

        /// <summary>
        /// Creates the list box control that displays resource base names for selection.
        /// </summary>
        /// <param name="baseNames">
        /// The list of resource base names to display.
        /// </param>
        /// <param name="allowBlank">
        /// If <c>true</c>, includes a blank option at the top of the list.
        /// </param>
        /// <param name="blankLabel">
        /// The label text to use for the blank selection.
        /// </param>
        /// <param name="value">
        /// The currently selected value, which will be preselected in the list if present.
        /// </param>
        /// <returns>
        /// A configured <see cref="ListBox"/> control containing the resource names.
        /// </returns>
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