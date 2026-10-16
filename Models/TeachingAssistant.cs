namespace Bai06.Models;

public class TeachingAssistant : Borrower
{
    public TeachingAssistant(string borrowerId, string fullName)
        : base(borrowerId, fullName, 2)
    {
    }
    public override string ToString()
    {
        return $"Teaching Assistant: {BorrowerId}, {FullName}, Maximum Devices: {GetMaximumDevices()}";
    }
    ~TeachingAssistant()
    {
        Console.WriteLine($"Destructor Teaching Assistant {BorrowerId}.");
    }
}