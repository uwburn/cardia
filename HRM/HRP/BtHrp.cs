using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Devices.Enumeration.Pnp;
using Windows.Storage.Streams;

namespace MGT.HRM.HRP
{
    public class BtHrp : HeartRateMonitor
    {
        private DeviceInformation device;
        private String deviceContainerId;

        private GattDeviceService service;
        private GattCharacteristic characteristic;
        private Guid CHARACTERISTIC_UUID = GattCharacteristicUuids.HeartRateMeasurement;
        // Heart Rate devices typically have only one Heart Rate Measurement characteristic.
        // Make sure to check your device's documentation to find out how many characteristics your specific device has.
        private const int CHARACTERISTIC_INDEX = 0;
        private const GattClientCharacteristicConfigurationDescriptorValue CHARACTERISTIC_NOTIFICATION_TYPE = GattClientCharacteristicConfigurationDescriptorValue.Notify;

        private PnpObjectWatcher watcher;

        private const byte HEART_RATE_VALUE_FORMAT = 0x01;
        private const byte ENERGY_EXPANDED_STATUS = 0x08;

        public override string Name
        {
            get { return "Bluetooth Smart HRP"; }
        }

        private static TimeSpan START_TIMEOUT = new TimeSpan(0, 0, 10);
        private static TimeSpan RUN_TIMEOUT = new TimeSpan(0, 0, 30);
        private Timer timeoutTimer = new Timer(1000);
        private DateTime lastReceivedDate;

        public BtHrp()
        {
            Running = false;

            heartRateSmoothing = new int?[1];

            TotalPackets = 0;
            CorruptedPackets = 0;
            HeartBeats = 0;

            timeoutTimer.Elapsed += timeoutTimer_Elapsed;
        }

        private BtHrpPacket lastPacket;
        public override IHRMPacket LastPacket
        {
            get
            {
                return lastPacket;
            }
            protected set
            {
                lastPacket = (BtHrpPacket)value;
            }
        }

        private BtHrpPacket secondLastPacket;

        public override int TotalPackets { get; protected set; }
        public override int CorruptedPackets { get; protected set; }

        // Processed data
        public override int HeartBeats { get; protected set; }

        public override byte? MinHeartRate { get; protected set; }
        public override byte? MaxHeartRate { get; protected set; }

        private int?[] heartRateSmoothing;
        public override int HeartRateSmoothingFactor
        {
            get
            {
                return heartRateSmoothing.Length;
            }

            set
            {
                if (Running)
                    throw new Exception();

                if (value < 1)
                    value = 1;

                heartRateSmoothing = new int?[value];
            }
        }
        public override double SmoothedHeartRate { get; protected set; }

        public override bool Running { get; protected set; }

        public DeviceInformation Device
        {
            get
            {
                return device;
            }
            set
            {
                DeviceInformation backup = Device;

                device = value;

                deviceContainerId = "{" + device.Properties["System.Devices.ContainerId"] + "}";

                if (backup.Equals(value))
                    if (DeviceChanged != null)
                        DeviceChanged(this, value);
            }
        }

        public delegate void DeviceChangedEventHandler(object sender, DeviceInformation device);
        public event DeviceChangedEventHandler DeviceChanged;

        public override async void Start()
        {
            if (Running)
                return;

            lastReceivedDate = DateTime.Now;

            try
            {
                service = await GattDeviceService.FromIdAsync(device.Id);
                if (service != null)
                {
                    await ConfigureServiceForNotificationsAsync();
                }
                else
                {
                    FireTimeout("Bluetooth HRP device initialization failed");
                    throw new Exception("Bluetooth HRP device initialization failed");
                }
            }
            catch
            { }

            timeoutTimer.Start();
            Running = true;
        }

