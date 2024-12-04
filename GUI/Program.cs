using System;
using System.Windows.Forms;
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
            Application.Run(new frm_Container());
        }
    }
}
