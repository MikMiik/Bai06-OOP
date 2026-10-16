namespace Bai06.Models;

public class Lecturer : Borrower
{
    public Lecturer(string borrowerId, string fullName)
        : base(borrowerId, fullName, 3)
    {
    }
    public override string ToString()
    {
        return $"Lecturer: {BorrowerId}, {FullName}, Maximum Devices: {GetMaximumDevices()}";
    }
    ~Lecturer()
    {
        Console.WriteLine($"Destructor Lecturer {BorrowerId}.");
    }
}