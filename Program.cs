using Bai06.Models;
using Bai06.Enums;

// 1. Tạo một giảng viên. 
Console.WriteLine("1. Tạo một giảng viên.");
Lecturer lecturer1 = new("L1", "Nguyen Van A");
Lecturer lecturer2 = new("L2", "Nguyen Van D");
// 2. Tạo một điều khiển máy chiếu, một điều khiển điều hòa và một micro không dây
Console.WriteLine("2. Tạo một điều khiển máy chiếu, một điều khiển điều hòa và một micro không dây");
ProjectorRemote projectorRemote1 = new("PR1", "Projector Remote 1", 2024, 100000, 5, "HDMI");
ACRemote acRemote1 = new("AC1", "AC Remote 1", 2023, 500000, 3, ACRemoteMode.Cool, ACRemoteSwingMode.Off);
ACRemote acRemote2 = new("AC2", "AC Remote 2", 2020, 200000, 1, ACRemoteMode.Cool, ACRemoteSwingMode.Off);
Micro microphone1 = new("M1", "Microphone 1", 2022, 200000, 4, false);
// 3. Tạo phiếu mượn cho một phòng dạy. 
Console.WriteLine("3. Tạo phiếu mượn cho một phòng dạy.");
BorrowingSlip borrowingSlip1 = new("BS1", lecturer1, "Room 101", DateTime.Now);
// 4. Thêm điều khiển máy chiếu và micro vào phiếu. 
Console.WriteLine("4. Thêm điều khiển máy chiếu và micro vào phiếu.");
borrowingSlip1.AddDevice(projectorRemote1, 7);
borrowingSlip1.AddDevice(microphone1, 1);
// 5. Trả điều khiển máy chiếu đúng hạn. 
Console.WriteLine("5. Trả điều khiển máy chiếu đúng hạn.");
borrowingSlip1.ReturnDevice("PR1", DateTime.Now.AddDays(6));
// 6. Trả micro muộn 2 ngày.
Console.WriteLine("6. Trả micro muộn 2 ngày.");
borrowingSlip1.ReturnDevice("M1", DateTime.Now.AddDays(7));
// 7. Hiển thị các phiếu trả muộn, chưa trả. 
Console.WriteLine("7. Hiển thị các phiếu trả muộn, chưa trả.");
List<BorrowItem> lateOrNotReturnedItems = borrowingSlip1.NotReturnedOrLateDevices();
Console.WriteLine("Late or Not Returned Devices:");
foreach (var item in lateOrNotReturnedItems)
{
    Console.WriteLine($"Device ID: {item.Device.DeviceId}, Device Name: {item.Device.DeviceName}, Due Date: {item.DueDate}, Returned Date: {(item.ReturnedDate.HasValue ? item.ReturnedDate.Value.ToString() : "Not Returned")}");
}

// Luồng phát sinh ngoại lệ 
// 1. Mượn điều khiển điều hòa đang UnderMaintenance. 
Console.WriteLine("1. Mượn điều khiển điều hòa đang UnderMaintenance.");
acRemote1.Status = DeviceStatus.UnderMaintenance;
try
{
    borrowingSlip1.AddDevice(acRemote1, 3);
}
catch (DeviceUnavailableException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
// 2. Trợ giảng mượn thiết bị thứ ba
Console.WriteLine("2. Trợ giảng mượn thiết bị thứ ba");
TeachingAssistant teachingAssistant1 = new("TA1", "Tro Giang A");
BorrowingSlip borrowingSlip2 = new("BS2", teachingAssistant1, "Room 102", DateTime.Now);
acRemote1.Status = DeviceStatus.Available;
try
{
    borrowingSlip2.AddDevice(projectorRemote1, 5);
    borrowingSlip2.AddDevice(acRemote1, 1);
    borrowingSlip2.AddDevice(microphone1, 4);
}
catch (BorrowLimitExceededException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
// 4. Mượn micro trong 3 ngày dù giới hạn là 1 ngày.
Console.WriteLine("4. Mượn micro trong 3 ngày dù giới hạn là 1 ngày.");
BorrowingSlip borrowingSlip3 = new("BS3", lecturer2, "Room 103", DateTime.Now);
try
{
    microphone1.MaxDaysAllowed = 1;
    borrowingSlip3.AddDevice(microphone1, 3);
}
catch (BorrowDayLimitExceededException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
// 5. Trả một điều khiển máy chiếu không thuộc phiếu.
Console.WriteLine("5. Trả một điều khiển máy chiếu không thuộc phiếu.");
try
{
    borrowingSlip2.ReturnDevice("AC2", DateTime.Now);
}
catch (DeviceNotInSlipException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
// 6. Trả cùng một thiết bị hai lần.
Console.WriteLine("6. Trả cùng một thiết bị hai lần.");
try
{
    borrowingSlip1.ReturnDevice("PR1", DateTime.Now.AddDays(1));
}
catch (DeviceAlreadyReturnedException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
// 7. Trả thiết bị với ngày trả trước ngày mượn
Console.WriteLine("7. Trả thiết bị với ngày trả trước ngày mượn.");
try
{
    borrowingSlip1.AddDevice(acRemote2, 2);
    borrowingSlip1.ReturnDevice("AC2", DateTime.Now.AddDays(-1));
}
catch (ReturnBeforeReturnDateException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

