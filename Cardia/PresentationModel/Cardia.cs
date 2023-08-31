using System;
using System.Collections.Generic;
using MGT.ECG_Signal_Generator;
using MGT.HRM;
using MGT.Utilities.Network;
using System.Collections.Concurrent;
using System.Drawing;
using MGT.Utilities.EventHandlers;

namespace MGT.Cardia
{
    public class Cardia : IDisposable
    {
        #region Fields & constants

        // public readonly DateTime ExpiryDate = new DateTime(2018, 12, 01);
        private readonly String networkVersion = "1.1";

        public readonly Configuration configuration;

        #endregion

        #region Constructors & initialization

        public Cardia(Configuration configuration, List<Bundle> bundles)
        {
            this.configuration = configuration;
            this.Bundles = bundles;

            signalGenerator.OnSignalGenerated += signalGenerator_SignalGenerated;
            alarm.AlarmChanged += alarm_AlarmChanged;

            InitializeLoggers();
            InitializeNetworkProvider();
            InitializeColors();
        }

        #endregion

        #region HRM

        // Fields
        private Bundle bundle;
        private const int BUFFER_TIME = 10000;
        private SignalGenerator signalGenerator = new SignalGenerator(BUFFER_TIME);
        private HRMAlarm alarm = new HRMAlarm();

        // Events
        public event GenericEventHandler<Bundle> BundleChanged;
        public event GenericEventHandler<HRMStatus> PacketProcessed;
        public event GenericEventHandler<SignalGeneratedEventArgs> SignalGenerated;
        public event GenericEventHandler<bool> AlarmTripped;

        // Properties
        public List<Bundle> Bundles { get; private set; }
        public Bundle Bundle
        {
            get { return bundle; }
            set
            {
                if (bundle != null)
                    if (bundle.Device.Running)
                        throw new Exception("Cannot change device while current device is running");

                if (bundle != null)
                    bundle.Device.ResetSubscriptions();

                Bundle bck = bundle;
                bundle = value;

                SetLogger();

                if (StatusChanged != null)
                    StatusChanged(this, "Device changed", false);

                if (value != bck)
                {
                    if (bck != null)
                        bck.Device.ResetSubscriptions();

                    RegisterHrmEventHandlers();

                    if (BundleChanged != null)
                        BundleChanged(this, bundle);
                }
            }
        }

        // Methods
        private void RegisterHrmEventHandlers()
        {
            bundle.Device.PacketProcessed += hrm_PacketProcessed;
            bundle.Device.Timeout += Cardia_Timeout;
        }

        void Cardia_Timeout(object sender, string arg)
        {
            DoStop(false, true);

            if (StatusChanged != null)
                StatusChanged(this, arg, true);
        }

        // HRM event handler
        private void hrm_PacketProcessed(object sender, PacketProcessedEventArgs e)
        {
            signalGenerator.BPM = (int)e.HRMPacket.HeartRate;
            alarm.BPM = (int)e.HRMPacket.HeartRate;

            if (logger != null && logger.Running)
            {
                logger.Log(e.HRMPacket);
            }

            if (PacketProcessed != null)
                PacketProcessed(this, new HRMStatus(bundle.Device, e.HRMPacket));
        }

        // Signal generator event handler
        private void signalGenerator_SignalGenerated(object sender, SignalGeneratedEventArgs e)
        {
            if (SignalGenerated != null)
                SignalGenerated(this, e);
        }

        // Alarm event handler
        void alarm_AlarmChanged(object sender, bool arg)
        {
            if (AlarmTripped != null)
                AlarmTripped(this, arg);
        }

        #endregion

        #region User interface

        // Fields
        private Color color;
        private bool autoStart;
        private bool startShrinked;
        private int chartTime;
        private int width;
        private Point? location;
        private int volume;
        private bool playBeat;
        private bool playAlarm;

        // Events
        public event GenericEventHandler<string, bool> StatusChanged;
        public event GenericEventHandler<Color> ColorChanged;
        public event GenericEventHandler<bool> AutoStartChanged;
        public event GenericEventHandler<bool> StartShrinkedChanged;
        public event GenericEventHandler<int> ChartTimeChanged;
        public event GenericEventHandler<int> WidthChanged;
        public event GenericEventHandler<Point?> LocationChanged;
        public event GenericEventHandler<int> VolumeChanged;
        public event GenericEventHandler<bool> AlarmEnabledChanged;
        public event GenericEventHandler<int> AlarmLowThresholdChanged;
        public event GenericEventHandler<int> AlarmHighThresholdChanged;
        public event GenericEventHandler<bool> AlarmDefuseChanged;
        public event GenericEventHandler<int> AlarmDefuseTimeChanged;
        public event GenericEventHandler<bool> PlayBeatChanged;
        public event GenericEventHandler<bool> PlayAlarmChanged;

