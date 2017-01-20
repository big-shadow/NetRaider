using Core.Tor;
using NetRaider.Configuration;
using NetRaider.Core.Communication;
using NetRaider.Core.Models;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace NetRaider.Core.Controller
{
    public class Controller
    {
        public Communicator Communicator = new Communicator();

        public void TestTor()
        {
            TorProcess tor = new TorProcess(Config.Item["TorPath"], Config.Item["PrivoxyPort"]);
            tor.StartTor();

            Thread.Sleep(2500);
            Communicator.UpdateMessage(string.Format("IP Address is: {0}", tor.GetIPAddress().ToString()));
        }

        public void TestPing()
        {
            IPAddressRange range = new IPAddressRange("8.8.4.4", "8.8.8.8");

            foreach (IPAddress address in range)
            {
                address.MapToIPv4();

                if (IPAddress.IsLoopback(address))
                {
                    continue;
                }

                Ping ping = new Ping();
                PingReply reply = ping.Send(address, 45);

                if (reply.Status == IPStatus.Success)
                {
                    Communicator.UpdateMessage(string.Format("Address: {0}", reply.Address.ToString()));
                    Communicator.UpdateMessage(string.Format("RoundTrip time: {0}", reply.RoundtripTime));
                    Communicator.UpdateMessage(string.Format("Time to live: {0}", reply.Options.Ttl));
                    Communicator.UpdateMessage(string.Format("Don't fragment: {0}", reply.Options.DontFragment));
                    Communicator.UpdateMessage(string.Format("Buffer size: {0}", reply.Buffer.Length));
                }
            }
        }
    }
}