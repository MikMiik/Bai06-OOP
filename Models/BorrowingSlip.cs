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

    public BorrowingSlip(string slipId, IBorrower borrower, string teachingRoom, DateTime borrowedDate, IReadOnlyList<BorrowItem> items)
    {
        SlipId = slipId;
        Borrower = borrower;
        TeachingRoom = teachingRoom;
        BorrowedDate = borrowedDate;
        Items = items;
    }

    public void AddDevice(Device device, int borrowDays)
    {
        if (Items.Count >= Borrower.GetMaximumDevices())
        {
            throw new InvalidOperationException("Số lượng thiết bị mượn vượt quá giới hạn cho phép.");
        }

        DateTime dueDate = BorrowedDate.AddDays(borrowDays);
        BorrowItem borrowItem = new BorrowItem(device, BorrowedDate, dueDate);

        ((List<BorrowItem>)Items).Add(borrowItem);
    }

    public decimal ReturnDevice(string deviceId, DateTime returnedDate)
    {
        BorrowItem? borrowItem = Items.FirstOrDefault(item => item.Device.DeviceId == deviceId);
        if (borrowItem == null)
        {
            throw new InvalidOperationException("Thiết bị không tồn tại trong phiếu mượn.");
        }

        borrowItem.ReturnedDate = returnedDate;

        decimal lateFee = 0;
        if (returnedDate > borrowItem.DueDate)
        {
            var lateDuration = returnedDate - borrowItem.DueDate;
            lateFee = (decimal)lateDuration.Days * 10;
        }

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




    ~BorrowingSlip()
    {
        Console.WriteLine($"Destructor BorrowingSlip.");
    }
}
