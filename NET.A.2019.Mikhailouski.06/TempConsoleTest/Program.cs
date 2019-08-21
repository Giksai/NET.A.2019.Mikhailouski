using System;
using SixthDayLib;

namespace TempConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Polynomial pol1 = new Polynomial(new double[] { 14, 12, 30, 49, 5 });
            Polynomial pol2 = new Polynomial(new double[] { 1, 2, 2, 1 });
            Console.WriteLine(pol1);
            Console.WriteLine(pol2);
            Console.WriteLine(pol1+pol2);
        }
    }
}
