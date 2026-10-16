namespace Bai06.Models;

using Bai06.Enums;
public class BorrowItem
{
    private Device _device = null!;
    private DateTime _borrowedDate;

    private DateTime _dueDate;
    private int _fromPeriod;
    private int _toPeriod;
    private DateTime? _returnedDate = null;
    private bool _isReturned;

    public Device Device
    {
        get => _device;
        set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value.ToString());
            _device = value;
        }
    }
    public DateTime BorrowedDate
    {
        get => _borrowedDate;
        set => _borrowedDate = value;
    }
    public DateTime DueDate
    {
        get => _dueDate;
        set
        {
            if (value < _borrowedDate)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Ngày trả không được nhỏ hơn ngày mượn.");
            }
            _dueDate = value;
        }
    }

    public int FromPeriod
    {
        get => _fromPeriod;
        set => _fromPeriod = value;

    }

    public int ToPeriod
    {
        get => _toPeriod;
        set
        {
            if (value < _fromPeriod)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Thời gian trả không được nhỏ hơn thời gian mượn.");
            }
            _toPeriod = value;
        }
    }

    public DateTime? ReturnedDate
    {
        get => _returnedDate;
        set
        {
            if (ReturnedDate.HasValue)
            {
                throw new InvalidOperationException("Không được trả một BorrowItem hai lần.");
            }
            if (value < BorrowedDate)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Ngày trả không được trước ngày mượn.");
            }
            _returnedDate = value;
        }
    }

    public bool IsReturned
    {
        get => _isReturned;
        set => _isReturned = value;
    }

    public BorrowItem(Device device, DateTime borrowedDate, DateTime dueDate, int fromPeriod, int toPeriod)
    {
        Device = device;
        BorrowedDate = borrowedDate;
        DueDate = dueDate;
        FromPeriod = fromPeriod;
        ToPeriod = toPeriod;
        IsReturned = false;
    }

    public BorrowItem(Device device, DateTime borrowedDate, DateTime dueDate)
    {
        Device = device;
        BorrowedDate = borrowedDate;
        DueDate = dueDate;
        IsReturned = false;
    }

    public void Return(DateTime returnedDate)
    {
        ReturnedDate = returnedDate;
        IsReturned = true;
    }

    ~BorrowItem()
    {
        Console.WriteLine($"Destructor BorrowItem {Device}.");
    }
}
