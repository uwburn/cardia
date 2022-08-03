using log4net;
using System;
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
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private DeviceInformation device;
        private String deviceContainerId;

        private GattDeviceService service;
        private GattCharacteristic characteristic;
        private PnpObjectWatcher watcher;
        private const GattClientCharacteristicConfigurationDescriptorValue CHARACTERISTIC_NOTIFICATION_TYPE = GattClientCharacteristicConfigurationDescriptorValue.Notify;
        // Heart Rate devices typically have only one Heart Rate Measurement characteristic.
        // Make sure to check your device's documentation to find out how many characteristics your specific device has.
        private int characteristicIndex = 0;

        public delegate void CharacteristicIndexChangedEventHandler(object sender, int characteristicIndex);
        public event CharacteristicIndexChangedEventHandler CharacteristicIndexChanged;

        public int CharacteristicIndex
        {
            get
            {
                return characteristicIndex;
            }
            set
            {
                if (Running)
                    throw new Exception();

                int backup = characteristicIndex;

                characteristicIndex = value;

                if (backup != value)
                    if (CharacteristicIndexChanged != null)
                        CharacteristicIndexChanged(this, value);
            }
        }

        private int initDelay = 500;

        public delegate void InitDelayChangedEventHandler(object sender, int delay);
        public event InitDelayChangedEventHandler InitDelayChanged;

        public int InitDelay
        {
            get
            {
                return initDelay;
            }
            set
            {
                if (Running)
                    throw new Exception();

                int backup = initDelay;

                initDelay = value;

                if (backup != value)
                    if (InitDelayChanged != null)
                        InitDelayChanged(this, value);
            }
        }

        private const byte HEART_RATE_VALUE_FORMAT = 0x01;
        private const byte ENERGY_EXPANDED_STATUS = 0x08;

        public override string Name
        {
            get { return "Bluetooth Smart HRP"; }
        }

        private static TimeSpan START_TIMEOUT = new TimeSpan(0, 0, 10);
        private static TimeSpan RUN_TIMEOUT = new TimeSpan(0, 0, 30);
        private System.Timers.Timer timeoutTimer = new System.Timers.Timer(1000);
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

                if (backup == null && value == null)
                    return;

                if ((backup == null && value != null) || !backup.Equals(value))
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

#if DEBUG
            logger.Debug("Starting HRP");
#endif

            lastReceivedDate = DateTime.Now;

            timeoutTimer.Start();
            Running = true;

            await ConfigureServiceForNotificationsAsync();
        }

        private async Task ConfigureServiceForNotificationsAsync()
        {          
            try
            {
#if DEBUG
                logger.Debug($"Getting GattDeviceService {device.Name} with id {device.Id}");
#endif
                service = await GattDeviceService.FromIdAsync(device.Id);
                if (initDelay > 0)
                    await Task.Delay(initDelay);

#if DEBUG
                if (service != null)
                {
                    logger.Debug($"GattDeviceService instatiated successfully");

                    logger.Debug($"GattSession status = {service.Session.SessionStatus}, " +
                    $"mantain connection = {service.Session.MaintainConnection}, " +
                    $"can mantain connection = {service.Session.MaintainConnection}");
                }
                else
                {
                    logger.Debug($"Failed to instantiate GattDeviceService");
                }
#endif

                // List all the characteristics of the device
#if DEBUG
                logger.Debug("Getting all GattCharacteristic...");
                GattCharacteristicsResult allResult = await service.GetCharacteristicsAsync();
                logger.Debug($"GattCharacteristicsResult status {allResult.Status}");
                foreach (GattCharacteristic allCharacteristic in allResult.Characteristics)
                {
                    logger.Debug($"GattCharacteristic {allCharacteristic.Uuid}: " +
                        $"description = {allCharacteristic.UserDescription}, " +
                        $"protection level = {allCharacteristic.ProtectionLevel}");
                }
#endif

                // Obtain the characteristic for which notifications are to be received
#if DEBUG
                logger.Debug($"Getting HeartRateMeasurement GattCharacteristic {characteristicIndex}");
#endif
                GattCharacteristicsResult result = await service.GetCharacteristicsForUuidAsync(GattCharacteristicUuids.HeartRateMeasurement);

#if DEBUG
                logger.Debug($"GattCharacteristicsResult status {result.Status}");
                foreach (GattCharacteristic hrCharacteristic in result.Characteristics)
                {
                    logger.Debug($"GattCharacteristic {hrCharacteristic.Uuid}: " +
                        $"description = {hrCharacteristic.UserDescription}, " +
                        $"protection level = {hrCharacteristic.ProtectionLevel}");
                }
#endif

                characteristic = result.Characteristics[characteristicIndex];

                // While encryption is not required by all devices, if encryption is supported by the device,
                // it can be enabled by setting the ProtectionLevel property of the Characteristic object.
                // All subsequent operations on the characteristic will work over an encrypted link.
#if DEBUG
                logger.Debug("Setting EncryptionRequired protection level on GattCharacteristic");
#endif
                characteristic.ProtectionLevel = GattProtectionLevel.EncryptionRequired;

                // Register the event handler for receiving notifications
                if (initDelay > 0)
                    await Task.Delay(initDelay);
#if DEBUG
                logger.Debug("Registering event handler onction level on GattCharacteristic");
#endif
                characteristic.ValueChanged += Characteristic_ValueChanged;

                // Set the Client Characteristic Configuration Descriptor to enable the device to send notifications
                // when the Characteristic value changes
#if DEBUG
                logger.Debug("Setting GattCharacteristic configuration descriptor to enable notifications");
#endif

                GattCommunicationStatus status =
                    await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                    CHARACTERISTIC_NOTIFICATION_TYPE);

                if (status == GattCommunicationStatus.Unreachable)
                {
#if DEBUG
                    logger.Debug("Device unreachable");
#endif
                    // Register a PnpObjectWatcher to detect when a connection to the device is established,
                    // such that the application can retry device configuration.
                    StartDeviceConnectionWatcher();
                }
#if DEBUG
                else if (status == GattCommunicationStatus.Success)
                {
                    logger.Debug("Configuration successfull");

                    logger.Debug($"GattSession status = {service.Session.SessionStatus}, " +
                    $"mantain connection = {service.Session.MaintainConnection}, " +
                    $"can mantain connection = {service.Session.MaintainConnection}");
                }
#endif
            }
            catch (Exception e)
            {
#if DEBUG
                logger.Warn("Error configuring HRP device", e);
#endif

                Stop();
                FireTimeout("Bluetooth HRP device initialization failed");
                //throw new Exception("Bluetooth HRP device initialization failed");
            }
        }

        private void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
