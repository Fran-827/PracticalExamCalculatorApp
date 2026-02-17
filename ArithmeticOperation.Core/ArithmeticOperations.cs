using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Globalization;

namespace ArithmeticOperation.Core
{
    public class ArithmeticOperations
    {
        public List<string> operators { get; private set; }
        public List<double> numbers { get; private set; }
        private double resultTotal;
        private double resultMD;
        public ArithmeticOperations() { }

        public void ExtractExpression(string expression)
        {
            this.operators = Regex.Split(expression, @"[\d.]+")
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();
            this.numbers = Regex.Split(expression, @"[^\d.]+")
                .Where(s => !string.IsNullOrEmpty(s) && s != ".")
                .Select(s => double.Parse(s, CultureInfo.InvariantCulture))
                .ToList();

            if (operators.Count == numbers.Count)
            {
                if (operators[0] == "-")
                {
                    this.numbers[0] = -numbers[0];
                    this.operators.RemoveAt(0);
                }
            }

        }

        public bool IsAbleToCalculate(string expression)
        {
            ExtractExpression(expression);
            if (operators.Count == numbers.Count || numbers.Count <= 1)
            {
                return false;
            }
            return true;
        }

        public string Calculate(string expression)
        {
            ExtractExpression(expression);

            for (int i = 0; i < operators.Count; i++)
            {
                if (operators[i] == "*" || operators[i] == "/")
                {
                    if (operators[i] == "*")
                    {
                        resultMD = numbers[i] * numbers[i + 1];
                    }
                    else
                    {
                        resultMD = numbers[i] / numbers[i + 1];
                        if (Double.IsInfinity(resultMD))
                        {
                            return "Error!";
                        }
                    }

                    numbers[i] = resultMD;
                    numbers.RemoveAt(i + 1);
                    operators.RemoveAt(i);
                    i--;
                }
            }

            resultTotal = numbers[0];

            for (int i = 0; i < operators.Count; i++)
            {
                if (operators[i] == "+" || operators[i] == "-")
                {
                    if (operators[i] == "+")
                    {
                        resultTotal += numbers[i + 1];
                    }
                    else
                    {
                        resultTotal -= numbers[i + 1];
                    }
                }
            }

            return resultTotal.ToString();
        }
    }
}
