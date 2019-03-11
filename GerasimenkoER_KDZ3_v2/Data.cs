using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerasimenkoER_KDZ3_v2
{
    class Data
    {
        public bool flagmb = false;
        public string name = "";
        public List<List<string>> data = null;
        public string[] datas = null;
        public char separ = ';';
        public bool rewrite = false;
        public bool issaved = true;
        public bool isaded = false;
        public bool sne = false, ene = false;
        public Encoding encode = Encoding.Default;
        public int sn = 1;
        public int en = -1;
    }
}
