//namespaces
using System;
using System.Windows.Forms;

namespace DriverNamespace
{
    static class Driver
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DriverForm());
        }
    }
}
