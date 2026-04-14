namespace FinanceTracker
{
    public class Report
    {
        public string GenerateMonthlyReport(FinanceTracker tracker, int month, int year)
        {
            var transactions = tracker.ListTransactionsByMonth(month, year);

            if (!transactions.Any())
            {
                return string.Empty;
            }

            string output = $"Monthly Report for {month}/{year}\n";
            foreach (var transaction in transactions)
            {
                output += $"{transaction.Date.ToShortDateString()} - {transaction.Category}: {transaction.Amount} ({transaction.Type})\n";
            }

            return output;
        }

        public string GenerateCategoryReport(FinanceTracker tracker, int month, int year, string category)
        {
            var transactions = tracker.ListTransactionsByCategory(month, year, category);

            if (!transactions.Any())
            {
                return string.Empty;
            }

            string output = $"Category Report for '{category}' in {month}/{year}\n";
            foreach (var transaction in transactions)
            {
                output += $"{transaction.Date.ToShortDateString()} - {transaction.Amount} ({transaction.Type})\n";
            }

            return output;
        }
    }
}
