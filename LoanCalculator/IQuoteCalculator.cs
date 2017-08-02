namespace LoanCalculator
{
    public interface IQuoteCalculator
    {
        QuoteResult Quote { get; }
        bool CalculateLoanQuote(decimal loanAmount);
    }
}
