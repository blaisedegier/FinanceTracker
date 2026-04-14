namespace FinanceTracker
{
    public class FinanceTracker
    {
        private List<Budget> Budgets = new List<Budget>();

        // Add income or expense transaction
        public void AddTransaction(int month, int year, Transaction transaction)
        {
            // Find the budget for the given month and year
            var budget = Budgets.FirstOrDefault(b => b.Month == month && b.Year == year);

            // If the budget exists, add the transaction to it.
            if (budget != null)
            {
                budget.Transactions.Add(transaction);
            }
            // Otherwise, create a new budget and add the transaction to it.
            else
            {
                budget = new Budget { Month = month, Year = year };
                budget.Transactions.Add(transaction);
                Budgets.Add(budget);
            }
        }

        // Set budget for a month
        public void SetBudget(int month, int year, double incomeGoal, double expenseLimit)
        {
            // Find the budget for the given month and year
            var budget = Budgets.FirstOrDefault(b => b.Month == month && b.Year == year);

            // If the budget exists, update the income goal and expense limit.
            if (budget != null)
            {
                budget.IncomeGoal = incomeGoal;
                budget.ExpenseLimit = expenseLimit;
            }
            // Otherwise, create a new budget with the income goal and expense limit
            else
            {
                Budgets.Add(new Budget
                {
                    Month = month,
                    Year = year,
                    IncomeGoal = incomeGoal,
                    ExpenseLimit = expenseLimit
                });
            }
        }

        // Track progress against income and expense goals
        public double CalculateSavings(int month, int year)
        {
            // Find the budget for the given month and year
            var budget = Budgets.FirstOrDefault(b => b.Month == month && b.Year == year);

            // If the budget exists, calculate the savings
            return budget != null ? budget.ActualIncome - budget.ActualExpenses : 0;
        }

        // Generate reports on spending by category
        public IEnumerable<Transaction> ListTransactionsByCategory(int month, int year, string category)
        {
            // Find the budget for the given month and year
            var budget = Budgets.FirstOrDefault(b => b.Month == month && b.Year == year);

            // If the budget exists, return the transactions for the given category
            return budget?.Transactions.Where(t => t.Category == category) ?? Enumerable.Empty<Transaction>();
        }

        // List all transactions for a given month
        public IEnumerable<Transaction> ListTransactionsByMonth(int month, int year)
        {
            // Find the budget for the given month and year
            var budget = Budgets.FirstOrDefault(b => b.Month == month && b.Year == year);

            // If the budget exists, return the transactions
            return budget?.Transactions ?? Enumerable.Empty<Transaction>();
        }

        // Calculate savings
        public string TrackBudgetProgress(int month, int year)
        {
            // Find the budget for the given month and year
            var budget = Budgets.FirstOrDefault(b => b.Month == month && b.Year == year);

            // If the budget exists, display the budget progress
            if (budget != null)
            {
                string output = $"Income Goal: {budget.IncomeGoal}, Actual Income: {budget.ActualIncome}\nExpense Limit: {budget.ExpenseLimit}, Actual Expenses: {budget.ActualExpenses}";
                return output;
            }

            return string.Empty;
        }

        // Identify overspending in any category
        public string IdentifyOverspending(int month, int year)
        {
            // Find the budget for the given month and year
            var budget = Budgets.FirstOrDefault(b => b.Month == month && b.Year == year);
            string output = string.Empty;

            // If the budget exists, find the categories where the total expenses exceed the expense limit
            if (budget == null)
            {
                return output;
            }

            var overspentCategories = budget.Transactions
                    .Where(t => t.Type == "Expense")
                    .GroupBy(t => t.Category)
                    .Select(g => new
                    {
                        Category = g.Key,
                        Total = g.Sum(t => t.Amount)
                    })
                    .Where(c => c.Total > budget.ExpenseLimit);

            foreach (var category in overspentCategories)
            {
                output += $"Overspending in {category.Category}: {category.Total}";
            }

            return output;
        }

        // Predict future spending based on past trends
        public string PredictFutureSpending()
        {
            string output = string.Empty;

            // Calculate the average spending for each expense category
            var futurePrediction = Budgets
                .SelectMany(b => b.Transactions)
                .Where(t => t.Type == "Expense")
                .GroupBy(t => t.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    AverageSpending = g.Average(t => t.Amount)
                });

            foreach (var prediction in futurePrediction)
            {
                output += $"Predicted future spending for {prediction.Category}: {prediction.AverageSpending}\n";
            }

            return output;
        }
    }
}
