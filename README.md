# Personal Finance Tracker 📈

A WPF desktop application for managing personal finances. Track income and expense transactions, set monthly budgets with goals, and generate reports to gain insight into your spending habits.

Built with **C#** and **.NET 8** using **Windows Presentation Foundation (WPF)**.

## Features

### Transaction Management

- Record income and expense transactions with amount, date, category, and description
- View transactions in a sortable list grouped by month

### Budget Planning

- Set monthly income goals and expense limits
- Track actual income and expenses against your targets in real time

### Reporting & Analysis

- **Monthly Report:** View all transactions for a selected month and year
- **Category Report:** Filter transactions by a specific category within a month
- **Budget Progress:** Compare actual income/expenses against your goals
- **Overspending Detection:** Identify expense categories where spending exceeds your monthly limit
- **Spending Prediction:** Predict future spending per category based on historical averages

## Prerequisites

- **Windows 10/11** (WPF is Windows-only)
- [**.NET 8 SDK**](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- (Optional) **Visual Studio 2022** with the _.NET desktop development_ workload

## Getting Started

### Clone the repository

```bash
git clone https://github.com/<your-username>/FinanceTracker.git
cd FinanceTracker
```

### Build and run via CLI

```bash
dotnet restore FinanceTracker.sln
dotnet build FinanceTracker.sln -c Release
dotnet run --project FinanceTracker/FinanceTracker.csproj
```

### Build and run via Visual Studio

1. Open `FinanceTracker.sln` in Visual Studio 2022
2. Set **FinanceTracker** as the startup project
3. Press **F5** to build and launch

## Usage

The application opens with a two-panel layout:

| Area                               | Description                                                                                                                                                                       |
| ---------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Left panel — Add Transaction**   | Enter an amount, pick a date, choose Income or Expense, specify a category (e.g. _Groceries_, _Salary_), and add an optional description. Click **Add Transaction** to record it. |
| **Left panel — Set Budget**        | Select a month and enter a year, then set your income goal and expense limit. Click **Set Budget** to save.                                                                       |
| **Right panel — Transactions tab** | Displays a table of transactions for the current month showing date, type, category, amount, and description.                                                                     |
| **Right panel — Reports tab**      | Generate text-based reports using the five report buttons. Results appear in the read-only text area below.                                                                       |

## Project Structure

```text
FinanceTracker/
├── FinanceTracker.sln                        # Visual Studio solution file
├── .gitignore
├── .gitattributes
├── README.md
└── FinanceTracker/                           # Main project directory
    ├── FinanceTracker.csproj                 # .NET 8 WPF project file
    ├── App.xaml / App.xaml.cs                # Application entry point
    ├── MainWindow.xaml / MainWindow.xaml.cs  # UI layout and event handlers
    ├── FinanceTracker.cs                     # Core logic (budgets, transactions, reports)
    ├── Transaction.cs                        # Transaction model
    ├── Budget.cs                             # Budget model with computed totals
    ├── Report.cs                             # Monthly and category report generation
    └── AssemblyInfo.cs                       # Assembly metadata
```

## Architecture

The application follows a **code-behind** pattern with a small domain layer:

| Class            | Responsibility                                                                                                                                                                                    |
| ---------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `Transaction`    | Data model — stores amount, date, type (Income/Expense), category, and description. Each transaction is assigned a unique GUID.                                                                   |
| `Budget`         | Data model — represents a monthly budget with income goal, expense limit, and a list of transactions. Computes actual income/expenses via LINQ.                                                   |
| `FinanceTracker` | Service layer — manages an in-memory collection of budgets. Provides methods for adding transactions, setting budgets, tracking progress, detecting overspending, and predicting future spending. |
| `Report`         | Formatter — generates human-readable monthly and category report strings.                                                                                                                         |
| `MainWindow`     | UI layer — WPF window with XAML layout and C# event handlers that wire the UI to the domain classes.                                                                                              |

### Data Storage

All data is held **in memory** during the application session. Closing the application will discard all transactions and budgets.

## Technologies

| Component    | Technology                            |
| ------------ | ------------------------------------- |
| Language     | C# 12                                 |
| Framework    | .NET 8 (Windows)                      |
| UI           | WPF (Windows Presentation Foundation) |
| Dependencies | None (no external NuGet packages)     |

## Publishing

To create a self-contained executable:

```bash
dotnet publish FinanceTracker/FinanceTracker.csproj -c Release -r win-x64 --self-contained
```

The output will be in `bin/Release/net8.0-windows/win-x64/publish/`.

## Known Limitations

- **No persistence:** data is lost when the application closes
- **No input validation:** non-numeric values in amount or year fields will cause an error
- **No unit tests:** the project does not include a test suite
- **Windows only:** WPF does not support cross-platform deployment
