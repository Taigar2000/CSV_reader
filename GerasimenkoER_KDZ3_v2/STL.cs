using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerasimenkoER_KDZ3_v2
{
    class STL
    {
    }

    class pair<T1, T2>
    {
        public T1 first;
        public T2 second;
        //public void setfirst(T1 w)
        //{
        //    first= w;
        //}
        //public void setsecond(T2 w)
        //{
        //    second = w;
        //}

    }

    //struct pair
    //{
    //    public int first;
    //    public string second;
    //    public void setsecond(string w)
    //    {
    //        second = w;
    //    }
    //}

    struct vector<T>
    {
        List<T> e;
        int length;
        public void fill(List<T> c)
        {
            e = c;
            length = c.Count;
        }
        public int size()
        {
            return length;
        }
        public T this [int n]
        {
            get
            {
                return e[n];
            }
            set
            {
                e[n] = value;
            }
        }
        public void append(T v)
        {
            if (e == null) { e = new List<T>(); }
            e.Add(v);
            ++length;
        }
        public void remove(int n)
        {
            e.RemoveAt(n);
            --length;
        }
        public void reverse()
        {
            e.Reverse();
        }
        public string tostring(string separator = "")
        {
            string s = "";
            for(int i = 0; i < length; ++i)
            {
                s += e[i] + separator;
            }
            return s;
        }
    }

}
