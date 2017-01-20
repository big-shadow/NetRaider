using Core.Shared.Extensions;
using System;
using System.Collections;
using System.Net;

namespace NetRaider.Core.Models
{
    // Class	Private Networks	             Address Range
    // A	    10.0.0.0	                     10.0.0.0 - 10.255.255.255
    // B	    172.16.0.0 - 172.31.0.0	         172.16.0.0 - 172.31.255.255
    // C	    192.168.0.0	                     192.168.0.0 - 192.168.255.255

    public class IPAddressRange : IEnumerator, IEnumerable
    {
        private IPAddress _current;
        private IPAddress _start;
        private IPAddress _stop;

        /// <summary>
        /// Constructs the object by parsing two strings: A start, and end of an IP Address range.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        public IPAddressRange(string start, string stop)
        {
            if (!IPAddress.TryParse(start, out _start))
            {
                throw new Exception("The starting IP Address is not a valid address.");
            }

            if (!IPAddress.TryParse(stop, out _stop))
            {
                throw new Exception("The stopping IP Address is not a valid address.");
            }

            // If we're stopping on the increment, it should exceed the last address in range by 1.
            _stop = _stop.Increment();

            // IEnumerable iterates before evaluating what's current.
            _current = _start.Decrement();
        }

        object IEnumerator.Current
        {
            get
            {
                return _current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        bool IEnumerator.MoveNext()
        {
            _current = _current.Increment();

            if (_current.ToString() == _stop.ToString())
            {
                return false;
            }

            return true;
        }

        void IEnumerator.Reset()
        {
            _current = _start;
        }
    }
}