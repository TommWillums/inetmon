using System;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Text;
using System.Timers;

namespace inetmon
{
    class Program
    {
        const int PING_TIMEOUT = 3;

        static int pingInterval;    // default every minute
        static int aliveInterval;   // default every hour
        static string urlToMonitor; // default www.google.com

        static InputOptions options;    

        static Timer pingTimer;
        static Timer aliveTimer;

        static void Main(string[] args)
        {
            Configure(args);
            RunMonitor();
        }

        private static void Configure(string[] args)
        {
            options = new InputOptions(args);
            pingInterval = options.GetInt("ping");
            aliveInterval = options.GetInt("alive");
            urlToMonitor = options.Get("url");
        }

        private static void RunMonitor()
        {
            string assemblyVersion = "1.0.2.0";

            string msg = $"---- Internet Connection Monitor {assemblyVersion} ----";
            Console.WriteLine(msg);
            Console.Error.WriteLine(msg);

            msg = $"Started: {DateTime.Now:dd.MM.yyyy HH:mm:ss}";
            Console.WriteLine(msg);
            Console.Error.WriteLine(msg);

            Console.WriteLine($"Ping    : {pingInterval} seconds");
            Console.WriteLine($"Alive   : {aliveInterval / 60} minutes");
            Console.WriteLine($"Monitor : {urlToMonitor}");

            SetTimers();

            try
            {
                while ("q" != Console.ReadLine()) { }
            }
            finally
            {
                pingTimer.Stop();
                aliveTimer.Stop();
                pingTimer.Dispose();
                aliveTimer.Dispose();
                Console.WriteLine("---- Monitor Stopped ----");
            }
        }

        private static void SetTimers()
        {
            pingTimer = new Timer(pingInterval * 1000);
            pingTimer.Elapsed += OnPingTimerEvent;
            pingTimer.AutoReset = true;
            pingTimer.Enabled = true;

            aliveTimer = new Timer(aliveInterval * 1000);
            aliveTimer.Elapsed += OnAliveTimerEvent;
            aliveTimer.AutoReset = true;
            aliveTimer.Enabled = true;
        }

        private static void OnPingTimerEvent(Object source, ElapsedEventArgs e)
        {
            CheckInternetConnection();
        }

        static void CheckInternetConnection()
        {
            if (HasInternetConnection())
            {
                if (pingInterval == 1)
                {
                    string msg = $"{DateTime.Now:dd.MM.yyyy HH:mm} - OK";
                    Console.Error.WriteLine(msg);
                }
            }
            else
            {
                string msg = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} ERROR: Ping {urlToMonitor} failed!";
                Console.Error.WriteLine(msg);
            }
        }

        private static void OnAliveTimerEvent(Object source, ElapsedEventArgs e)
        {
            string msg = $"Still alive at {DateTime.Now:dd.MM.yyyy HH:mm}";
            Console.WriteLine(msg);
        }

        private static bool HasInternetConnection()
        {
            return PingUrl(urlToMonitor);
        }

        private static bool PingUrl(string host)
        {
            bool pingStatus = false;

            using (Ping p = new Ping())
            {
                string data = "01234567890123456789012345678901";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = PING_TIMEOUT * 1000;

                try
                {
                    PingReply reply = p.Send(host, timeout, buffer);
                    pingStatus = (reply.Status == IPStatus.Success);
                }
                catch (Exception)
                {
                    pingStatus = false;
                }
            }

            return pingStatus;
        }

        //private static void LogToFile(string str)
        //{
        //    using StreamWriter file = new(LOG_FILE, append: true);
        //    file.WriteLine(str);
        //}

    }
}
