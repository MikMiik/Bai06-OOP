namespace Bai06.Models;

public class Micro : Device
{
    private bool _wired;
    private double _volume = 50;
    private bool _isMuted = true;
    public bool Wired
    {
        get => _wired;
        set
        {
            _wired = value;
        }
    }

    public double Volume
    {
        get => _volume;
        set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Volume must be between 0 and 100.");
            }
            _volume = value;
        }
    }

    public bool IsMuted
    {
        get => _isMuted;
        set
        {
            _isMuted = value;
        }
    }

    public Micro(string deviceId, string deviceName, int commissionYear, double price, int maxSessionAllowed, bool wired)
        : base(deviceId, deviceName, commissionYear, price, maxSessionAllowed)
    {
        Wired = wired;
    }

    public override double CalculateEstimatedAnnualMaintenanceCosts()
    {
        return Price + (Wired ? 50 : 100);
    }

    public override string ToString()
    {
        return $"Microphone: {DeviceId}, {DeviceName}, {CommissionYear}, {Price}, {(Wired ? "Wired" : "Wireless")}, Volume: {Volume}, Is Muted: {IsMuted}, Status: {Status}";
    }
    ~Micro()
    {
        Console.WriteLine($"Destructor DeviceId {DeviceId}.");
    }
}