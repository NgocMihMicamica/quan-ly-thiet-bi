using System;

namespace QuanLyThietBi
{
    // Lop truu tuong dai dien cho moi thiet bi
    public abstract class Device
    {
        public string Code { get; }          // ma thiet bi, chi set o constructor
        public string Name { get; }          // ten thiet bi
        public int YearOfUse { get; }        // nam dua vao su dung
        public decimal PurchasePrice { get; } // gia mua
        public DeviceStatus Status { get; private set; }

        // so ngay duoc muon toi da (moi loai thiet bi khac nhau)
        public abstract int MaxBorrowDays { get; }

        public Device(string code, string name, int yearOfUse, decimal purchasePrice)
        {
            // kiem tra du lieu dau vao
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Ma thiet bi khong duoc rong");

            if (purchasePrice <= 0)
                throw new ArgumentException("Gia mua phai lon hon 0");

            if (yearOfUse > DateTime.Now.Year)
                throw new ArgumentException("Nam dua vao su dung khong duoc lon hon nam hien tai");

            Code = code;
            Name = name;
            YearOfUse = yearOfUse;
            PurchasePrice = purchasePrice;
            Status = DeviceStatus.Available;
        }

        // phuong thuc truu tuong: tinh chi phi bao tri du kien 1 nam
        public abstract decimal CalculateAnnualMaintenanceCost();

        // so nam da su dung tinh den hien tai
        public int YearsUsed()
        {
            return DateTime.Now.Year - YearOfUse;
        }

        // cac ham doi trang thai (de dung o lop nghiep vu)
        public void MarkAsBorrowed()
        {
            Status = DeviceStatus.Borrowed;
        }

        public void MarkAsAvailable()
        {
            Status = DeviceStatus.Available;
        }

        public void MarkAsUnderMaintenance()
        {
            Status = DeviceStatus.UnderMaintenance;
        }

        // ghi de de in thong tin thiet bi ra chuoi
        public override string ToString()
        {
            return "Ma: " + Code +
                   ", Ten: " + Name +
                   ", Nam: " + YearOfUse +
                   ", Gia: " + PurchasePrice +
                   ", Trang thai: " + Status;
        }
    }
}