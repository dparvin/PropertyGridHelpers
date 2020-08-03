using PropertyGridHelpers.Converters;
using PropertyGridHelpers.UIEditors;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace SampleControls
{
    public partial class TestControl : UserControl
    {
        public TestControl()
        {
            InitializeComponent();
        }

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
        [Category("Test Items")]
        [Description("Test Enum to show the possibilities of a Flag Type Enum")]
        [DisplayName("Test Enum")]
        [Editor(typeof(FlagEnumUIEditor<EnumTextConverter<TestEnums>>), typeof(UITypeEditor))]
        [TypeConverter(typeof(EnumTextConverter<TestEnums>))]
        public TestEnums Test { get; set; }
    }
}
