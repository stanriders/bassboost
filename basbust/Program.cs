using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Un4seen.Bass;

namespace basbust
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
            Application.Run(new mainForm());

            // close bass
            Bass.BASS_Stop();
            Bass.BASS_Free();
        }
    }
}
