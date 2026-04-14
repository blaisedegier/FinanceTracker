using System.Windows;
using System.Windows.Controls;

namespace FinanceTracker
{
    public partial class MainWindow : Window
    {
        private FinanceTracker financeTracker = new FinanceTracker();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            double amount = double.Parse(AmountTextBox.Text);
            DateTime date = DatePicker.SelectedDate ?? DateTime.Now;
            var type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string category = CategoryTextBox.Text;
            string description = DescriptionTextBox.Text;

            var transaction = new Transaction
            {
                Amount = amount,
                Date = date,
                Type = type,
                Category = category,
                Description = description
            };

            int month = date.Month;
            int year = date.Year;

            financeTracker.AddTransaction(month, year, transaction);
            UpdateTransactionList(month, year);

            MessageBox.Show("Transaction added successfully!");
        }

        private void SetBudget_Click(object sender, RoutedEventArgs e)
        {
            int month = MonthComboBox.SelectedIndex + 1;
            int year = int.Parse(YearTextBox.Text);
            double incomeGoal = double.Parse(IncomeGoalTextBox.Text);
            double expenseLimit = double.Parse(ExpenseLimitTextBox.Text);

            financeTracker.SetBudget(month, year, incomeGoal, expenseLimit);

            MessageBox.Show("Budget set successfully!");
        }

        private void GenerateMonthlyReport_Click(object sender, RoutedEventArgs e)
        {
            int month = MonthComboBox.SelectedIndex + 1;
            int year = int.Parse(YearTextBox.Text);

            var report = new Report();
            string reportContent = report.GenerateMonthlyReport(financeTracker, month, year);

            ReportTextBox.Text = reportContent;
        }

        private void GenerateCategoryReport_Click(object sender, RoutedEventArgs e)
        {
            int month = MonthComboBox.SelectedIndex + 1;
            int year = int.Parse(YearTextBox.Text);
            string category = CategoryTextBox.Text;

            var report = new Report();
            string reportContent = report.GenerateCategoryReport(financeTracker, month, year, category);

            ReportTextBox.Text = reportContent;
        }

        private void TrackBudgetProgress_Click(object sender, RoutedEventArgs e)
        {
            int month = MonthComboBox.SelectedIndex + 1;
            int year = int.Parse(YearTextBox.Text);

            string progress = financeTracker.TrackBudgetProgress(month, year);
            ReportTextBox.Text = progress;
        }

        private void IdentifyOverspending_Click(object sender, RoutedEventArgs e)
        {
            int month = MonthComboBox.SelectedIndex + 1;
            int year = int.Parse(YearTextBox.Text);

            string overspending = financeTracker.IdentifyOverspending(month, year);
            ReportTextBox.Text = overspending;
        }

        private void PredictFutureSpending_Click(object sender, RoutedEventArgs e)
        {
            string prediction = financeTracker.PredictFutureSpending();
            ReportTextBox.Text = prediction;
        }

        private void UpdateTransactionList(int month, int year)
        {
            TransactionsListView.ItemsSource = financeTracker.ListTransactionsByMonth(month, year);
        }
    }
}
