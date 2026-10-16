namespace Bai06.Models;

public class DuplicateDeviceException : BorrowingException
{
    public string DeviceId { get; }
    public DuplicateDeviceException(string deviceId)
        : base($"Thiết bị '{deviceId}' đã tồn tại trong phiếu mượn.")
    {
        DeviceId = deviceId;
    }
}