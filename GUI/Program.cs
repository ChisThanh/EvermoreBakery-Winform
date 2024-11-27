using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.Bills;
using BLL;
using DTO;

namespace GUI
{
    internal static class Program
    {
        public static user userAuth;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frm_Container());
            Application.Run(new GUI.Sales.frm_Event());
        }
    }
}
