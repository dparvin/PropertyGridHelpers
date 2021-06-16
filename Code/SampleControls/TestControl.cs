using PropertyGridHelpers.Converters;
using PropertyGridHelpers.UIEditors;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;

namespace SampleControls
{
    public partial class TestControl : UserControl
    {
        public TestControl()
        {
            InitializeComponent();
            Scrollbars = ScrollBars.Both;
            Scrollbars = ScrollBars.None;
            lbl.Text = Assembly.GetAssembly(typeof(TestControl)).Location + " - " + Assembly.GetAssembly(typeof(TestControl)).ImageRuntimeVersion;
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        }

        ScrollBars _Scrollbars = ScrollBars.None;

        /// <summary>
        /// Gets or sets the test.
        /// </summary>
        /// <value>
        /// The test.
        /// </value>
        /// <remarks>
        /// The <see cref="EditorAttribute"/> is used to setup the drop-down on the grid to display the data the way the
        /// programmer intends the Enum to be represented.  The <see cref="TypeConverterAttribute"/>
        /// is used to set the text in the grid in a normal basis.
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
            get
            {
                return _Scrollbars;
            }
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
        /// Gets or sets the strings.
        /// </summary>
        /// <value>
        /// The strings.
        /// </value>
        /// <summary>
        /// Gets or sets the test.
        /// </summary>
        /// <value>
        /// The test.
        /// </value>
        /// <remarks>
        /// The <see cref="EditorAttribute"/> is used to setup the dropdown on the grid to display the data the way the
        /// programmer intends the Enum to be represented.  The <see cref="TypeConverterAttribute"/>
        /// is used to set the text in the grid in a normal basis.
        /// </remarks>
        //[Editor(typeof(CollectionUIEditor<string>), typeof(UITypeEditor))]
        [Category("Test Items")]
        [Description("A test of processing a list of strings")]
        [DisplayName("Strings")]
        public List<string> Strings { get; }

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        /// <value>
        /// A <see cref="Color"/> that represents the background color of the control. The default is the value of the <see cref="Control.DefaultBackColor"/> property.
        /// </value>
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
        /// The foreground <see cref="Color"/> of the control. The default is the value of the <see cref="Control.DefaultForeColor"/> property.
        /// </value>
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
