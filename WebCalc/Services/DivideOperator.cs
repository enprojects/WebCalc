using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCalc.Interfaces;

namespace WebCalc.Services
{
    public class DivideOperator : IOperator
    {
        public string Name => "divide";
        public int Precedence => 1;

        public T Calculate<T>(T param1, T param2)
        {
            dynamic p1 = param1;
            dynamic p2 = param2;

            return p1 / p2;
        }

    }
}
