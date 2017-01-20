using System;

namespace NetRaider.Core.Communication
{
    public sealed class Communicator
    {
        public delegate void Message(String message);

        public event Message MessageEvent;

        public void UpdateMessage(String message)
        {
            try
            {
                MessageEvent.Invoke(message);
            }
            catch (Exception ex)
            {
                throw new Exception("The MessageEvent could not be invoked.", ex);
            }
        }
    }
}