using System;
using MediatR;

namespace EcMic.Core.Messages
{
    public class Event: Message, INotification
    {
        public DateTime TimeStamp { get; private set; }

        protected Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}