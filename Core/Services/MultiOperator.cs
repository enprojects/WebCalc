﻿

using Core.Interfaces;

namespace Core.Services
{
    public class MultiOperator : IOperator
    {
        public string Name => "Multi";
        public int Precedence => 1;

        public T Calculate<T>(T param1, T param2)
        {
            dynamic p1 = param1;
            dynamic p2 = param2;

            return p1 * p2;
        }
        
    }
}
