using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GerasimenkoER_KDZ3_v2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            start: bool flag = false;
            try
            {
                flag = false;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());

            }
            catch(Exception ex) { flag = true; }
            finally { }
            if (flag) { goto start; }
            //Application.Run(new Find());
        }
    }
}
