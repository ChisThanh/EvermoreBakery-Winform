using GUI.Auth;
using GUI.AccessManagement;
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
        public static user userAuth = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new GUI.Bills.Frm_Bills());
            //Application.Run(new GUI.ProductReview.Frm_ProductReview());
            //Application.Run(new Frm_AddPermission());
            Application.Run(new Frm_AddPermissionRole());
            //Application.Run(new Main());
        }
    }
}
