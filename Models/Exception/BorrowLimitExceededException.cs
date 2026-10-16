namespace Bai06.Models;

using Bai06.Enums;
public class BorrowLimitExceededException : BorrowingException
{
    public string BorrowerId { get; }
    public int MaximumDevices { get; }

    public BorrowLimitExceededException(string borrowerId, int maximumDevices)
        : base($"Người mượn '{borrowerId}' đã vượt quá số lượng thiết bị tối đa được cho phép ({maximumDevices}).")
    {
        BorrowerId = borrowerId;
        MaximumDevices = maximumDevices;
    }
}