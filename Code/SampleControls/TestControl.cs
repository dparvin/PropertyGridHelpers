using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.Converters;
using PropertyGridHelpers.UIEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace SampleControls
{
    /// <summary>
    /// Control used to test the functionality of the PropertyGridHelpers Library
    /// </summary>
    /// <seealso cref="UserControl" />
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
            lbl.Text = "The main property to test here is the ScrollBars Property. There are other properties in the that use features of the PropertyGridHelpers in the Test Items group.\n" + Assembly.GetAssembly(typeof(TestControl)).Location + " - " + Assembly.GetAssembly(typeof(TestControl)).ImageRuntimeVersion;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
        }

        private ScrollBars _Scrollbars = ScrollBars.None;
        private ImageTypes _ImageTypes = ImageTypes.None;

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
        [Category("Layout")]
        [Description("Scrollbars to show for the user")]
        [DisplayName("Scroll Bars")]
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
                            lbl.Width = Width - lbl.Left * 2;
                            lbl.Height = Height - lbl.Top * 2;
                            break;
                        case ScrollBars.Vertical:
                            hs.Visible = false;
                            vshsPanel.Visible = false;
                            vs.Visible = true;
                            vs.Height = Height;
                            vs.Top = 0;
                            vs.Width = vshsPanel.Width;
                            vs.Left = Width - vshsPanel.Width;
                            lbl.Width = vs.Left - lbl.Left * 2;
                            lbl.Height = Height - lbl.Top * 2;
                            break;
                        case ScrollBars.Horizontal:
                            vs.Visible = false;
                            vshsPanel.Visible = false;
                            hs.Visible = true;
                            hs.Width = Width;
                            hs.Left = 0;
                            hs.Height = vshsPanel.Height;
                            hs.Top = Height - vshsPanel.Height;
                            lbl.Width = Width - lbl.Left * 2;
                            lbl.Height = hs.Top - lbl.Top * 2;
                            break;
                        case ScrollBars.Both:
                            vs.Visible = true;
                            vs.Height = Height - vshsPanel.Height;
                            vs.Top = 0;
                            vs.Width = vshsPanel.Width;
                            vs.Left = Width - vshsPanel.Width;
                            hs.Visible = true;
                            hs.Width = Width - vshsPanel.Width;
                            hs.Left = 0;
                            hs.Height = vshsPanel.Height;
                            hs.Top = Height - vshsPanel.Height;
                            vshsPanel.Visible = true;
                            vshsPanel.Left = Width - vshsPanel.Width;
                            vshsPanel.Top = Height - vshsPanel.Height;
                            lbl.Width = vs.Left - lbl.Left * 2;
                            lbl.Height = hs.Top - lbl.Top * 2;
                            break;
                        default:
                            break;
                    }
                    ResumeLayout(false);
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
        [Category("Layout")]
        [Description("Image to Add to control")]
        [DisplayName("Image To Display")]
        [Editor(typeof(ImageTextUIEditor<ImageTypes>), typeof(UITypeEditor))]
        [TypeConverter(typeof(EnumTextConverter<ImageTypes>))]
        [DefaultValue(ImageTypes.None)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        public ImageTypes ImageTypes
        {
            get => _ImageTypes;
            set
            {
                _ImageTypes = value;
                if (value == ImageTypes.None)
                {
                    pictureBox1.Visible = false;
                }
                else
                {
                    pictureBox1.Image = GetImage(_ImageTypes);
                    pictureBox1.Visible = true;
                }
            }
        }

        /// <summary>
        /// Get the image to show in the control
        /// </summary>
        /// <param name="imageTypes">Enum entry to select the image</param>
        /// <returns></returns>
        private static Image GetImage(ImageTypes imageTypes)
        {
            Type _enumType = typeof(ImageTypes);
            var fi = _enumType.GetField(Enum.GetName(typeof(ImageTypes), imageTypes));
            EnumImageAttribute dna =
                    (EnumImageAttribute)Attribute.GetCustomAttribute(
                    fi, typeof(EnumImageAttribute));

            if (dna != null)
            {
                string m = imageTypes.GetType().Module.Name;
#if NET5_0_OR_GREATER
                m = m[0..^4];
#else
                m = m.Substring(0, m.Length - 4);
#endif
                var rm = new ResourceManager(
                    m + ".Properties.Resources", imageTypes.GetType().Assembly);

                // Draw the image
                Bitmap newImage = (Bitmap)rm.GetObject(dna.EnumImage);
                newImage.MakeTransparent();
                return newImage;
            }
            return null;
        }

        /// <summary>
        /// Gets or sets the test date.
        /// </summary>
        /// <value>
        /// The test date.
        /// </value>
        [Category("Test Items")]
        [Description("A test of having a DateTime field.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DateTime TestDate { get; set; }

        /// <summary>Gets or sets the strings.</summary>
        /// <value>The strings.</value>
        /// <remarks>
        /// The <see cref="EditorAttribute" /> is used to setup the drop down on the grid to 
        /// display the data the way the programmer intends the Enum to be represented.  
        /// The <see cref="TypeConverterAttribute" /> is used to set the text in the grid in 
        /// a normal basis.
        /// </remarks>
        [Editor(typeof(CollectionUIEditor<string>), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Test Items")]
        [Description("A test of processing a list of strings")]
        [DisplayName("Strings")]
        public List<string> Strings { get; set; } = new List<string>();

        /// <summary>Gets or sets the strings.</summary>
        /// <value>The strings.</value>
        /// <remarks>
        /// The <see cref="EditorAttribute" /> is used to setup the drop down on the grid to 
        /// display the data the way the programmer intends the Enum to be represented.  
        /// The <see cref="TypeConverterAttribute" /> is used to set the text in the grid in 
        /// a normal basis.
        /// </remarks>
        [Editor(typeof(CollectionUIEditor<decimal>), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Test Items")]
        [Description("A test of processing a list of decimals")]
        [DisplayName("Decimals")]
        public List<decimal> Decimals { get; set; } = new List<decimal>();

        /// <summary>Gets or sets the strings.</summary>
        /// <value>The strings.</value>
        /// <remarks>
        /// The <see cref="EditorAttribute" /> is used to setup the drop down on the grid to 
        /// display the data the way the programmer intends the Enum to be represented.  
        /// The <see cref="TypeConverterAttribute" /> is used to set the text in the grid in 
        /// a normal basis.
        /// </remarks>
        [Editor(typeof(CollectionUIEditor<Size>), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Test Items")]
        [Description("A test of processing a list of sizes")]
        [DisplayName("Sizes")]
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
    }
}
