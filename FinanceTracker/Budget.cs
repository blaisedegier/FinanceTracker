namespace FinanceTracker
{
    public class Budget
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public double IncomeGoal { get; set; }
        public double ExpenseLimit { get; set; }
        public double ActualIncome => Transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);
        public double ActualExpenses => Transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
