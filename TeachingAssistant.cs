namespace QuanLyThietBi
{
    // Tro giang duoc muon toi da 2 thiet bi tren 1 phieu
    public class TeachingAssistant : IBorrower
    {
        public string BorrowerId { get; }
        public string FullName { get; }

        public TeachingAssistant(string borrowerId, string fullName)
        {
            BorrowerId = borrowerId;
            FullName = fullName;
        }

        public int GetMaximumDevices()
        {
            return 2;
        }
    }
}