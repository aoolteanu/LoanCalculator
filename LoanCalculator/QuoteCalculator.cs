using System;
using System.Collections.Generic;
using System.Linq;

namespace LoanCalculator
{
    public class QuoteCalculator : IQuoteCalculator
    {
        private const double _loanPeriodInMonths = 36d;
        private List<Lender> _lendersOrderedByRate;
        private int _numberOfActivatedLenders;
        public QuoteResult Quote { get; private set; }

        public QuoteCalculator(List<Lender> lendersByRate)
        {
            _lendersOrderedByRate = lendersByRate.OrderBy(l => l.Rate)
                                            .ThenByDescending(l => l.Amount).ToList();
        }

        public bool CalculateLoanQuote(decimal loanAmount)
        {
            decimal availableAmount = 0m;
            bool isRequestedAmountAvailable = false;
            for (int i = 0; i < _lendersOrderedByRate.Count; i++)
            {
                availableAmount += _lendersOrderedByRate[i].Amount;
                _numberOfActivatedLenders = i;
                if (availableAmount >= loanAmount)
                {
                    isRequestedAmountAvailable = true;
                    this.Quote = CalculateQuote(loanAmount);
                    break;
                }
            }

            return isRequestedAmountAvailable;
        }

        private QuoteResult CalculateQuote(decimal loanAmount)
        {
            var quote = new QuoteResult();
            var amountRemainingToBeLoaned = loanAmount;
            for (int i = 0; i < _numberOfActivatedLenders; i++)
            {
                var lender = _lendersOrderedByRate[i];
                var amountReceivedFromLender = lender.Amount;
                amountRemainingToBeLoaned -= amountReceivedFromLender;
                quote.TotalRepayment += CalculateCompoundInterestRepaymentAmount(lender.Rate, lender.Amount);
            }

            var finalLender = _lendersOrderedByRate[_numberOfActivatedLenders];
            quote.TotalRepayment += CalculateCompoundInterestRepaymentAmount(finalLender.Rate, amountRemainingToBeLoaned);
            quote.Rate = CalculateLoanRate(loanAmount, quote.TotalRepayment) * 100;
            quote.MonthlyRepayment = quote.TotalRepayment / (decimal)_loanPeriodInMonths;

            return quote;
        }

        private decimal CalculateCompoundInterestRepaymentAmount(decimal rate, decimal loanAmount)
        {
            return (loanAmount * (decimal)Math.Pow((1 + (double)(rate / 12)), _loanPeriodInMonths));
        }

        private decimal CalculateLoanRate(decimal loanAmount, decimal repaymentAmount)
        {
            return 12 * (decimal)(Math.Pow((double)(repaymentAmount / loanAmount), (1 / _loanPeriodInMonths)) - 1);
        }
    }
}
