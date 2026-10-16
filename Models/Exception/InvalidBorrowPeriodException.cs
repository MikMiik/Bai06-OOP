namespace Bai06.Models;

using Bai06.Enums;
public class InvalidBorrowPeriodException : BorrowingException
{
    public string DeviceId { get; }
    public int BorrowDays { get; }
    public int MaxBorrowDaysAllowed { get; }
    public InvalidBorrowPeriodException(string deviceId, int borrowDays, int maxBorrowDaysAllowed)
        : base($"Thiết bị '{deviceId}' có số ngày mượn không hợp lệ.")
    {
        DeviceId = deviceId;
        BorrowDays = borrowDays;
        MaxBorrowDaysAllowed = maxBorrowDaysAllowed;
    }
}