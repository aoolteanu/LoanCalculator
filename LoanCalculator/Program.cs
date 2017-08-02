using System;
using System.Threading.Tasks;

namespace LoanCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var quoteCalc = new QuoteProcessor();
            quoteCalc.Process(args, Console.Out, Console.Error);

            Console.ReadLine();
        }
    }


}
