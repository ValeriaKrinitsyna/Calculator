using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalcLibrary
{
    public static class Calc
    {
        public static string DoOperation(string s)
        {
            try
            {
                string[] operands = GetOperands(s); //получить операнды
                string operation = GetOperation(s)[0]; //получить операцию
                string res = "";
                if (operation == "=") //если число отрицательное, минус относится к числу, а не к операции
                {
                    operation = operation.Remove(operation.Length - 1);
                    operation += "+"; //при вычитании прибавляется отрицательное число
                }
                if (operation == "+" || operation == "-" || operation == "*" || operation == "/" || operation == "mod" || operation == "div" || operation == "^")
                    res = DoubleOperation[operation](double.Parse(operands[0]), double.Parse(operands[1])).ToString();
                else res = SingleOperation[operation](double.Parse(operands[0])).ToString();
                return res; //вычислить и получить строку
            }
            catch
            {
                return "Ошибка";
            }
        }

        public static string[] GetOperands(string s)
        {
            Regex rgx = new Regex(@"(-\d+[.,]?\d*)|(\d*[.,]?\d*)"); //регулярное выражение для отрицательного и положительного чисел, целого и десятичной дроби
            MatchCollection mc = rgx.Matches(s); //коллекция всех подходящих по шаблону выражений
            List<string> Im = new List<string>(); //список строк
            foreach (Match m in mc) //преобразование коллекции в список строк
            {
                if (m.Value.Length > 0) Im.Add(m.Value);
            }
            return Im.ToArray();
        }

        public static string[] GetOperation(string s)
        {
            Regex rgx = new Regex(@"(-\d+[.,]?\d*)|(\d*[.,]?\d*)"); //регулярное выражение для отрицательного и положительного чисел, целого и десятичной дроби
            MatchCollection mc = rgx.Matches(s); //коллекция всех подходящих по шаблону выражений
            List<string> Im = new List<string>(); //список строк
            foreach (Match m in mc) //преобразование коллекции в список строк
            {
                if (m.Value.Length > 0) Im.Add(m.Value);
            }
            string[] a = s.Split(Im.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            if (a.Length == 0) a = new string[] { "+" };
            return a;
        }

        public delegate T OperationDelegate<T>(T x, T y);
        public delegate T SingleOperationDelegate<T>(T x);

        public static Dictionary<string, OperationDelegate<double>> DoubleOperation = new Dictionary<string, OperationDelegate<double>>
        {
            { "+", (x, y) => x + y },
            { "-", (x, y) => x - y },
            { "*", (x, y) => x * y },
            { "/", (x, y) => x / y },
            { "mod", (x, y) => x % y }, //остаток от деления
            { "div", (x, y) => (int)x / (int)y }, //целая часть от деления
            { "^", (x, y) => Math.Pow(x,y) }, //возведение в степень
        };

        public static Dictionary<string, SingleOperationDelegate<double>> SingleOperation = new Dictionary<string, SingleOperationDelegate<double>>
        {
            { "+/-", (x) => -x }, //смена знака
            { "1/", (x) => 1/x }, //нахождение числа 1/x
            { "!", (x) => Factorial(x) }, //факториал числа
            { "sqrt", (x) => Math.Sqrt(x) }, //квадратный корень
            { "^2", (x) => Math.Pow(x,2) }, //возведение в квадрат
            { "sin", (x) => Math.Sin(x) }, //синус
            { "cos", (x) => Math.Cos(x) }, //косинус
            { "tan", (x) => Math.Sin(x) / Math.Cos(x) }, //тангенс
            { "e^", (x) => Math.Pow(x, Math.E) }, //нахождение e^x
        };

        static double Factorial(double x)
        {
            if (x == 0) return 1;
            else return x * Factorial(x - 1);
        }
    }
}
