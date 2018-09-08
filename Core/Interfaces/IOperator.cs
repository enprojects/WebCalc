using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOperator
    {
        string Name { get; }
        int Precedence { get; }
        T Calculate<T>(T param1, T param2);
    }
}