#if DEBUG
            logger.Debug($"GattCharacteristic value changed, args = {args}");
#endif
            byte[] data = new byte[args.CharacteristicValue.Length];

            DataReader.FromBuffer(args.CharacteristicValue).ReadBytes(data);

            ProcessData(data, args.Timestamp);
        }

        private void ProcessData(byte[] data, DateTimeOffset timestamp)
        {
#if DEBUG
            logger.Debug($"Processing HRP payload, data = {data}");
#endif

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

#if DEBUG
            logger.Debug($"Constructed HRP packet = {btHrpPacket}");
#endif

            TotalPackets++;

            secondLastPacket = lastPacket;
            lastPacket = btHrpPacket;

            ProcessPackets();
        }

        private void ProcessPackets()
        {
#if DEBUG
            logger.Debug("Processing HRP packets");
#endif

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

#if DEBUG
            logger.Debug($"Firing PacketProcessed event, packet = {LastPacket}");
#endif
            PacketProcessedEventArgs args = new PacketProcessedEventArgs(LastPacket);
            base.FirePacketProcessed(args);
        }

        private void StartDeviceConnectionWatcher()
        {
            watcher = PnpObject.CreateWatcher(PnpObjectType.DeviceContainer,
                new string[] { "System.Devices.Connected" }, String.Empty);

#if DEBUG
            logger.Debug("Registering device connection watcher updated event handler");
#endif
            watcher.Updated += DeviceConnection_Updated;

#if DEBUG
            logger.Debug("Starting device connection watcher");
#endif
            watcher.Start();
        }

        private async void DeviceConnection_Updated(PnpObjectWatcher sender, PnpObjectUpdate args)
        {
#if DEBUG
            logger.Debug($"Device connection updated, args = {args}");
#endif

            var connectedProperty = args.Properties["System.Devices.Connected"];

#if DEBUG
            logger.Debug($"Connected property, args = {connectedProperty}");
#endif

            bool isConnected = false;
            if ((deviceContainerId == args.Id) && Boolean.TryParse(connectedProperty.ToString(), out isConnected) &&
                isConnected)
            {
                var status = await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                    CHARACTERISTIC_NOTIFICATION_TYPE);

                if (status == GattCommunicationStatus.Success)
                {
#if DEBUG
                    logger.Debug("Stopping device connection watcher");
#endif

                    // Once the Client Characteristic Configuration Descriptor is set, the watcher is no longer required
                    watcher.Stop();
                    watcher = null;
#if DEBUG
                    logger.Debug("Configuration successfull");
#endif
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
#if DEBUG
                if (Running)
                    logger.Debug("Communication timeout elapsed");
                else
                    logger.Debug("Start timeout elapsed");
#endif

                Stop();
                FireTimeout("Bluetooth HRP device not transmitting");
            }
        }

        public override void Stop()
        {
            if (Running)
            {
#if DEBUG
                logger.Debug("Stopping HRP");
#endif

                Running = false;

#if DEBUG
                logger.Debug("Stopping timeout timer");
#endif
                timeoutTimer.Stop();

                if (characteristic != null)
                {
#if DEBUG
                    logger.Debug("Clearing GattCharacteristic");
#endif
                    characteristic.ValueChanged -= Characteristic_ValueChanged;
                    characteristic = null;
                }

                if (watcher != null)
                {
#if DEBUG
                    logger.Debug("Clearing device changed watcher");
#endif
                    watcher.Stop();
                    watcher = null;
                }

                if (service != null)
                {
#if DEBUG
                    logger.Debug("Clearing GattDeviceService");
#endif
                    service.Dispose();
                    service = null;
                }

#if DEBUG
                logger.Debug("Resetting counters");
#endif
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

        public override void Reset()
        {
#if DEBUG
            logger.Debug("Resetting HRP");
#endif

            Stop();
            Start();
        }

        public override void Dispose()
        {
            if (characteristic != null)
            {
                characteristic.ValueChanged -= Characteristic_ValueChanged;
                characteristic = null;
            }

            if (watcher != null)
            {
                watcher.Stop();
                watcher = null;
            }

            if (service != null)
            {
                service.Dispose();
                service = null;
            }
        }
    }
}
