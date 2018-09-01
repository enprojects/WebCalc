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

        public CalculateService(ICacheManager cache)
        {
            _cache = cache;
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
                                if (Operatorprecedence(opsStack.Peek()) >= Operatorprecedence(tokens[i]))
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
                        //if (valuesStack.Count > 1)
                        //{
                        //    res = Calculate(valuesStack, opsStack.Pop());
                        //    valuesStack.Push(res);
                        //}
                        //else
                        //{
                        //    var op = opsStack.Pop();
                        //    if (op == '-' || op == '+')
                        //        return decimal.Parse(op.ToString() + valuesStack.Pop());
                        //}
                    }

                    return valuesStack.Pop();
                }
                return null;

            });
        }



        public bool ExpressionValidation(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return false;

            //var expressionChars = expression.ToCharArray();


            ////expression must start && end with number
            //if (IsNumber(expression.First())  && IsNumber(expression.Last()))
            //{
            //    for (int i = 0; i < length; i++)
            //    {

            //    }
            //}



            return true;
        }

        private bool IsNumber(char ch)
        {
            if (ch >= '0' && ch <= '9')
                return true;

            return false;
        }
        private int Operatorprecedence(char op)
        {
            var precedence = 0;

            switch (op)
            {
                case '+':
                case '-':
                    break;

                case '*':
                case '/':
                    precedence = 1;
                    break;

            }

            return precedence;

        }
        private decimal Calculate(Stack<decimal> valuesStack, char op)
        {

            decimal val2 = valuesStack.Pop();
            decimal val1 = valuesStack.Pop();

            var result = 0M;

            switch (op)
            {
                case '+':
                    result = val1 + val2;
                    break;
                case '-':
                    result = val1 - val2;
                    break;
                case '*':
                    result = val1 * val2;
                    break;
                case '/':
                    result = val1 / val2;
                    break;

            }
            return result;
        }

    }
}
