namespace Bai06.Models;

public class DeviceAlreadyReturnedException : BorrowingException
{
    public string DeviceId { get; }

    public DateTime ReturnDate { get; }

    public DeviceAlreadyReturnedException(string deviceId, DateTime returnDate)
        : base($"Thiết bị '{deviceId}' đã được trả trước đó vào ngày {returnDate}.")
    {
        DeviceId = deviceId;
        ReturnDate = returnDate;
    }
}