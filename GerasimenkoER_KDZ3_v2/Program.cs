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
            start: bool flag = false;
            string s = "";
            try
            {
                flag = false;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(s));

            }
            catch (Exception ex) { flag = true; s = "Возникла ошибка связанная с попыткой вашей операционной системы принудительно закрыть данную программу.\nПрограмма вступила в неравный бой с системой, но не справилась с партией и была отправлена на уничтожение.\nОна долго ждала своей миллисекунды и наконец Программа смогла победить и была перезапущена\n" + ex.Message; }
            finally { }
            if (flag) { goto start; }
            //Application.Run(new Find());
        }
    }
}
