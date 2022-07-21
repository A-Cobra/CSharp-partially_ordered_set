using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matematica_Discreta
{
    class Relaciones
    {
        int a,b;

        public Relaciones() { 
        
        }

        public Relaciones(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        //public int A { get => A1; set => A1 = value; }
        //public int A1 { get => a; set => a = value; }




        public void set_a(int nuevo1)
        {
            a = nuevo1;
        }
        public void set_b(int nuevo2)
        {
            b = nuevo2;
        }

        public int get_a()
        {
            return a;
        }

        public int get_b()
        {
            return b;
        }



    }
}
