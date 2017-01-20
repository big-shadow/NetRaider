using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Tor
{
    public class TorProcess
    {
        private Process _tor;
        private string _privoxyPort;
        private string _torPath;

        public TorProcess(string torPath, string privoxyPort)
        {
            _privoxyPort = privoxyPort;
            _torPath = torPath;
        }

        public void StartTor()
        {
            // Check to see if it's already running.
            if (_tor != null && _tor.TotalProcessorTime.Milliseconds > 0)
            {
                return;
            }

            _tor = new Process();
            _tor.StartInfo = new ProcessStartInfo(_torPath);
            _tor.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _tor.Start();
        }

        public void StopTor()
        {
            foreach (var process in Process.GetProcessesByName("tor"))
            {
                process.Kill();
            }
        }

        public IPAddress GetIPAddress()
        {
            Regex regex = new Regex("\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}", RegexOptions.Multiline);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://check.torproject.org/");

            request.Proxy = new WebProxy(string.Format("127.0.0.1:{0}", _privoxyPort));
            request.KeepAlive = false;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                {
                    string data = reader.ReadToEnd();

                    IPAddress address;

                    if (!IPAddress.TryParse(regex.Match(data).Groups[0].Value, out address))
                    {
                        throw new Exception("Couldn't get the current IP Address");
                    }

                    return address;
                }
            }
        }
    }
}