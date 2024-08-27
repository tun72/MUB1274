using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionOverLoading
{
    class Calculate
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }

        public int Sum(int a, int b, int c)
        {
            return a + b + c;

        }

        public double Sum(double x, double y)
        {

            return x + y;

        }
    }


    class Program
    {
        public static void Main(string[] args)
        {
            Calculate calculate = new Calculate();
            Console.WriteLine("Sum of two numbers: " + calculate.Sum(1, 2));
            Console.WriteLine("Sum of three numbers: " + calculate.Sum(1, 2, 3));
            Console.WriteLine("Sum of two double numbers: " + calculate.Sum(1.1, 2.2));



            Console.ReadLine();
        }

    }
}