        // Properties
        public List<Color> Colors { get; private set; }

        public Color Color 
        {
            get { return color; }
            set 
            {
                Color bck = color;
                color = value;

                if (value != bck)
                    if (ColorChanged != null)
                        ColorChanged(this, value);
            }
        }

        public bool AutoStart
        {
            get { return autoStart; }
            set
            {
                bool bck = autoStart;
                autoStart = value;

                if (bck != value)
                    if (AutoStartChanged != null)
                        AutoStartChanged(this, value);
            }
        }

        public bool StartShrinked
        {
            get { return startShrinked; }
            set
            {
                bool bck = startShrinked;
                startShrinked = value;

                if (bck != value)
                    if (StartShrinkedChanged != null)
                        StartShrinkedChanged(this, value);
            }
        }

        public int ChartTime
        {
            get { return chartTime; }
            set
            {
                int bck = chartTime;
                chartTime = value;

                if (value != bck)
                    if (ChartTimeChanged != null)
                        ChartTimeChanged(this, value);
            }
        }

        public int Width
        {
            get { return width; }
            set
            {
                int bck = width;
                width = value;

                if (value != bck)
                    if (WidthChanged != null)
                        WidthChanged(this, value);
            }
        }

        public Point? Location
        {
            get { return location; }
            set
            {
                Point? bck = location;
                location = value;

                if (value != bck)
                    if (LocationChanged != null)
                        LocationChanged(this, value);
            }
        }

        public int Volume
        {
            get { return volume; }
            set
            {
                int bck = volume;
                volume = value;

                if (bck != value)
                    if (VolumeChanged != null)
                        VolumeChanged(this, value);
            }
        }

        public bool AlarmEnabled
        {
            get { return alarm.Enabled; }
            set
            {
                bool bck = alarm.Enabled;
                alarm.Enabled = value;

                if (bck != value)
                    if (AlarmEnabledChanged != null)
                        AlarmEnabledChanged(this, value);
            }
        }

        public int AlarmLowThreshold
        {
            get { return alarm.LowThreshold; }
            set
            {
                int bck = alarm.LowThreshold;
                alarm.LowThreshold = value;

                if (bck != value)
                    if (AlarmLowThresholdChanged != null)
                        AlarmLowThresholdChanged(this, value);
            }
        }

        public int AlarmHighThreshold
        {
            get { return alarm.HighThreshold; }
            set
            {
                int bck = alarm.HighThreshold;
                alarm.HighThreshold = value;

                if (bck != value)
                    if (AlarmHighThresholdChanged != null)
                        AlarmHighThresholdChanged(this, value);
            }
        }

        public bool AlarmDefuse
        {
            get { return alarm.Defuse; }
            set
            {
                bool bck = alarm.Defuse;
                alarm.Defuse = value;

                if (bck != value)
                    if (AlarmDefuseChanged != null)
                        AlarmDefuseChanged(this, value);
            }
        }

        public int AlarmDefuseTime
        {
            get { return alarm.DefuseTime; }
            set
            {
                int bck = alarm.DefuseTime;
                alarm.DefuseTime = value;

                if (bck != value)
                    if (AlarmDefuseTimeChanged != null)
                        AlarmDefuseTimeChanged(this, value);
            }
        }

        public bool PlayBeat
        {
            get { return playBeat; }
            set
            {
                bool bck = playBeat;
                playBeat = value;

                if (bck != value)
                    if (PlayBeatChanged != null)
                        PlayBeatChanged(this, value);
            }
        }

        public bool PlayAlarm
        {
            get { return playAlarm; }
            set
            {
                bool bck = playAlarm;
                playAlarm = value;

                if (bck != value)
                    if (PlayAlarmChanged != null)
                        PlayAlarmChanged(this, value);
            }
        }

        // Methods
        private void InitializeColors()
        {
            Colors = new List<Color>();
            Colors.Add(Color.Lime);
            Colors.Add(Color.Green);
            Colors.Add(Color.Blue);
            Colors.Add(Color.Cyan);
            Colors.Add(Color.Purple);
            Colors.Add(Color.Fuchsia);
            Colors.Add(Color.Red);
            Colors.Add(Color.Orange);
            Colors.Add(Color.Yellow);
            Colors.Add(Color.White);
        }

