namespace QuanLyThietBi
{
    // Micro (khong day)
    public class Microphone : Device
    {
        // micro de hong nen chi duoc muon 1 ngay
        public override int MaxBorrowDays => 1;

        public Microphone(string code, string name, int yearOfUse, decimal purchasePrice)
            : base(code, name, yearOfUse, purchasePrice)
        {
        }

        // bao tri micro ton khoang 3% gia mua + 50.000 dong moi nam su dung
        public override decimal CalculateAnnualMaintenanceCost()
        {
            return PurchasePrice * 0.03m + 50000m * YearsUsed();
        }
    }
}