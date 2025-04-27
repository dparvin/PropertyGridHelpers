using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.Converters;
using PropertyGridHelpers.Support;
using PropertyGridHelpers.TypeDescriptionProviders;
using PropertyGridHelpers.TypeDescriptors;
using PropertyGridHelpers.UIEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;

namespace SampleControls
{
    /// <summary>
    /// Control used to test the functionality of the PropertyGridHelpers Library
    /// </summary>
    /// <seealso cref="UserControl" />
    [ResourcePath(nameof(TestControl))]
    [TypeDescriptionProvider(typeof(LocalizedTypeDescriptionProvider))]
    public partial class TestControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestControl"/> class.
        /// </summary>
        public TestControl()
        {
            InitializeComponent();
            Scrollbars = ScrollBars.Both;
            Scrollbars = ScrollBars.None;
            lbl.Text = $"The main property to test here is the ScrollBars Property. There are other properties in the that use features of the PropertyGridHelpers in the Test Items group.\n{Assembly.GetAssembly(typeof(TestControl)).Location} - {Assembly.GetAssembly(typeof(TestControl)).ImageRuntimeVersion}";
            lbl.TextAlign = ContentAlignment.MiddleCenter;
        }

        private ScrollBars _Scrollbars = ScrollBars.None;
        private ImageTypes _ImageTypes = ImageTypes.None;
        private ImageFileExtension _FileExtension = ImageFileExtension.png;

