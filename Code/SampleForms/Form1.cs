using System.Windows.Forms;

namespace SampleForms
{
    /// <summary>
    /// 
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

            propertyGrid1.SelectedObject = testControl1;
        }
    }
}
