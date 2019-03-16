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
            //Комменты, так как иначе она не убиваема (в самом теле также потёр все Catche для эксперимента)
            //start: bool flag = false;
            string s = "";
            //try
            //{
            //    flag = false;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(s));

            //}
            //catch(Exception ex) { flag = true; }
            //finally { }
            //if (flag) { s="Возникла ошибка связанная с попыткой вашей операционной системы закрыть данную программу.\nПрограмма была перезапущена";goto start; }
            //Application.Run(new Find());
        }
    }
}
