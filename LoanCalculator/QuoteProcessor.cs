using System.IO;

namespace LoanCalculator
{
    public class QuoteProcessor : ProcessorTemplateBase<ConsoleOptions>
    {
        private const double MAX_LOAN_AMOUNT = 15000;
        private const double MIN_LOAN_AMOUNT = 1000;
        private const double LOAN_INCREMENT = 100;
        private IQuoteCalculator _quoteCalculator;
        private DataImporter _dataImporter;

        public QuoteProcessor()
        {
            _dataImporter = new DataImporter();
        }

        protected override void ProcessFile()
        {
            _quoteCalculator = new QuoteCalculator(_dataImporter.ImportLenderData(Options.MarketFile));
            var isAmountAvailable = _quoteCalculator.CalculateLoanQuote((decimal)Options.LoanAmount);
            if(!isAmountAvailable)
            {
                this.Output.WriteLine($"Requested amount: {Options.LoanAmount} is larger than available.");
                return;
            }
            
            this.Output.WriteLine($"Requested amount: {Options.LoanAmount}");
            this.Output.Write(_quoteCalculator.Quote.ToString());
        }

        protected override bool ValidateArguments()
        {
            if (Options == null)
                return false;
            var isValidFile = ValidateFileName(Options.MarketFile, this.Error);
            var isLoanAmountValid = ValidateLoanAmount(Options.LoanAmount, this.Error);

            return isValidFile && isLoanAmountValid;
        }

        private bool ValidateFileName(string filename, TextWriter error)
        {
            if (File.Exists(filename))
                return true;
            else
                error.WriteLine("Specified file could not be found.");
            return false;
        }

        private bool ValidateLoanAmount(double amount, TextWriter error)
        {
            if (amount < MIN_LOAN_AMOUNT)
            {
                error.WriteLine($"Amount has to be at least £{MIN_LOAN_AMOUNT}");
                return false;
            }

            if (amount > MAX_LOAN_AMOUNT)
            {
                error.WriteLine($"Amount has to be at most £{MAX_LOAN_AMOUNT}");
                return false;
            }

            if (amount % LOAN_INCREMENT != 0)
            {
                error.WriteLine($"Amount has to be in increments of {LOAN_INCREMENT}");
                return false;
            }

            return true;
        }
    }


}
