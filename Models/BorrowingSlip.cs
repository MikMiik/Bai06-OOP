namespace Bai06.Models;

using Bai06.Enums;
using Bai06.Interfaces;
public class BorrowingSlip
{
    private string _slipId = string.Empty;
    private IBorrower _borrower = null!;

    private string _teachingRoom = string.Empty;
    private DateTime _borrowedDate;

    private IReadOnlyList<BorrowItem> Items { get; } = new List<BorrowItem>();

    public string SlipId
    {
        get => _slipId;
        set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            _slipId = value;
        }
    }

    public IBorrower Borrower
    {
        get => _borrower;
        set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value.ToString());
            _borrower = value;
        }
    }

    public string TeachingRoom
    {
        get => _teachingRoom;
        set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            _teachingRoom = value;
        }
    }

    public DateTime BorrowedDate
    {
        get => _borrowedDate;
        set => _borrowedDate = value;
    }

    public BorrowingSlip(string slipId, IBorrower borrower, string teachingRoom, DateTime borrowedDate)
    {
        SlipId = slipId;
        Borrower = borrower;
        TeachingRoom = teachingRoom;
        BorrowedDate = borrowedDate;
    }

    public void AddDevice(Device device, int borrowDays)
    {
        ArgumentNullException.ThrowIfNull(device, nameof(device));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(borrowDays, 0, nameof(borrowDays));
        if (Items.Any(item => item.Device.DeviceId == device.DeviceId))
        {
            throw new InvalidOperationException("Thiết bị đã tồn tại trong phiếu mượn.");
        }
        if (Items.Count >= Borrower.GetMaximumDevices())
        {
            throw new BorrowLimitExceededException(Borrower.BorrowerId, Borrower.GetMaximumDevices());
        }
        if (device.Status != DeviceStatus.Available)
        {
            throw new DeviceUnavailableException(device.DeviceId, device.Status);
        }
        if (borrowDays > device.MaxDaysAllowed)
        {
            throw new BorrowDayLimitExceededException(Borrower.BorrowerId, borrowDays, device.MaxDaysAllowed);
        }

        DateTime dueDate = BorrowedDate.AddDays(borrowDays);
        BorrowItem borrowItem = new BorrowItem(device, BorrowedDate, dueDate);
        device.Status = DeviceStatus.Borrowed;

        ((List<BorrowItem>)Items).Add(borrowItem);
    }

    public decimal ReturnDevice(string deviceId, DateTime returnedDate)
    {
        BorrowItem? borrowItem = Items.FirstOrDefault(item => item.Device.DeviceId == deviceId);
        if (borrowItem == null)
        {
            throw new DeviceNotInSlipException(deviceId);
        }

        if (borrowItem.ReturnedDate.HasValue)
        {
            throw new DeviceAlreadyReturnedException(deviceId, borrowItem.ReturnedDate.Value);
        }

        if (returnedDate < borrowItem.BorrowedDate)
        {
            throw new ReturnBeforeReturnDateException(Borrower.BorrowerId, returnedDate, borrowItem.BorrowedDate);
        }

        borrowItem.ReturnedDate = returnedDate;

        decimal lateFee = 0;
        if (returnedDate > borrowItem.DueDate)
        {
            var lateDuration = returnedDate - borrowItem.DueDate;
            lateFee = (decimal)lateDuration.Days * 10;
        }

        borrowItem.Device.Status = DeviceStatus.Available;
        Console.WriteLine($"Device {deviceId} returned. Number of days late: {lateFee / 10}. Late fee: {lateFee}");
        return lateFee;
    }

    public decimal CalculateTotalLateFee()
    {
        decimal totalLateFee = 0;
        foreach (var item in Items)
        {
            if (item.ReturnedDate.HasValue && item.ReturnedDate.Value > item.DueDate)
            {
                var lateDuration = item.ReturnedDate.Value - item.DueDate;
                totalLateFee += (decimal)lateDuration.Days * 10;
            }
        }
        return totalLateFee;
    }

    public List<BorrowItem> NotReturnedOrLateDevices()
    {

        List<BorrowItem> lateOrNotReturnedItems = new List<BorrowItem>();
        foreach (var item in Items)
        {
            if (!item.ReturnedDate.HasValue || item.ReturnedDate.Value > item.DueDate)
            {
                lateOrNotReturnedItems.Add(item);
            }
        }
        return lateOrNotReturnedItems;
    }




    ~BorrowingSlip()
    {
        Console.WriteLine($"Destructor BorrowingSlip.");
    }
}
