namespace Bai06.Models;

using Bai06.Enums;

public class ACRemote : Device
{
    public string _brand = string.Empty;
    public ACRemoteMode _mode = ACRemoteMode.Cool;

    public ACRemoteSwingMode _swingMode = ACRemoteSwingMode.Off;

    public string Brand
    {
        get => _brand;
        set
        {
            _brand = value;
        }
    }

    public ACRemoteMode Mode
    {
        get => _mode;
        set
        {
            _mode = value;
        }
    }

    public ACRemoteSwingMode SwingMode
    {
        get => _swingMode;
        set
        {
            _swingMode = value;
        }
    }

    public ACRemote(string deviceId, string deviceName, int commissionYear, double price, int maxSessionAllowed, ACRemoteMode mode, ACRemoteSwingMode swingMode)
        : base(deviceId, deviceName, commissionYear, price, maxSessionAllowed)
    {
        Mode = mode;
        SwingMode = swingMode;
    }

    public override double CalculateEstimatedAnnualMaintenanceCosts()
    {
        return Price + (Brand == "LG" ? 100 : 150);
    }

    public override string ToString()
    {
        return $"AC Remote: {DeviceId}, {DeviceName}, {CommissionYear}, {Price},Brand: {Brand}, Status: {Status}";
    }
    ~ACRemote()
    {
        Console.WriteLine($"Destructor DeviceId {DeviceId}.");
    }
}