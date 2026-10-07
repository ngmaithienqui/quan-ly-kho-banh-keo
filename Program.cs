using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Uncomment one of these lines to test the form you want to display
           Application.Run(new FTrangChuNhanVien());  // Show owner home page
           // Application.Run(new FTrangchuQuanlykho());  // Show inventory management home page
        }
    }
}
