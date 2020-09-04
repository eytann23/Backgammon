using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgammonWorld
{
    static class BackgammonApp
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.ThreadException += myGlobalExceptionHandler;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BackgammonGameUI());
        }
        static void myGlobalExceptionHandler(object sender,ThreadExceptionEventArgs args)
        { }
    }
}
