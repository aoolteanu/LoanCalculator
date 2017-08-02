using CommandLine;

namespace LoanCalculator
{
    public class ConsoleOptions
    {
        [Value(0, Required = true, HelpText = "The name of the file containing market data")]
        public string MarketFile { get; set; }

        [Value(1, Required = true, HelpText = "Loan amount")]
        public double LoanAmount { get; set; }


    }

}
