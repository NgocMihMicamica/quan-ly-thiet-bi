using System;
using System.Collections.Generic;

namespace QuanLyThietBi
{
    public class BorrowingSlip
    {
        public string SlipId { get; }
        public IBorrower Borrower { get; }
        public string TeachingRoom { get; }
        public DateTime BorrowedDate { get; }

        private List<BorrowItem> items;

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

        public void AddDevice(Device device, int borrowDays)
        {
            ValidateBorrowing(device, borrowDays);
            BorrowItem item = new BorrowItem(device, BorrowedDate, BorrowedDate.AddDays(borrowDays));
            device.MarkAsBorrowed();
            items.Add(item);
        }

        private void ValidateBorrowing(Device device, int borrowDays)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device), "Thiet bi khong duoc null");

            if (borrowDays <= 0)
                throw new ArgumentException("So ngay muon phai lon hon 0");

            // FIX lan 1: kiem tra so ngay vuot gioi han cua thiet bi
            if (borrowDays > device.MaxBorrowDays)
                throw new InvalidBorrowPeriodException(device.Code, borrowDays, device.MaxBorrowDays);

            // FIX lan 1: kiem tra thiet bi co dang Available hay khong
            if (device.Status != DeviceStatus.Available)
                throw new DeviceUnavailableException(device.Code, device.Status);

            // FIX lan 1: kiem tra thiet bi da co trong phieu chua (trung)
            foreach (BorrowItem item in items)
            {
                if (item.Device.Code == device.Code)
                    throw new DuplicateDeviceException(device.Code);
            }

            // FIX lan 1: kiem tra nguoi muon da dat gioi han thiet bi chua
            if (items.Count >= Borrower.GetMaximumDevices())
                throw new BorrowLimitExceededException(Borrower.BorrowerId, Borrower.GetMaximumDevices());
        }

        public decimal ReturnDevice(string deviceId, DateTime returnedDate)
        {
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

            item.Return(returnedDate);

            // TODO: van con quen chuyen thiet bi ve Available sau khi tra, de sau fix

            return item.CalculateLateFee();
        }

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