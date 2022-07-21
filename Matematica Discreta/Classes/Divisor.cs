using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matematica_Discreta
{
    class Divisor
    {
        int valor;

        public Divisor(int valor)
        {
            this.valor = valor;
        }

        public int Valor { get => valor; set => valor = value; }
    }
}
