using System;
using System.Windows.Forms;

namespace SampleForms
{
    /// <summary>
    /// 
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if NET6_0_OR_GREATER
            using var frm = new Form1();
#else
            using (var frm = new Form1())
#endif
            {
                Application.Run(frm);
            }
        }
    }
}
