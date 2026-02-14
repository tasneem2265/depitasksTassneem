
using System;
using System.Collections.Generic;

class BankAccount
{
    private static int counter = 1;

    public int AccountNumber { get; private set; }
    public decimal Balance { get; protected set; }
    public DateTime DateOpened { get; private set; }

    protected List<string> transactions = new List<string>();

    public BankAccount(decimal balance)
    {
        AccountNumber = counter++;
        Balance = balance;
        DateOpened = DateTime.Now;
    }

    public virtual void Deposit(decimal amount)
    {
        Balance += amount;
        transactions.Add($"Deposited: {amount}");
    }

    public virtual void Withdraw(decimal amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            transactions.Add($"Withdrawn: {amount}");
        }
        else
        {
            Console.WriteLine("Insufficient balance!");
        }
    }

    public void Transfer(BankAccount target, decimal amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            target.Balance += amount;

            transactions.Add($"Transferred {amount} to Account {target.AccountNumber}");
            target.transactions.Add($"Received {amount} from Account {AccountNumber}");
        }
        else
        {
            Console.WriteLine("Transfer failed: insufficient balance");
        }
    }

    public virtual void ShowAccountDetails()
    {
        Console.WriteLine($"Account: {AccountNumber}");
        Console.WriteLine($"Balance: {Balance}");
        Console.WriteLine($"Opened: {DateOpened}");
    }

    public void ShowTransactions()
    {
        Console.WriteLine("Transaction History:");
        foreach (var t in transactions)
        {
            Console.WriteLine(t);
        }
    }

    public virtual void CalculateMonthlyInterest()
    {
        // base account no interest
    }
}
class SavingAccount : BankAccount
{
    public decimal InterestRate { get; set; }

    public SavingAccount(decimal balance, decimal interestRate)
        : base(balance)
    {
        InterestRate = interestRate;
    }

    public override void CalculateMonthlyInterest()
    {
        decimal interest = Balance * (InterestRate / 100);
        Balance += interest;
        transactions.Add($"Monthly Interest Added: {interest}");
    }
}
class CurrentAccount : BankAccount
{
    public decimal OverdraftLimit { get; set; }

    public CurrentAccount(decimal balance, decimal overdraftLimit)
        : base(balance)
    {
        OverdraftLimit = overdraftLimit;
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= Balance + OverdraftLimit)
        {
            Balance -= amount;
            transactions.Add($"Withdrawn: {amount}");
        }
        else
        {
            Console.WriteLine("Exceeded overdraft limit!");
        }
    }
}
class Customer
{
    private static int idCounter = 1;

    public int CustomerId { get; private set; }
    public string FullName { get; set; }
    public string NationalId { get; set; }
    public DateTime DateOfBirth { get; set; }

    public List<BankAccount> Accounts { get; set; } = new List<BankAccount>();

    public Customer(string name, string nationalId, DateTime dob)
    {
        CustomerId = idCounter++;
        FullName = name;
        NationalId = nationalId;
        DateOfBirth = dob;
    }

    public decimal GetTotalBalance()
    {
        decimal total = 0;
        foreach (var acc in Accounts)
            total += acc.Balance;
        return total;
    }
}
class Bank
{
    public string Name { get; set; }
    public string BranchCode { get; set; }

    public List<Customer> Customers { get; set; } = new List<Customer>();

    public Bank(string name, string branch)
    {
        Name = name;
        BranchCode = branch;
    }

    public void AddCustomer(Customer customer)
    {
        Customers.Add(customer);
    }

    public void ShowReport()
    {
        foreach (var customer in Customers)
        {
            Console.WriteLine($"Customer: {customer.FullName}");
            foreach (var acc in customer.Accounts)
            {
                acc.ShowAccountDetails();
            }
            Console.WriteLine("-------------------");
        }
    }

    public Customer SearchByNationalId(string nationalId)
    {
        return Customers.Find(c => c.NationalId == nationalId);
    }
}
class Program
{
    static void Main()
    {
        Bank bank = new Bank("Cairo Bank", "001");

        Customer c1 = new Customer("saber", "123456", new DateTime(1976,9,3));

        SavingAccount s1 = new SavingAccount(10000, 5);
        CurrentAccount cAcc = new CurrentAccount(5000, 2000);

        c1.Accounts.Add(s1);
        c1.Accounts.Add(cAcc);

        bank.AddCustomer(c1);

        s1.Deposit(2000);
        s1.CalculateMonthlyInterest();

        cAcc.Withdraw(6000);

        s1.Transfer(cAcc, 1000);

        bank.ShowReport();

        s1.ShowTransactions();
    }
}