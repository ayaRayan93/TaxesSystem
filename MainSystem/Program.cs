using TaxesSystem;
using TaxesSystem.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows.Forms;

namespace TaxesSystem
{
    static class Program
    {
        /// <summary>
        /// The TaxesSystem entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