        #endregion

        #region Commands

        // Events
        public event GenericEventHandler Started;
        public event GenericEventHandler Stopped;

        // Methods
        public void Init()
        {
            RegisterBundlesEventHandlers();
            LoadConfig();
            if (bundle == null)
                bundle = Bundles[0];       

            FireEventChain();
        }

        private void RegisterBundlesEventHandlers()
        {
            foreach (Bundle b in Bundles)
            {
                b.RegisterCardiaEventHandlers(this);
            }
        }

        private void LoadConfig()
        {
            foreach (Bundle b in Bundles)
            {
                b.LoadConfig(configuration.Device, configuration.Log);
                if (b.ConfigEnumerator == configuration.Device.Type)
                {
                    bundle = b;
                }
            }

            RegisterHrmEventHandlers();

            color = Colors[configuration.Color];

            autoStart = configuration.AutoStart;

            startShrinked = configuration.StartInShrinkMode;

            chartTime = configuration.ChartTime;

            volume = configuration.Volume;

            playBeat = configuration.PlayBeat;

            playAlarm = configuration.PlayAlarm;

            alarm.Enabled = configuration.Alarm;

            alarm.LowThreshold = configuration.AlarmLowThreshold;

            alarm.HighThreshold = configuration.AlarmHighThreshold;

            alarm.Defuse = configuration.AlarmDefuse;

            alarm.DefuseTime = configuration.AlarmDefuseTime;

            width = configuration.WindowWidth;

            location = configuration.WindowLocation;

            logFormat = configuration.Log.Format;

            switch(logFormat)
            {
                case LogFormat.CSV:
                    logger = bundle.CSVLogger;
                    break;
                case LogFormat.XML:
                    logger = bundle.XMLLogger;
                    break;
                case LogFormat.XLSX:
                    logger = bundle.XLSXLogger;
                    break;
                case LogFormat.UDP:
                    logger = bundle.UDPLogger;
                    break;
            }
        }

        private void FireEventChain()
        {
            if (BundleChanged != null)
                BundleChanged(this, bundle);

            if (ColorChanged != null)
                ColorChanged(this, color);

            if (AutoStartChanged != null)
                AutoStartChanged(this, autoStart);

            if (StartShrinkedChanged != null)
                StartShrinkedChanged(this, startShrinked);

            if (ChartTimeChanged != null)
                ChartTimeChanged(this, chartTime);

            if (VolumeChanged != null)
                VolumeChanged(this, volume);

            if (PlayBeatChanged != null)
                PlayBeatChanged(this, playBeat);

            if (PlayAlarmChanged != null)
                PlayAlarmChanged(this, playAlarm);

            if (AlarmEnabledChanged != null)
                AlarmEnabledChanged(this, alarm.Enabled);

            if (AlarmLowThresholdChanged != null)
                AlarmLowThresholdChanged(this, alarm.LowThreshold);

            if (AlarmHighThresholdChanged != null)
                AlarmHighThresholdChanged(this, alarm.HighThreshold);

            if (AlarmDefuseChanged != null)
                AlarmDefuseChanged(this, alarm.Defuse);

            if (AlarmDefuseTimeChanged != null)
                AlarmDefuseTimeChanged(this, alarm.DefuseTime);

            if (WidthChanged != null)
                WidthChanged(this, width);

            if (LocationChanged != null)
                LocationChanged(this, location);

            if (LogFormatChanged != null)
                LogFormatChanged(this, logFormat);

            if (LoggerChanged != null)
                LoggerChanged(this, logger);

            if (NetworkModeChanged != null)
                NetworkModeChanged(this, networkMode);
            if (NetworkRelayChanged != null)
                NetworkRelayChanged(this, networkRelay); // Useless? Should be already called by NetworkModeChanged
            networkRelay.Init();
        }

        public void Start()
        {
            if (StatusChanged != null)
                StatusChanged(this, "Starting...", false);

            bundle.Device.HeartRateSmoothingFactor = 1;

            try
            {
                if (logEnabled)
                    logger.Start(bundle.Device);
            }
            catch (Exception)
            {
                if (Stopped != null)
                    Stopped(this);
                if (StatusChanged != null)
                    StatusChanged(this, "Failed to start logger", true);
                return;
            }

            try
            {
                bundle.Device.Start();
            }
            catch (Exception)
            {
                if (Stopped != null)
                    Stopped(this);
                if (StatusChanged != null)
                    StatusChanged(this, "Failed to start HRM", true);
                return;
            }

            if (bundle.Device.Running)
            {
                signalGenerator.Start();

                if (Started != null)
                    Started(this);
            }
        }

