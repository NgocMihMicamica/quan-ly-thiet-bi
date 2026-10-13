namespace QuanLyThietBi
{
    // Nguoi muon thiet bi, ai muon cung phai co thong tin nay
    public interface IBorrower
    {
        string BorrowerId { get; }
        string FullName { get; }
        int GetMaximumDevices(); // so thiet bi duoc muon toi da tren 1 phieu
    }
}