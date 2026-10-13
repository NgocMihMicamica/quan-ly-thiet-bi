namespace QuanLyThietBi
{
    // Dieu khien may chieu
    public class ProjectorController : Device
    {
        // dieu khien may chieu duoc muon toi da 3 ngay
        public override int MaxBorrowDays => 3;

        public ProjectorController(string code, string name, int yearOfUse, decimal purchasePrice)
            : base(code, name, yearOfUse, purchasePrice)
        {
        }

        // may chieu dat tien nen bao tri ton 8% gia mua + 200.000 dong moi nam su dung
        public override decimal CalculateAnnualMaintenanceCost()
        {
            return PurchasePrice * 0.08m + 200000m * YearsUsed();
        }
    }
}