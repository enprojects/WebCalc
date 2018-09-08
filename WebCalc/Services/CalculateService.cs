using Neleus.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Interfaces;

namespace WebCalc.Services
{
    public class CalculateService : ICalculateService
    {
        private readonly ICacheManager _cache;
        private readonly IServiceByNameFactory<IOperator> _factory;
       

        public CalculateService(ICacheManager cache, IServiceByNameFactory<IOperator> factory)
        {
            _cache = cache;
            _factory = factory;
        }

        public decimal? Evaluate(string expression)
        {
            var res = 0M;
            var opsStack = new Stack<char>();
            var valuesStack = new Stack<decimal>();
            expression = expression.Trim();
            
            return _cache.Get<decimal?>(expression, () =>
            {
                 
                if (ExpressionValidation(expression))
                {
                    try
                    {
                        var tokens = expression.ToCharArray();

                        for (int i = 0; i < tokens.Length; i++)
                        {
                            StringBuilder sb = new StringBuilder();

                            if (tokens[i] >= '0' && tokens[i] <= '9')
                            {
                               
                                while (tokens.Length > i && IsNumber(tokens[i]))
                                    sb.Append(tokens[i++]);

                                valuesStack.Push(decimal.Parse(sb.ToString()));
                            }

                            if (tokens.Length > i && (tokens[i] == '+' || tokens[i] == '-' ||
                                   tokens[i] == '*' || tokens[i] == '/'))
                            {
                                if (opsStack.Count == 0)
                                {
                                    opsStack.Push(tokens[i]);
                                }
                                else
                                {
                                    if (OperatorPrecedence(opsStack.Peek()) >= OperatorPrecedence(tokens[i]))
                                    {
                                        var result = Calculate(valuesStack, opsStack.Pop());
                                        valuesStack.Push(result);
                                        opsStack.Push(tokens[i]);
                                    }
                                    else
                                        opsStack.Push(tokens[i]);
                                }
                            }
                        }
                        
                        while (opsStack.Count > 0)
                        {
                            res = Calculate(valuesStack, opsStack.Pop());
                            valuesStack.Push(res);
                        }

                        return valuesStack.Pop();
                    }
                    catch {

                        return null;
                    }
                }
                return null;
            });
        }



        public bool ExpressionValidation(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return false;

            return true;
        }

        private bool IsNumber(char ch)
        {
            if (ch >= '0' && ch <= '9')
                return true;

            return false;
        }
        private int OperatorPrecedence(char op)
        {

            var operatorService = _factory.GetByName(op.ToString());
            return operatorService.Precedence;
            //var precedence = 0;

            //switch (op)
            //{
            //    case '+':
            //    case '-':
            //        break;

            //    case '*':
            //    case '/':
            //        precedence = 1;
            //        break;

            //}

            //return precedence;

        }
        private decimal Calculate(Stack<decimal> valuesStack, char op)    
        {
            decimal val2 = valuesStack.Pop();
            decimal val1 = valuesStack.Pop();

            var operatorService = _factory.GetByName(op.ToString());
            var result =   operatorService.Calculate<decimal>(val1, val2);

            //var result = 0M;

            //switch (op)
            //{
            //    case '+':
            //        result = val1 + val2;
            //        break;
            //    case '-':
            //        result = val1 - val2;
            //        break;
            //    case '*':
            //        result = val1 * val2;
            //        break;
            //    case '/':
            //        result = val1 / val2;
            //        break;

            //}
            return result;
        }

    }
}
