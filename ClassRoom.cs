using System;
using System.Collections.Generic;

namespace QuanLyThietBi
{
    // Phong hoc chua danh sach thiet bi (quan he ket tap)
    public class ClassRoom
    {
        public string RoomCode { get; }
        public string RoomName { get; }
        public int Capacity { get; }

        private List<Device> devices; // danh sach thiet bi trong phong

        public ClassRoom(string roomCode, string roomName, int capacity)
        {
            RoomCode = roomCode;
            RoomName = roomName;
            Capacity = capacity;
            devices = new List<Device>();
        }

        // them mot thiet bi vao phong
        public void AddDevice(Device device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device), "Thiet bi khong duoc null");

            // TODO: chua kiem tra ma thiet bi trung nhau, de sau xu ly
            devices.Add(device);
        }

        // xoa thiet bi theo ma, tra ve true neu xoa duoc
        public bool RemoveDevice(string deviceId)
        {
            Device device = FindDevice(deviceId);
            if (device == null)
                return false;

            devices.Remove(device);
            return true;
        }

        // tim thiet bi theo ma, tra ve null neu khong co
        public Device FindDevice(string deviceId)
        {
            foreach (Device device in devices)
            {
                if (device.Code == deviceId)
                    return device;
            }
            return null;
        }

        // tong chi phi bao tri 1 nam cua ca phong (dung tinh da hinh)
        public decimal CalculateAnnualMaintenanceCost()
        {
            decimal total = 0;
            foreach (Device device in devices)
            {
                // goi phuong thuc cua lop con, khong dung if/switch de kiem tra loai
                total += device.CalculateAnnualMaintenanceCost();
            }
            return total;
        }

        // cac thiet bi can bao tri: dang bao tri HOAC su dung hon 5 nam
        public List<Device> GetDevicesRequiringMaintenance()
        {
            List<Device> result = new List<Device>();
            foreach (Device device in devices)
            {
                if (device.Status == DeviceStatus.UnderMaintenance || device.YearsUsed() > 5)
                    result.Add(device);
            }
            return result;
        }
    }
}