        public void Stop()
        {
            DoStop(true, false);
        }

        private void DoStop(bool stopHrm, bool notifyError)
        {
            if (stopHrm)
                bundle.Device.Stop();

            signalGenerator.Stop();

            if (AlarmTripped != null)
                AlarmTripped(this, false);

            if (networkRelay != null && networkRelay.Running)
                networkRelay.Send(new HeartRateMessage(null, null, null));

            if (logger != null && logger.Running)
                logger.Stop();

            if (Stopped != null)
                Stopped(this);

            if (StatusChanged != null)
                StatusChanged(this, "Device stopped", notifyError);
        }

        public void SaveConfig()
        {
            configuration.Color = Colors.IndexOf(color);

            configuration.AutoStart = autoStart;

            configuration.StartInShrinkMode = startShrinked;

            bundle.SaveConfig(configuration.Device);

            configuration.ChartTime = chartTime;

            configuration.Volume = volume;

            configuration.PlayBeat = playBeat;

            configuration.PlayAlarm = playAlarm;

            configuration.Alarm = alarm.Enabled;

            configuration.AlarmLowThreshold = alarm.LowThreshold;

            configuration.AlarmHighThreshold = alarm.HighThreshold;

            configuration.AlarmDefuse = alarm.Defuse;

            configuration.AlarmDefuseTime = alarm.DefuseTime;

            configuration.WindowWidth = width;

            configuration.WindowLocation = location;

            configuration.Log.Format = logFormat;

            if (logger is IHRMNetLogger)
            {
                configuration.Log.Address = ((IHRMNetLogger)logger).Address;
                configuration.Log.Port = ((IHRMNetLogger)logger).Port;
            }

            configuration.Network.Mode = networkMode;

            if (networkRelay is TcpRelayClient<HeartRateMessage>)
                configuration.Network.Address = ((TcpRelayClient<HeartRateMessage>)networkRelay).ServerAddress;

            configuration.Network.Port = networkRelay.Port;

            configuration.Network.Nickname = networkRelay.Nickname;
        }

        #endregion Commands

        #region Logging

        // Fields
        private LogFormat logFormat;
        private bool logEnabled = false;
        private IHRMLogger logger;

        // Events
        public event GenericEventHandler<LogFormat> LogFormatChanged;
        public event GenericEventHandler<bool> LogEnabledChanged;
        public event GenericEventHandler<IHRMLogger> LoggerChanged;

        // Properties
        public LogFormat LogFormat
        {
            get { return logFormat; }
            set
            {
                LogFormat bck = logFormat;

                logFormat = value;

                SetLogger();

                if (bck != value)
                    if (LogFormatChanged != null)
                        LogFormatChanged(this, value);
            }
        }
        
        public bool LogEnabled
        {
            get { return logEnabled; }
            set
            {
                bool bck = logEnabled;

                logEnabled = value;

                if (bck != value)
                    if (LogEnabledChanged != null)
                        LogEnabledChanged(this, value);
            }
        }

        public IHRMLogger Logger
        {
            get { return logger; }
            private set
            {
                if (bundle != null)
                    if (bundle.Device.Running)
                        throw new Exception("Cannot change logger while device is running");

                IHRMLogger bck = logger;

                logger = value;
                if (bck != value)
                {
                    if (bck != null)
                        bck.ResetSubscriptions();
                    if (LoggerChanged != null)
                        LoggerChanged(this, logger);
                }
            }
        }

        // Methods
        private void SetLogger()
        {
            if (bundle == null)
                return;

            switch (logFormat)
            {
                case LogFormat.CSV:
                    Logger = bundle.CSVLogger;
                    break;
                case LogFormat.XLSX:
                    Logger = bundle.XLSXLogger;
                    break;
                case LogFormat.XML:
                    Logger = bundle.XMLLogger;
                    break;
                case LogFormat.UDP:
                    Logger = bundle.UDPLogger;
                    break;
            }
        }

        private void InitializeLoggers()
        {
            logFormat = configuration.Log.Format;

            SetLogger();
        }

        #endregion Logging

        #region Networking

