using System;
using System.Collections.Generic;

namespace QuanLyThietBi
{
    public class ClassRoom
    {
        public string RoomCode { get; }
        public string RoomName { get; }
        public int Capacity { get; }

        private List<Device> devices;

        public ClassRoom(string roomCode, string roomName, int capacity)
        {
            RoomCode = roomCode;
            RoomName = roomName;
            Capacity = capacity;
            devices = new List<Device>();
        }

        public void AddDevice(Device device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device), "Thiet bi khong duoc null");

            // FIX: kiem tra ma thiet bi trung truoc khi them
            if (FindDevice(device.Code) != null)
                throw new InvalidOperationException("Thiet bi ma " + device.Code + " da ton tai trong phong");

            devices.Add(device);
        }

        public bool RemoveDevice(string deviceId)
        {
            Device device = FindDevice(deviceId);
            if (device == null)
                return false;

            devices.Remove(device);
            return true;
        }

        public Device FindDevice(string deviceId)
        {
            foreach (Device device in devices)
            {
                if (device.Code == deviceId)
                    return device;
            }
            return null;
        }

        public decimal CalculateAnnualMaintenanceCost()
        {
            decimal total = 0;
            foreach (Device device in devices)
            {
                total += device.CalculateAnnualMaintenanceCost();
            }
            return total;
        }

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