        private async Task ConfigureServiceForNotificationsAsync()
        {
            try
            {
                // Obtain the characteristic for which notifications are to be received
                characteristic = service.GetCharacteristics(CHARACTERISTIC_UUID)[CHARACTERISTIC_INDEX];

                // While encryption is not required by all devices, if encryption is supported by the device,
                // it can be enabled by setting the ProtectionLevel property of the Characteristic object.
                // All subsequent operations on the characteristic will work over an encrypted link.
                characteristic.ProtectionLevel = GattProtectionLevel.EncryptionRequired;

                // Register the event handler for receiving notifications
                characteristic.ValueChanged += Characteristic_ValueChanged;

                // In order to avoid unnecessary communication with the device, determine if the device is already 
                // correctly configured to send notifications.
                // By default ReadClientCharacteristicConfigurationDescriptorAsync will attempt to get the current
                // value from the system cache and communication with the device is not typically required.
                var currentDescriptorValue = await characteristic.ReadClientCharacteristicConfigurationDescriptorAsync();

                if ((currentDescriptorValue.Status != GattCommunicationStatus.Success) ||
                    (currentDescriptorValue.ClientCharacteristicConfigurationDescriptor != CHARACTERISTIC_NOTIFICATION_TYPE))
                {
                    // Set the Client Characteristic Configuration Descriptor to enable the device to send notifications
                    // when the Characteristic value changes
                    GattCommunicationStatus status =
                        await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                        CHARACTERISTIC_NOTIFICATION_TYPE);

                    if (status == GattCommunicationStatus.Unreachable)
                    {
                        // Register a PnpObjectWatcher to detect when a connection to the device is established,
                        // such that the application can retry device configuration.
                        StartDeviceConnectionWatcher();
                    }
                }
            }
            catch (Exception e)
            {
                FireTimeout("Bluetooth HRP device initialization failed");
                throw new Exception("Bluetooth HRP device initialization failed");
            }
        }

        private void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            byte[] data = new byte[args.CharacteristicValue.Length];

            DataReader.FromBuffer(args.CharacteristicValue).ReadBytes(data);

