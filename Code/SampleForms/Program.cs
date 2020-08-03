using System;
using System.Windows.Forms;

namespace SampleForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var frm = new Form1())
                Application.Run(frm);
        }
    }
}
