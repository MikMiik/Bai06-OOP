namespace Bai06.Models;

using Bai06.Enums;
public class BorrowDayLimitExceededException : BorrowingException
{
    public string BorrowerId { get; }
    public int MaxDaysAllowed { get; }
    public int BorrowDays { get; }

    public BorrowDayLimitExceededException(string borrowerId, int borrowDays, int maxDaysAllowed)
        : base($"Số ngày mượn vượt quá giới hạn cho phép. BorrowerId: {borrowerId}, BorrowDays: {borrowDays}, MaxDaysAllowed: {maxDaysAllowed}")
    {
        BorrowerId = borrowerId;
        BorrowDays = borrowDays;
        MaxDaysAllowed = maxDaysAllowed;
    }
}