using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LoanCalculator.Tests
{
    public class QuoteCalculatorShould
    {
        [Test]
        public void Create_Quote_result_when_amount_is_available_from_one_lender()
        {
            var lenderList = new List<Lender>() { new Lender() { Amount = 450, Rate = 0.069m, Name = "Joe" } };
            var quoteCalculator = new QuoteCalculator(lenderList);
            quoteCalculator.CalculateLoanQuote(300m);

            Assert.IsNotNull(quoteCalculator.Quote);
        }

        [Test]
        public void Create_Quote_result_with_totalAmount_when_amount_is_available()
        {
            var lenderList = new List<Lender>() { new Lender() { Amount = 450, Rate = 0.069m, Name = "Joe" } };
            var quoteCalculator = new QuoteCalculator(lenderList);
            quoteCalculator.CalculateLoanQuote(300m);
            Assert.That(368.776m, Is.EqualTo(quoteCalculator.Quote.TotalRepayment).Within(0.0009));
        }

        //TODO: Add more tests
    }
}
