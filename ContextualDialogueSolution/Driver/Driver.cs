//namespaces
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ContextualDialogue.DialogueGenerator;
using ContextualDialogue.WorldManager;

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
