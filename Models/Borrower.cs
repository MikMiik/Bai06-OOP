namespace Bai06.Models;

using Bai06.Interfaces;

public abstract class Borrower : IBorrower
{
    private string _borrowerId = string.Empty;
    private string _fullName = string.Empty;
    private int _maximumDevices;

    public string BorrowerId
    {
        get => _borrowerId;
        set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            _borrowerId = value;
        }
    }

    public string FullName
    {
        get => _fullName;
        set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            _fullName = value;
        }
    }

    public int MaximumDevices
    {
        get => _maximumDevices;
        protected set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            _maximumDevices = value;
        }
    }

    public int GetMaximumDevices()
    {
        return _maximumDevices;
    }

    public Borrower(string borrowerId, string fullName)
    {
        BorrowerId = borrowerId;
        FullName = fullName;
    }

    public Borrower(string borrowerId, string fullName, int maximumDevices)
    {
        BorrowerId = borrowerId;
        FullName = fullName;
        MaximumDevices = maximumDevices;
    }
    public override string ToString()
    {
        return $"Borrower: {BorrowerId}, {FullName}, Maximum Devices: {GetMaximumDevices()}";
    }
    ~Borrower()
    {
        Console.WriteLine($"Destructor Borrower {BorrowerId}.");
    }
}