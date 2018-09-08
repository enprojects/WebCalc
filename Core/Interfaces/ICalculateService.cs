using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICalculateService
    {
        decimal? Evaluate(string expression);
        bool ExpressionValidation(string expression);
    }
}
