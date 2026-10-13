namespace QuanLyThietBi
{
    // Dieu khien dieu hoa
    public class AirConditionerController : Device
    {
        // dieu khien dieu hoa duoc muon toi da 2 ngay
        public override int MaxBorrowDays => 2;

        public AirConditionerController(string code, string name, int yearOfUse, decimal purchasePrice)
            : base(code, name, yearOfUse, purchasePrice)
        {
        }

        // bao tri dieu hoa ton khoang 5% gia mua + 100.000 dong moi nam su dung
        public override decimal CalculateAnnualMaintenanceCost()
        {
            return PurchasePrice * 0.05m + 100000m * YearsUsed();
        }
    }
}