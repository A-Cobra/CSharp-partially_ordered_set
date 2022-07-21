using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matematica_Discreta
{
    internal class PrimeFactor
    {

		int value;
		int iterations;
		public PrimeFactor()
		{
			iterations = 0;
		}
		public PrimeFactor(int value)
		{
			iterations = 0;
			this.value = value;
		}
		~PrimeFactor()
		{

		}

		public void incrementIterations()
		{
			iterations += 1;
		}


		public int getIterations()
		{
			return iterations;
		}
		public int getValue()
		{
			return value;

		}











	}
}
