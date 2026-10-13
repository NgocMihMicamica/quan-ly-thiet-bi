namespace QuanLyThietBi
{
    // Giang vien duoc muon toi da 3 thiet bi tren 1 phieu
    public class Lecturer : IBorrower
    {
        public string BorrowerId { get; }
        public string FullName { get; }

        public Lecturer(string borrowerId, string fullName)
        {
            BorrowerId = borrowerId;
            FullName = fullName;
        }

        public int GetMaximumDevices()
        {
            return 3;
        }
    }
}