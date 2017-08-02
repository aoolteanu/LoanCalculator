using System;
using System.Text;

namespace LoanCalculator
{
    public class QuoteResult
    {
        public decimal Rate { get; set; }
        public decimal MonthlyRepayment { get; set; }
        public decimal TotalRepayment { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Rate: ").Append(Math.Round(Rate, 1)).Append("%").AppendLine();
            sb.Append("Monthly repayment: ").Append(Math.Round(MonthlyRepayment, 2)).AppendLine();
            sb.Append("Total repayment: ").Append(Math.Round(TotalRepayment, 2));

            return sb.ToString();
        }
    }
}
