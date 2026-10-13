using System;

namespace QuanLyThietBi
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== HE THONG QUAN LY THIET BI VA PHIEU MUON ===");
            Console.WriteLine();

            TestSuccessFlow();

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();

            TestExceptionFlow();

            Console.WriteLine();
            Console.WriteLine("Nhan phim bat ky de thoat...");
            Console.ReadKey();
        }

        // 11.1 Luong thanh cong
        static void TestSuccessFlow()
        {
            Console.WriteLine("== 11.1 Luong thanh cong ==");

            IBorrower gv = new Lecturer("GV001", "Nguyen Van A");

            Device maychieu = new ProjectorController("MC001", "Dieu khien may chieu", 2023, 1500000m);
            Device dieuhoa = new AirConditionerController("DH001", "Dieu khien dieu hoa", 2022, 800000m);
            Device micro = new Microphone("MIC001", "Micro khong day", 2024, 500000m);

            BorrowingSlip slip = new BorrowingSlip("PM001", gv, "Phong A101", DateTime.Today);

            slip.AddDevice(maychieu, 1);
            slip.AddDevice(micro, 1);
            Console.WriteLine("Da them " + slip.Items.Count + " thiet bi vao phieu PM001.");

            decimal fee1 = slip.ReturnDevice("MC001", DateTime.Today.AddDays(1));
            Console.WriteLine("Tra may chieu dung han -> phi phat: " + fee1 + " dong");

            decimal fee2 = slip.ReturnDevice("MIC001", DateTime.Today.AddDays(1 + 2));
            Console.WriteLine("Tra micro muon 2 ngay -> phi phat: " + fee2 + " dong");

            Console.WriteLine("Tong phi phat cua phieu: " + slip.CalculateTotalLateFee() + " dong");

            Console.WriteLine("Trang thai may chieu sau tra: " + maychieu.Status);
            Console.WriteLine("Trang thai micro sau tra: " + micro.Status);
        }

        // 11.2 Luong phat sinh ngoai le
        static void TestExceptionFlow()
        {
            Console.WriteLine("== 11.2 Luong phat sinh ngoai le ==");

            // Test 1: muon dieu hoa dang UnderMaintenance
            Console.WriteLine();
            Console.WriteLine("[Test 1] Muon dieu hoa dang bao tri");
            Device dieuhoa = new AirConditionerController("DH002", "Dieu khien dieu hoa 2", 2022, 800000m);
            dieuhoa.MarkAsUnderMaintenance();
            BorrowingSlip s1 = new BorrowingSlip("PM1", new Lecturer("GV001", "Nguyen Van A"), "A101", DateTime.Today);
            try
            {
                s1.AddDevice(dieuhoa, 1);
                Console.WriteLine("   KHONG BAT DUOC LOI (dang bao tri van muon duoc)");
            }
            catch (DeviceUnavailableException ex)
            {
                Console.WriteLine("   BAT DUNG LOI: " + ex.Message);
            }

            // Test 2: tro giang muon thiet bi thu 3
            Console.WriteLine();
            Console.WriteLine("[Test 2] Tro giang muon thiet bi thu ba");
            IBorrower tg = new TeachingAssistant("TG001", "Tran Thi B");
            BorrowingSlip s2 = new BorrowingSlip("PM2", tg, "A102", DateTime.Today);
            Device d1 = new Microphone("MIC1", "Micro 1", 2024, 400000m);
            Device d2 = new Microphone("MIC2", "Micro 2", 2024, 400000m);
            Device d3 = new ProjectorController("MC2", "Dieu khien may chieu 2", 2023, 1000000m);
            try
            {
                s2.AddDevice(d1, 1);
                s2.AddDevice(d2, 1);
                s2.AddDevice(d3, 1);
                Console.WriteLine("   KHONG BAT DUOC LOI (tro giang muon 3 thiet bi)");
            }
            catch (BorrowLimitExceededException ex)
            {
                Console.WriteLine("   BAT DUNG LOI: " + ex.Message);
            }

            // Test 3: them cung mot micro hai lan
            Console.WriteLine();
            Console.WriteLine("[Test 3] Them cung mot micro hai lan");
            Device micro = new Microphone("MIC3", "Micro 3", 2024, 500000m);
            BorrowingSlip s3 = new BorrowingSlip("PM3", new Lecturer("GV002", "Le Van C"), "A103", DateTime.Today);
            try
            {
                s3.AddDevice(micro, 1);
                s3.AddDevice(micro, 1);
                Console.WriteLine("   KHONG BAT DUOC LOI (them 2 lan cung 1 thiet bi)");
            }
            catch (DuplicateDeviceException ex)
            {
                Console.WriteLine("   BAT DUNG LOI: " + ex.Message);
            }

            // Test 4: muon micro 3 ngay (gioi han 1 ngay)
            Console.WriteLine();
            Console.WriteLine("[Test 4] Muon micro 3 ngay (gioi han 1 ngay)");
            Device micro2 = new Microphone("MIC4", "Micro 4", 2024, 500000m);
            BorrowingSlip s4 = new BorrowingSlip("PM4", new Lecturer("GV003", "Pham Van D"), "A104", DateTime.Today);
            try
            {
                s4.AddDevice(micro2, 3);
                Console.WriteLine("   KHONG BAT DUOC LOI (muon 3 ngay khi gioi han 1)");
            }
            catch (InvalidBorrowPeriodException ex)
            {
                Console.WriteLine("   BAT DUNG LOI: " + ex.Message);
            }

            // Test 5: tra thiet bi khong thuoc phieu
            Console.WriteLine();
            Console.WriteLine("[Test 5] Tra thiet bi khong thuoc phieu");
            Device maychieu = new ProjectorController("MC5", "Dieu khien may chieu 5", 2023, 1000000m);
            BorrowingSlip s5 = new BorrowingSlip("PM5", new Lecturer("GV004", "Hoang Van E"), "A105", DateTime.Today);
            s5.AddDevice(maychieu, 1);
            try
            {
                s5.ReturnDevice("MA_KHONG_TON_TAI", DateTime.Today);
                Console.WriteLine("   KHONG BAT DUOC LOI");
            }
            catch (DeviceNotInSlipException ex)
            {
                Console.WriteLine("   BAT DUNG LOI: " + ex.Message);
            }

            // Test 6: tra cung thiet bi hai lan
            Console.WriteLine();
            Console.WriteLine("[Test 6] Tra cung thiet bi hai lan");
            Device micro5 = new Microphone("MIC5", "Micro 5", 2024, 500000m);
            BorrowingSlip s6 = new BorrowingSlip("PM6", new Lecturer("GV005", "Nguyen Van F"), "A106", DateTime.Today);
            s6.AddDevice(micro5, 1);
            s6.ReturnDevice("MIC5", DateTime.Today.AddDays(1));
            try
            {
                s6.ReturnDevice("MIC5", DateTime.Today.AddDays(2));
                Console.WriteLine("   KHONG BAT DUOC LOI");
            }
            catch (DeviceAlreadyReturnedException ex)
            {
                Console.WriteLine("   BAT DUNG LOI: " + ex.Message);
            }

            // Test 7: tra voi ngay tra truoc ngay muon
            Console.WriteLine();
            Console.WriteLine("[Test 7] Tra voi ngay tra truoc ngay muon");
            Device micro6 = new Microphone("MIC6", "Micro 6", 2024, 500000m);
            BorrowingSlip s7 = new BorrowingSlip("PM7", new Lecturer("GV006", "Nguyen Van G"), "A107", DateTime.Today);
            s7.AddDevice(micro6, 1);
            try
            {
                s7.ReturnDevice("MIC6", DateTime.Today.AddDays(-1));
                Console.WriteLine("   KHONG BAT DUOC LOI");
            }
            catch (InvalidReturnDateException ex)
            {
                Console.WriteLine("   BAT DUNG LOI: " + ex.Message);
            }
        }
    }
}