namespace Bai06.Models;

public class DeviceNotInSlipException : BorrowingException
{
    public string DeviceId { get; }
    public DeviceNotInSlipException(string deviceId)
        : base($"Thiết bị '{deviceId}'không tồn tại trong phiếu mượn.")
    {
        DeviceId = deviceId;
    }
}