        // Fields
        private NetworkRelayMode networkMode;
        private List<NetworkRelay<HeartRateMessage>> networkRelays;
        private NetworkRelay<HeartRateMessage> networkRelay;
        private IDictionary<int, SignalGenerator> generators = new ConcurrentDictionary<int, SignalGenerator>();
        private IDictionary<int, string> nicknames = new ConcurrentDictionary<int, string>();
        private NetworkSampler networkSampler;

        // Events
        public event GenericEventHandler<NetworkRelayMode> NetworkModeChanged;
        public event GenericEventHandler<NetworkRelay<HeartRateMessage>> NetworkRelayChanged;
        public event GenericEventHandler<string> NetworkStatusChenged;
        public event GenericEventHandler<string> NetworkConnected;
        public event GenericEventHandler<int, string> NetworkClientConnected;
        public event GenericEventHandler<int, SignalGeneratedEventArgs> ClientSignalGenerated;
        public event GenericEventHandler<int> NetworkClientDisconnected;
        public event GenericEventHandler<bool> NetworkDisconnected;
        public event GenericEventHandler<HeartRateMessage> NetworkMessageReceived;

        // Properties
        public NetworkRelayMode NetworkMode
        {
            get { return networkMode; }
            set
            {
                if (networkRelay != null)
                    if (networkRelay.Running)
                        throw new Exception("Cannot change network mode while current network relay is running");

                NetworkRelayMode bck = networkMode;

                networkMode = value;

                if (bck != value)
                    if (NetworkModeChanged != null)
                        NetworkModeChanged(this, value);

                SetNetworkProvider();
            }
        }

        public NetworkRelay<HeartRateMessage> NetworkRelay
        {
            get { return networkRelay; }
            private set
            {
                if (networkRelay != null)
                    if (networkRelay.Running)
                        throw new Exception("Cannot change network mode while current network relay is running");

                NetworkRelay<HeartRateMessage> bck = networkRelay;

                networkRelay = value;
                if (bck != value)
                {
                    networkRelay.ResetSubscriptions();
                    if (NetworkRelayChanged != null)
                        NetworkRelayChanged(this, value);
                }
            }
        }

        // Methods
        private void SetNetworkProvider()
        {
            switch (networkMode)
            {
                case NetworkRelayMode.Client:
                    if (networkRelay != null)
                    {
                        networkRelays[0].Port = networkRelay.Port;
                        networkRelays[0].Timeout = networkRelay.Timeout;
                        networkRelays[0].Version = networkRelay.Version;
                        networkRelays[0].Nickname = networkRelay.Nickname;
                    }
                    NetworkRelay = networkRelays[0];
                    break;
                case NetworkRelayMode.Server:
                    if (networkRelay != null)
                    {
                        networkRelays[1].Port = networkRelay.Port;
                        networkRelays[1].Timeout = networkRelay.Timeout;
                        networkRelays[1].Version = networkRelay.Version;
                        networkRelays[1].Nickname = networkRelay.Nickname;
                    }
                    NetworkRelay = networkRelays[1];
                    break;
            }
        }

        public void StartNetwork()
        {
            if (networkRelay.Nickname == null)
            {
                if (NetworkStatusChenged != null)
                    NetworkStatusChenged(this, "Invalid nickname");

                if (NetworkDisconnected != null)
                    NetworkDisconnected(this, true);

                return;
            }

            if (networkRelay.Nickname.Length == 0)
            {
                if (NetworkStatusChenged != null)
                    NetworkStatusChenged(this, "Invalid nickname");

                if (NetworkDisconnected != null)
                    NetworkDisconnected(this, true);

                return;
            }

            networkRelay.OnClientConnected += networkProvider_OnClientConnected;
            networkRelay.OnclientDisconnected += networkProvider_OnclientDisconnected;
            networkRelay.OnProviderDisconnected += networkProvider_OnProviderDisconnected;
            networkRelay.OnClientMessageReceived += networkProvider_OnClientMessageReceived;

            if (!networkRelay.Start())
                return;

            if (NetworkStatusChenged != null)
                NetworkStatusChenged(this, "Connected");

            if (NetworkConnected != null)
                NetworkConnected(this, networkRelay.Nickname);

            networkSampler.Start();
        }

        public void StopNetwork()
        {
            networkSampler.Stop();
            networkRelay.Stop();

            networkRelay.OnClientConnected -= networkProvider_OnClientConnected;
            networkRelay.OnclientDisconnected -= networkProvider_OnclientDisconnected;
            networkRelay.OnProviderDisconnected -= networkProvider_OnProviderDisconnected;
            networkRelay.OnClientMessageReceived -= networkProvider_OnClientMessageReceived;
        }

