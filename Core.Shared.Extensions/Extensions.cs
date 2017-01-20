using System;
using System.Linq;
using System.Net;

namespace Core.Shared.Extensions
{
    public static class Extensions
    {
        public static IPAddress Increment(this IPAddress value)
        {
            var ip = BitConverter.ToInt32(value.GetAddressBytes().Reverse().ToArray(), 0);
            ip++;

            return new IPAddress(BitConverter.GetBytes(ip).Reverse().ToArray());
        }
    }
}