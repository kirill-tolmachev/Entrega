using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scripts.Infrastructure.Messages;

namespace Assets.Scripts.MessageImpl
{
    internal class LocalScoreChangedMessage : IMessage
    {
        public LocalScoreChangedMessage(int delta)
        {
            Delta = delta;
        }

        public int Delta { get; }
    }
}