        private void InitializeNetworkProvider()
        {
            networkRelays = new List<NetworkRelay<HeartRateMessage>>();
            TcpRelayClient<HeartRateMessage> client = new TcpRelayClient<HeartRateMessage>();
            client.Timeout = 10000;
            client.Version = networkVersion;
            networkRelays.Add(client);

            TcpRelayServer<HeartRateMessage> server = new TcpRelayServer<HeartRateMessage>();
            server.MaxConnections = 4;
            server.Timeout = 10000;
            server.Version = networkVersion;
            networkRelays.Add(server);

            NetworkMode = configuration.Network.Mode;
            networkRelay.Port = configuration.Network.Port;
            networkRelay.Nickname = configuration.Network.Nickname;

            client.ServerAddress = configuration.Network.Address;

            networkSampler = new NetworkSampler(this);
            networkSampler.PacketSampled += networkSampler_PacketSampled;
        }

        void networkSampler_PacketSampled(object sender, IHRMPacket hrmPacket, byte? minHeartRate, byte? maxHeartRate)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Sending HRM message");
#endif
            networkRelay.Send(new HeartRateMessage(hrmPacket.HeartRate, minHeartRate, maxHeartRate));
        }

        // Network relay event handlers
        private void networkProvider_OnClientConnected(object sender, int clientId, string nickname)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(nickname + " connected");
#endif

            nicknames[clientId] = nickname;

            if (NetworkStatusChenged != null)
                NetworkStatusChenged(this, nickname + " connected");

            SignalGenerator clientGenerator = new SignalGenerator(clientId, BUFFER_TIME);

            if (NetworkClientConnected != null)
                NetworkClientConnected(this, clientId, nickname);

            clientGenerator.OnSignalGenerated += delegate(object o, SignalGeneratedEventArgs e)
            {
                if (ClientSignalGenerated != null)
                    ClientSignalGenerated(this, clientId, e);
            };

            clientGenerator.Start();
            generators[clientId] = clientGenerator;
        }

        private void networkProvider_OnclientDisconnected(object sender, int clientId)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(clientId + " diconnected");
#endif

            try
            {
                SignalGenerator clientGenerator = generators[clientId];
                clientGenerator.ResetSubscriptions();
                clientGenerator.Stop();
                generators.Remove(clientId);
            }
            catch (KeyNotFoundException) { }

            try
            {
                if (NetworkStatusChenged != null)
                    NetworkStatusChenged(this, nicknames[clientId] + " disconnected");
            }
            catch (KeyNotFoundException) { }

            if (NetworkClientDisconnected != null)
                NetworkClientDisconnected(this, clientId);

            nicknames.Remove(clientId);
        }

        private void networkProvider_OnProviderDisconnected(object sender, string cause, bool error)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Diconnected");
#endif

            foreach (SignalGenerator clientGenerator in generators.Values)
            {
                clientGenerator.Stop();
            }

            generators.Clear();
            nicknames.Clear();

            networkSampler.Stop();

            if (NetworkStatusChenged != null)
            {
                if (error)
                    NetworkStatusChenged(this, "Disconnected - " + cause);
                else
                    NetworkStatusChenged(this, "Disconnected");
            }

            networkRelay.OnClientConnected -= networkProvider_OnClientConnected;
            networkRelay.OnclientDisconnected -= networkProvider_OnclientDisconnected;
            networkRelay.OnProviderDisconnected -= networkProvider_OnProviderDisconnected;
            networkRelay.OnClientMessageReceived -= networkProvider_OnClientMessageReceived;

            if (NetworkDisconnected != null)
                NetworkDisconnected(this, false);
        }

        private void networkProvider_OnClientMessageReceived(object sender, HeartRateMessage heartRateMessage)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("HRM message received - BPM: " + heartRateMessage.BPM);
#endif

            generators[heartRateMessage.ClientId].BPM = heartRateMessage.BPM ?? 0;

            if (NetworkMessageReceived != null)
                NetworkMessageReceived(this, heartRateMessage);
        }

        #endregion Networking

        #region IDisposable

        //Fields
        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            Stop();

            StopNetwork();

            foreach (Bundle bundle in Bundles)
                bundle.Dispose();

            alarm.Dispose();

            if (logger != null)
                logger.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
