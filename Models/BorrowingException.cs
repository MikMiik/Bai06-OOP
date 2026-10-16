namespace Bai06.Models;

public class BorrowingException : Exception
{
    public BorrowingException(string message)
    : base(message)
    {
    }
    public BorrowingException(
    string message,
    Exception innerException)
    : base(message, innerException)
    {
    }
}