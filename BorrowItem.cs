using System;

namespace QuanLyThietBi
{
    // Mot dong thiet bi trong phieu muon
    public class BorrowItem
    {
        public Device Device { get; }          // thiet bi duoc muon
        public DateTime BorrowedDate { get; }  // ngay muon
        public DateTime DueDate { get; }       // ngay den han tra
        public DateTime? ReturnedDate { get; private set; } // ngay tra (null neu chua tra)

        // phi tra muon moi ngay (dong)
        private const decimal LateFeePerDay = 5000m;

        public BorrowItem(Device device, DateTime borrowedDate, DateTime dueDate)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device), "Thiet bi khong duoc null");

            if (dueDate < borrowedDate)
                throw new ArgumentException("Ngay den han phai sau hoac bang ngay muon");

            Device = device;
            BorrowedDate = borrowedDate;
            DueDate = dueDate;
            ReturnedDate = null;
        }

        public bool IsReturned
        {
            get { return ReturnedDate.HasValue; }
        }

        // tra thiet bi (cho nay de kim soat viec tra 2 lan va ngay tra)
        public void Return(DateTime returnedDate)
        {
            if (IsReturned)
                throw new DeviceAlreadyReturnedException(Device.Code);

            if (returnedDate < BorrowedDate)
                throw new InvalidReturnDateException("Ngay tra " + returnedDate.ToString("dd/MM/yyyy") + " som hon ngay muon");

            ReturnedDate = returnedDate;
        }

        // tinh phi tra muon (neu tra tre)
        public decimal CalculateLateFee()
        {
            if (!IsReturned)
                return 0; // chua tra thi chua tinh phi

            int lateDays = (ReturnedDate.Value - DueDate).Days;
            if (lateDays <= 0)
                return 0;

            return lateDays * LateFeePerDay;
        }
    }
}