using System;
using System.Collections.Generic;

namespace QuanLyThietBi
{
    // Phieu muon: chua nhieu BorrowItem cua mot nguoi muon
    public class BorrowingSlip
    {
        public string SlipId { get; }
        public IBorrower Borrower { get; }
        public string TeachingRoom { get; }
        public DateTime BorrowedDate { get; }

        private List<BorrowItem> items; // danh sach thiet bi trong phieu

        public BorrowingSlip(string slipId, IBorrower borrower, string teachingRoom, DateTime borrowedDate)
        {
            if (string.IsNullOrWhiteSpace(slipId))
                throw new ArgumentException("SlipId khong duoc rong");

            SlipId = slipId;
            Borrower = borrower ?? throw new ArgumentNullException(nameof(borrower));
            TeachingRoom = teachingRoom ?? throw new ArgumentNullException(nameof(teachingRoom));
            BorrowedDate = borrowedDate;
            items = new List<BorrowItem>();
        }

        public IReadOnlyList<BorrowItem> Items
        {
            get { return items; }
        }

        // them thiet bi vao phieu
        public void AddDevice(Device device, int borrowDays)
        {
            // kiem tra truoc khi thay doi gi (dam bao tinh nhat quan)
            ValidateBorrowing(device, borrowDays);

            // tao item mới
            BorrowItem item = new BorrowItem(device, BorrowedDate, BorrowedDate.AddDays(borrowDays));

            // chuyen thiet bi sang Borrowed roi them vao phieu
            device.MarkAsBorrowed();
            items.Add(item);
        }

        // kiem tra cac dieu kien de duoc muon
        private void ValidateBorrowing(Device device, int borrowDays)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device), "Thiet bi khong duoc null");

            if (borrowDays <= 0)
                throw new ArgumentException("So ngay muon phai lon hon 0");

            // TODO: con chua kiem tra so ngay vuot gioi han, trung thiet bi,
            //       gioi han nguoi muon va trang thai Available.
            //       (de commit sau se bo sung, hien tai? chay tam)
        }

        // tra thiet bi, tra ve so tien phat neu tra tre
        public decimal ReturnDevice(string deviceId, DateTime returnedDate)
        {
            // tim BorrowItem theo ma thiet bi
            BorrowItem item = null;
            foreach (BorrowItem b in items)
            {
                if (b.Device.Code == deviceId)
                {
                    item = b;
                    break;
                }
            }

            if (item == null)
                throw new DeviceNotInSlipException(deviceId);

            // goi Return() se bat loi tra 2 lan va ngay tra som hon ngay muon
            item.Return(returnedDate);

            // TODO: quen chuyen thiet bi ve Available sau khi tra, can sua lai

            return item.CalculateLateFee();
        }

        // tong phi tra muon cua ca phieu
        public decimal CalculateTotalLateFee()
        {
            decimal total = 0;
            foreach (BorrowItem item in items)
            {
                total += item.CalculateLateFee();
            }
            return total;
        }
    }
}