namespace FinanceTracker
{
    public class Transaction
    {
        public string TransactionID { get; set; } = Guid.NewGuid().ToString();
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Type { get; set; } // "Income" or "Expense"
        public string? Category { get; set; } // e.g., Groceries, Rent, Salary, etc.
        public string? Description { get; set; }
    }
}
