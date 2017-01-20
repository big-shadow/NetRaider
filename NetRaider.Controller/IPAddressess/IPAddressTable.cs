using System;
using System.Net;

namespace NetRaider.Controller.IPAddressess
{
    // Class	Private Networks	             Address Range
    // A	    10.0.0.0	                     10.0.0.0 - 10.255.255.255
    // B	    172.16.0.0 - 172.31.0.0	         172.16.0.0 - 172.31.255.255
    // C	    192.168.0.0	                     192.168.0.0 - 192.168.255.255

    internal class IPAddressTable
    {
        private string _start;
        private string _stop;

        public IPAddressTable(string start, string stop)
        {
            IPAddress temp;

            if (!IPAddress.TryParse(start, out temp))
            {
                throw new Exception("The starting IP Address is not a valid address.");
            }

            if (!IPAddress.TryParse(stop, out temp))
            {
                throw new Exception("The stopping IP Address is not a valid address.");
            }
        }
    }
}