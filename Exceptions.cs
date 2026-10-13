using System;

namespace QuanLyThietBi
{
    // Ngoai le co so cho nghiep vu muon/tra thiet bi
    public class BorrowingException : Exception
    {
        public BorrowingException(string message) : base(message)
        {
        }

        public BorrowingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    // Thiet bi khong o trang thai Available
    public class DeviceUnavailableException : BorrowingException
    {
        public string DeviceId { get; }
        public DeviceStatus CurrentStatus { get; }

        public DeviceUnavailableException(string deviceId, DeviceStatus currentStatus)
            : base("Thiet bi " + deviceId + " khong san sang (trang thai: " + currentStatus + ")")
        {
            DeviceId = deviceId;
            CurrentStatus = currentStatus;
        }
    }

    // Nguoi muon da dat so thiet bi toi da
    public class BorrowLimitExceededException : BorrowingException
    {
        public string BorrowerId { get; }
        public int MaximumDevices { get; }

        public BorrowLimitExceededException(string borrowerId, int maximumDevices)
            : base("Nguoi muon " + borrowerId + " da dat gioi han " + maximumDevices + " thiet bi")
        {
            BorrowerId = borrowerId;
            MaximumDevices = maximumDevices;
        }
    }

    // So ngay muon khong hop le (qua gioi han cua thiet bi)
    public class InvalidBorrowPeriodException : BorrowingException
    {
        public string DeviceId { get; }
        public int RequestedDays { get; }
        public int MaxDays { get; }

        public InvalidBorrowPeriodException(string deviceId, int requestedDays, int maxDays)
            : base("Thiet bi " + deviceId + " yeu cau muon " + requestedDays + " ngay nhung toi da la " + maxDays + " ngay")
        {
            DeviceId = deviceId;
            RequestedDays = requestedDays;
            MaxDays = maxDays;
        }
    }

    // Cung mot thiet bi duoc them hai lan vao phieu
    public class DuplicateDeviceException : BorrowingException
    {
        public DuplicateDeviceException(string deviceId)
            : base("Thiet bi " + deviceId + " da co trong phieu roi")
        {
        }
    }

    // Thiet bi khong thuoc phieu muon
    public class DeviceNotInSlipException : BorrowingException
    {
        public DeviceNotInSlipException(string deviceId)
            : base("Thiet bi " + deviceId + " khong thuoc phieu muon nay")
        {
        }
    }

    // Thiet bi da duoc tra truoc do
    public class DeviceAlreadyReturnedException : BorrowingException
    {
        public DeviceAlreadyReturnedException(string deviceId)
            : base("Thiet bi " + deviceId + " da duoc tra truoc do roi")
        {
        }
    }

    // Ngay tra khong hop le
    public class InvalidReturnDateException : BorrowingException
    {
        public InvalidReturnDateException(string message) : base(message)
        {
        }
    }
}