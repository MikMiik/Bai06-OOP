namespace Bai06.Models;


public class ProjectorRemote : Device
{
    public string _inputSource = string.Empty;

    public bool isLaserOn = false;

    public string InputSource
    {
        get => _inputSource;
        set
        {
            _inputSource = value;
        }
    }

    public ProjectorRemote(string deviceId, string deviceName, int commissionYear, double price, int maxSessionAllowed, string inputSource)
        : base(deviceId, deviceName, commissionYear, price, maxSessionAllowed)
    {
        InputSource = inputSource;
    }

    public override double CalculateEstimatedAnnualMaintenanceCosts()
    {
        return Price + (InputSource == "HDMI" ? 50 : 100);
    }

    public override string ToString()
    {
        return $"Projector Remote: {DeviceId}, {DeviceName}, {CommissionYear}, {Price}, Input Source: {InputSource}, Status: {Status}";
    }
    ~ProjectorRemote()
    {
        Console.WriteLine($"Destructor DeviceId {DeviceId}.");
    }
}