namespace Bai06.Models;

using Bai06.Enums;

public class ClassRoom
{
    private string _classRoomId = string.Empty;
    private string _classRoomName = string.Empty;
    private int _capacity = 0;
    public List<Device> Devices { get; } = new();

    public string ClassRoomId
    {
        get => _classRoomId;
        set => _classRoomId = value;
    }

    public string ClassRoomName
    {
        get => _classRoomName;
        set => _classRoomName = value;
    }

    public int Capacity
    {
        get => _capacity;
        set => _capacity = value;
    }


    public ClassRoom(string classRoomId, string classRoomName, int capacity)
    {
        ClassRoomId = classRoomId;
        ClassRoomName = classRoomName;
        Capacity = capacity;

    }

    public bool AddDevice(Device device)
    {
        ArgumentNullException.ThrowIfNull(device);
        if (Devices.Contains(device))
        {
            throw new InvalidOperationException($"Thiết bị {device.DeviceId} đã tồn tại trong lớp học.");
        }
        Devices.Add(device);
        Console.WriteLine($"Đã thêm thiết bị {device.DeviceId} vào lớp học {ClassRoomName}.");
        return true;
    }

    public bool RemoveDevice(string deviceId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(deviceId);
        var device = FindDevice(deviceId);
        if (device == null)
        {
            throw new InvalidOperationException($"Thiết bị {deviceId} không tồn tại trong lớp học.");
        }
        Devices.Remove(device);
        Console.WriteLine($"Đã xóa thiết bị {device.DeviceId} khỏi lớp học {ClassRoomName}.");
        return true;
    }


    public Device? FindDevice(string deviceId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(deviceId);
        return Devices.FirstOrDefault(d => d.DeviceId.Equals(deviceId, StringComparison.OrdinalIgnoreCase));

    }

    public double CalculateAnnualMaintenanceCost()
    {
        double totalCost = 0;
        foreach (var device in Devices)
        {
            totalCost += device.CalculateEstimatedAnnualMaintenanceCosts();
        }
        return totalCost;
    }

    public List<Device> GetDevicesRequiringMaintenance()
    {
        return [.. Devices.Where(d => d.Status == DeviceStatus.UnderMaintenance || d.CommissionYear + 5 < DateTime.Now.Year)];
    }

    public void DisplayDevices()
    {
        Console.WriteLine($"Class Room: {ClassRoomName}, ID: {ClassRoomId}");
        foreach (var device in Devices)
        {
            Console.WriteLine($"- {device.DeviceName} (ID: {device.DeviceId}, Year: {device.CommissionYear}, Price: {device.Price}, Status: {device.Status})");
        }
    }

    ~ClassRoom()
    {
        Console.WriteLine($"Destructor ClassRoomId {ClassRoomId}.");
    }
}