namespace Bai06.Models;

using Bai06.Enums;
public class DeviceUnavailableException : BorrowingException
{
    public string DeviceId { get; }
    public DeviceStatus CurrentStatus { get; }

    public DeviceUnavailableException(string deviceId, DeviceStatus currentStatus)
        : base($"Thiết bị '{deviceId}' hiện không sẵn sàng để mượn (Trạng thái: {currentStatus}).")
    {
        DeviceId = deviceId;
        CurrentStatus = currentStatus;
    }
}