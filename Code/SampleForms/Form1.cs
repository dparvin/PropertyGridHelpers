using System.Windows.Forms;

namespace SampleForms
{
    /// <summary>
    /// form for testing the <see cref="PropertyGridHelpers"/> library
    /// </summary>
    /// <seealso cref="Form" />
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            //Support.SetLanguage("fr");

            propertyGrid1.SelectedObject = testControl1;
        }

        private void Form1_Resize(object sender, System.EventArgs e) => propertyGrid1.Refresh();
    }
}
