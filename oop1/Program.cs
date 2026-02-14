using System;

class BankAccount
{
    // ===== Fields =====
    public const string BankCode = "BNK001";

    public readonly DateTime CreatedDate;

    private int _accountNumber;
    private string _fullName;
    private string _nationalID;
    private string _phoneNumber;
    private string _address;
    private decimal _balance;

    // ===== Properties (with validation) =====

    public int AccountNumber
    {
        get { return _accountNumber; }
        set { _accountNumber = value; }
    }

    public string FullName
    {
        get { return _fullName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Full name cannot be empty.");

            _fullName = value;
        }
    }

    public string NationalID
    {
        get { return _nationalID; }
        set
        {
            if (!IsValidNationalID(value))
                throw new ArgumentException("National ID must be exactly 14 digits.");

            _nationalID = value;
        }
    }

    public string PhoneNumber
    {
        get { return _phoneNumber; }
        set
        {
            if (!IsValidPhoneNumber(value))
                throw new ArgumentException("Phone must start with 01 and be 11 digits.");

            _phoneNumber = value;
        }
    }

    public string Address
    {
        get { return _address; }
        set { _address = value; }
    }

    public decimal Balance
    {
        get { return _balance; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Balance cannot be negative.");

            _balance = value;
        }
    }

    // ===== Constructors =====

    public BankAccount()
    {
        CreatedDate = DateTime.Now;
        _balance = 0;
    }
    public BankAccount(int accountNumber, string fullName, string nationalID,
                       string phoneNumber, string address, decimal balance)
    {
        CreatedDate = DateTime.Now;

        AccountNumber = accountNumber;
        FullName = fullName;
        NationalID = nationalID;
        PhoneNumber = phoneNumber;
        Address = address;
        Balance = balance;
    }

    public BankAccount(int accountNumber, string fullName, string nationalID,
                       string phoneNumber, string address)
        : this(accountNumber, fullName, nationalID, phoneNumber, address, 0)
    {
    }


    public void ShowAccountDetails()
    {
        Console.WriteLine("===== Account Details =====");
        Console.WriteLine($"Bank Code: {BankCode}");
        Console.WriteLine($"Account Number: {AccountNumber}");
        Console.WriteLine($"Full Name: {FullName}");
        Console.WriteLine($"National ID: {NationalID}");
        Console.WriteLine($"Phone: {PhoneNumber}");
        Console.WriteLine($"Address: {Address}");
        Console.WriteLine($"Balance: {Balance}");
        Console.WriteLine($"Created Date: {CreatedDate}");
        Console.WriteLine("============================");
    }

    public bool IsValidNationalID(string id)
    {
        return !string.IsNullOrEmpty(id)
               && id.Length == 14
               && long.TryParse(id, out _);
    }

    public bool IsValidPhoneNumber(string phone)
    {
        return !string.IsNullOrEmpty(phone)
               && phone.StartsWith("01")
               && phone.Length == 11
               && long.TryParse(phone, out _);
    }
}
class Program
{
    static void Main()
    {
        BankAccount acc1 = new BankAccount(
            1001,
            "ahmed mohamed",
            "12345678912345",
            "01012345678",
            "Cairo",
            5000
        );

        // Object 2 - Overloaded constructor (balance = 0)
        BankAccount acc2 = new BankAccount(
            1002,
            "mohamed ahmed",
            "12345678912344",
            "01123456789",
            "Giza"
        );

        acc1.ShowAccountDetails();
        acc2.ShowAccountDetails();
    }
}