        /// <summary>
        /// Gets or sets the test.
        /// </summary>
        /// <value>
        /// The test.
        /// </value>
        /// <remarks>
        /// The <see cref="EditorAttribute"/> is used to setup the drop-down on the grid to 
        /// display the data the way the programmer intends the Enum to be represented.  The 
        /// <see cref="TypeConverterAttribute"/> is used to set the text in the grid in a 
        /// normal basis.
        /// </remarks>
        [LocalizedCategory("Category_Layout")]
        [LocalizedDescription("Description_Scrollbar")]
        [LocalizedDisplayName("DisplayName_Scrollbar")]
        [Editor(typeof(FlagEnumUIEditor<EnumTextConverter<ScrollBars>>), typeof(UITypeEditor))]
        [TypeConverter(typeof(EnumTextConverter<ScrollBars>))]
        [DefaultValue(ScrollBars.None)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        public ScrollBars Scrollbars
        {
            get => _Scrollbars;
            set
            {
                if (_Scrollbars != value)
                {
                    SuspendLayout();
                    switch (value)
                    {
                        case ScrollBars.None:
                            vs.Visible = false;
                            hs.Visible = false;
                            vshsPanel.Visible = false;
                            break;
                        case ScrollBars.Vertical:
                            hs.Visible = false;
                            vshsPanel.Visible = false;
                            vs.Visible = true;
                            break;
                        case ScrollBars.Horizontal:
                            vs.Visible = false;
                            vshsPanel.Visible = false;
                            hs.Visible = true;
                            break;
                        case ScrollBars.Both:
                            vs.Visible = true;
                            hs.Visible = true;
                            vshsPanel.Visible = true;
                            break;
                        default:
                            break;
                    }

                    ResumeLayout(false);
                }
                // Ensure design-time updates
                if (DesignMode)
                {
                    Invalidate();
                    Update();
                }

                _Scrollbars = value;
            }
        }

        /// <summary>
        /// Gets or sets the image types.
        /// </summary>
        /// <value>
        /// The image types.
        /// </value>
        [LocalizedCategory("Category_Layout")]
        [LocalizedDescription("Description_ImageToDisplay")]
        [LocalizedDisplayName("DisplayName_ImageToDisplay")]
        [Editor(typeof(ImageTextUIEditor<ImageTypes>), typeof(UITypeEditor))]
        [ResourcePath("Properties.Resources")]
        [TypeConverter(typeof(EnumTextConverter<ImageTypes>))]
        [FileExtension(nameof(FileExtension))]
        [DefaultValue(ImageTypes.None)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        public ImageTypes ImageTypes
        {
            get => _ImageTypes;
            set
            {
                if (_ImageTypes == value) return; // Avoid unnecessary updates
                _ImageTypes = value;
                UpdateImage(); // Call the shared update routine
            }
        }

        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        /// <value>
        /// The file extension.
        /// </value>
        [LocalizedCategory("Category_Layout")]
        [LocalizedDescription("Description_FileExtension")]
        [LocalizedDisplayName("DisplayName_FileExtension")]
        [TypeConverter(typeof(EnumTextConverter<ImageFileExtension>))]
        [DefaultValue(ImageFileExtension.None)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        public ImageFileExtension FileExtension
        {
            get => _FileExtension;
            set
            {
                if (_FileExtension == value) return; // Avoid unnecessary updates
                _FileExtension = value;
                UpdateImage(); // Call the shared update routine
            }
        }

        /// <summary>
        /// Gets or sets the test date.
        /// </summary>
        /// <value>
        /// The test date.
        /// </value>
        [LocalizedCategory("Category_TestItems")]
        [LocalizedDescription("Description_DateTime")]
        [LocalizedDisplayName("DisplayName_DateTime")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DateTime TestDate
        {
            get; set;
        }

        /// <summary>Gets or sets the strings.</summary>
        /// <value>The strings.</value>
        /// <remarks>
        /// The <see cref="EditorAttribute" /> is used to setup the drop down on the grid to 
        /// display the data the way the programmer intends the Enum to be represented.  
        /// The <see cref="TypeConverterAttribute" /> is used to set the text in the grid in 
        /// a normal basis.
        /// </remarks>
        [LocalizedCategory("Category_TestItems")]
        [LocalizedDescription("Discription_Strings")]
        [LocalizedDisplayName("DisplayName_Strings")]
        [Editor(typeof(CollectionUIEditor<string>), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<string> Strings { get; set; } = new List<string>();

        /// <summary>Gets or sets the strings.</summary>
        /// <value>The strings.</value>
        /// <remarks>
        /// The <see cref="EditorAttribute" /> is used to setup the drop down on the grid to 
        /// display the data the way the programmer intends the Enum to be represented.  
        /// The <see cref="TypeConverterAttribute" /> is used to set the text in the grid in 
        /// a normal basis.
        /// </remarks>
        [LocalizedCategory("Category_TestItems")]
        [LocalizedDescription("Description_Decimals")]
        [LocalizedDisplayName("DisplayName_Decimals")]
        [Editor(typeof(CollectionUIEditor<decimal>), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<decimal> Decimals { get; set; } = new List<decimal>();

        /// <summary>Gets or sets the strings.</summary>
        /// <value>The strings.</value>
        /// <remarks>
        /// The <see cref="EditorAttribute" /> is used to setup the drop down on the grid to 
        /// display the data the way the programmer intends the Enum to be represented.  
        /// The <see cref="TypeConverterAttribute" /> is used to set the text in the grid in 
        /// a normal basis.
        /// </remarks>
        [LocalizedCategory("Category_TestItems")]
        [LocalizedDescription("Description_Sizes")]
        [LocalizedDisplayName("DisplayName_Sizes")]
        [Editor(typeof(CollectionUIEditor<Size>), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<Size> Sizes { get; set; } = new List<Size>();

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        /// <value>
        /// A <see cref="Color"/> that represents the background color of the control. The default 
        /// is the value of the <see cref="Control.DefaultBackColor"/> property.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public override Color BackColor
        {
            get => base.BackColor;

            set
            {
                base.BackColor = value;
                lbl.BackColor = value;
            }
        }

        /// <summary>Gets or sets the foreground color of the control.</summary>
        /// <value>
        /// The foreground <see cref="Color"/> of the control. The default is the value of the 
        /// <see cref="Control.DefaultForeColor"/> property.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                lbl.ForeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the resource path.
        /// </summary>
        /// <value>
        /// The resource path.
        /// </value>
        [AllowBlank(includeItem: true, resourceItem: "Blank_ResourcePath")]
        [LocalizedCategory("Category_TestItems")]
        [LocalizedDescription("Description_ResourcePath")]
        [LocalizedDisplayName("DisplayName_ResourcePath")]
        [Editor(typeof(ResourcePathEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(OnlySelectableTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string ResourcePath
        {
            get; set;
        }

        #region Support Code ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        /// <summary>
        /// Updates the image.
        /// </summary>
        private void UpdateImage()
        {
            if (ImageTypes == ImageTypes.None)
            {
                pictureBox1.Visible = false;
                pictureBox1.Image = null;
                return;
            }

            var propertyDescriptor = TypeDescriptor.GetProperties(this)[nameof(ImageTypes)];
            var context = new CustomTypeDescriptorContext(propertyDescriptor, this);
            var resourcePath = Support.GetResourcePath(context, ImageTypes.GetType());
            var bounds = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);

            pictureBox1.Image = ImageTextUIEditor.GetImageFromResource(
                ImageTypes,
                ImageTypes.GetType(),
                resourcePath,
                FileExtension == ImageFileExtension.None ? "" : FileExtension.ToString(),
                bounds);

            pictureBox1.Visible = true;

            if (DesignMode)
            {
                pictureBox1.Invalidate(); // Redraw for design-time updates
            }
        }

        #endregion
    }
}
