
using System;
using System.Collections.Generic;

class BankAccount
{
    public int AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public decimal Balance { get; set; }

    public BankAccount(int accountNumber, string accountHolder, decimal balance)
    {
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Balance = balance;
    }

    public virtual void ShowAccountDetails()
    {
        Console.WriteLine($"Account Number: {AccountNumber}");
        Console.WriteLine($"Account Holder: {AccountHolder}");
        Console.WriteLine($"Balance: {Balance}");
    }

    public virtual void CalculateInterest()
    {
        Console.WriteLine("No interest calculation for base account.");
    }
}
class SavingAccount : BankAccount
{
    public decimal InterestRate { get; set; }

    public SavingAccount(int accountNumber, string accountHolder, decimal balance, decimal interestRate)
        : base(accountNumber, accountHolder, balance)
    {
        InterestRate = interestRate;
    }

    public override void ShowAccountDetails()
    {
        base.ShowAccountDetails();
        Console.WriteLine($"Interest Rate: {InterestRate}%");
    }

    public override void CalculateInterest()
    {
        decimal interest = Balance * InterestRate / 100;
        Console.WriteLine($"Calculated Interest: {interest}");
    }
}
class CurrentAccount : BankAccount
{
    public decimal OverdraftLimit { get; set; }

    public CurrentAccount(int accountNumber, string accountHolder, decimal balance, decimal overdraftLimit)
        : base(accountNumber, accountHolder, balance)
    {
        OverdraftLimit = overdraftLimit;
    }

    public override void ShowAccountDetails()
    {
        base.ShowAccountDetails();
        Console.WriteLine($"Overdraft Limit: {OverdraftLimit}");
    }

    public override void CalculateInterest()
    {
        Console.WriteLine("No interest for current account.");
    }
}
class Program
{
    static void Main()
    {
        SavingAccount saving = new SavingAccount(1, "Tasneem", 10000, 10);
        CurrentAccount current = new CurrentAccount(2, "asmaa", 5000, 2000);

        List<BankAccount> accounts = new List<BankAccount>();
        accounts.Add(saving);
        accounts.Add(current);

        foreach (BankAccount acc in accounts)
        {
            acc.ShowAccountDetails();
            acc.CalculateInterest();
            Console.WriteLine("----------------------");
        }
    }
}