            ProcessData(data, args.Timestamp);
        }

        private void ProcessData(byte[] data, DateTimeOffset timestamp)
        {
            byte currentOffset = 0;
            byte flags = data[currentOffset];
            bool isHeartRateValueSizeLong = ((flags & HEART_RATE_VALUE_FORMAT) != 0);
            bool hasEnergyExpended = ((flags & ENERGY_EXPANDED_STATUS) != 0);

            currentOffset++;

            ushort heartRateMeasurementValue = 0;

            if (isHeartRateValueSizeLong)
            {
                heartRateMeasurementValue = (ushort)((data[currentOffset + 1] << 8) + data[currentOffset]);
                currentOffset += 2;
            }
            else
            {
                heartRateMeasurementValue = data[currentOffset];
                currentOffset++;
            }

            ushort expendedEnergyValue = 0;

            if (hasEnergyExpended)
            {
                expendedEnergyValue = (ushort)((data[currentOffset + 1] << 8) + data[currentOffset]);
                currentOffset += 2;
            }

            BtHrpPacket btHrpPacket = new BtHrpPacket
            {
                HeartRate = heartRateMeasurementValue,
                HasExpendedEnergy = hasEnergyExpended,
                ExpendedEnergy = expendedEnergyValue,
                Timestamp = timestamp
            };

            secondLastPacket = lastPacket;
            lastPacket = btHrpPacket;
        }

        private void ProcessPackets()
        {
            // Smoothed values computation
            if (heartRateSmoothing[0] == null)
            {
                for (int i = 0; i < heartRateSmoothing.Length; i++)
                {
                    heartRateSmoothing[i] = lastPacket.HeartRate;
                }

                SmoothedHeartRate = lastPacket.HeartRate;
            }
            else
            {
                int?[] shiftedArray = new int?[heartRateSmoothing.Length];
                Array.Copy(heartRateSmoothing, 0, shiftedArray, 1, heartRateSmoothing.Length - 1);
                heartRateSmoothing = shiftedArray;
                heartRateSmoothing[0] = lastPacket.HeartRate;

                double d = 0;
                for (int i = 0; i < heartRateSmoothing.Length; i++)
                {
                    d += heartRateSmoothing[i] ?? 0;
                }
                SmoothedHeartRate = d / heartRateSmoothing.Length;
            }

            // Computation across multiple packets
            if (MinHeartRate == null && lastPacket.HeartRate > 30)
                MinHeartRate = (byte)lastPacket.HeartRate;
            else if (lastPacket.HeartRate < MinHeartRate && lastPacket.HeartRate > 30)
                MinHeartRate = (byte)lastPacket.HeartRate;

            if (MaxHeartRate == null && lastPacket.HeartRate < 240)
                MaxHeartRate = (byte)lastPacket.HeartRate;
            else if (lastPacket.HeartRate > MaxHeartRate && lastPacket.HeartRate < 240)
                MaxHeartRate = (byte)lastPacket.HeartRate;

            if (secondLastPacket == null)
            {
                HeartBeats = 1;
                return;
            }

            ++HeartBeats;

            lastReceivedDate = DateTime.Now;

            PacketProcessedEventArgs args = new PacketProcessedEventArgs(LastPacket);
            base.FirePacketProcessed(args);
        }

        private void StartDeviceConnectionWatcher()
        {
            watcher = PnpObject.CreateWatcher(PnpObjectType.DeviceContainer,
                new string[] { "System.Devices.Connected" }, String.Empty);

            watcher.Updated += DeviceConnection_Updated;
            watcher.Start();
        }

        private async void DeviceConnection_Updated(PnpObjectWatcher sender, PnpObjectUpdate args)
        {
            var connectedProperty = args.Properties["System.Devices.Connected"];
            bool isConnected = false;
            if ((deviceContainerId == args.Id) && Boolean.TryParse(connectedProperty.ToString(), out isConnected) &&
                isConnected)
            {
                var status = await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                    CHARACTERISTIC_NOTIFICATION_TYPE);

                if (status == GattCommunicationStatus.Success)
                {
                    // Once the Client Characteristic Configuration Descriptor is set, the watcher is no longer required
                    watcher.Stop();
                    watcher = null;
                }
            }
        }

        void timeoutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimeSpan timeout;
            if (Running)
                timeout = RUN_TIMEOUT;
            else
                timeout = START_TIMEOUT;

            TimeSpan diff = DateTime.Now - lastReceivedDate;
            if (diff > timeout)
            {
                Stop();
                FireTimeout("Bluetooth HRP device not transmitting");
            }
        }

        public override async void Stop()
        {
            if (Running)
            {
                Running = false;
                
                timeoutTimer.Stop();

                if (service != null)
                {
                    service.Dispose();
                    service = null;
                }

                if (characteristic != null)
                {
                    characteristic = null;
                }

                if (watcher != null)
                {
                    watcher.Stop();
                    watcher = null;
                }

                DoReset();
            }
        }

        private void DoReset()
        {
            TotalPackets = 0;
            CorruptedPackets = 0;
            HeartBeats = 0;
            MinHeartRate = null;
            MaxHeartRate = null;

            for (int i = 0; i < heartRateSmoothing.Length; i++)
            {
                heartRateSmoothing[i] = null;
            }
            SmoothedHeartRate = 0;
        }

        public override async void Reset()
        {
            Stop();
            Start();
        }

        public override void Dispose()
        {
            if (service != null)
            {
                service.Dispose();
                service = null;
            }

            if (characteristic != null)
            {
                characteristic = null;
            }

            if (watcher != null)
            {
                watcher.Stop();
                watcher = null;
            }
        }

        // Move this in the config form
        public async void ListHRPDevices()
        {
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(
                GattDeviceService.GetDeviceSelectorFromUuid(GattServiceUuids.HeartRate),
                new string[] { "System.Devices.ContainerId" });
        }
    }
}
