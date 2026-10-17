namespace Bai06.Models;

public class ReturnBeforeReturnDateException : BorrowingException
{
    public string BorrowerId { get; }
    public DateTime ReturnedDate { get; }

    public DateTime BorrowedDate { get; }

    public ReturnBeforeReturnDateException(string borrowerId, DateTime returnedDate, DateTime borrowedDate)
        : base($"Ngày trả không được trước ngày mượn. BorrowerId: {borrowerId}, ReturnedDate: {returnedDate}, BorrowedDate: {borrowedDate}")
    {
        BorrowerId = borrowerId;
        ReturnedDate = returnedDate;
        BorrowedDate = borrowedDate;
    }
}