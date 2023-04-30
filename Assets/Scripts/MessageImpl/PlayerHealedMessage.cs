using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scripts.Infrastructure.Messages;

namespace Assets.Scripts.MessageImpl
{
    internal class PlayerHealRequestMessage : IMessage
    {
        public int Value { get; }

        public PlayerHealRequestMessage(int value)
        {
            Value = value;
        }
    }

    internal class PlayerHealedMessage : IMessage
    {
        public PlayerHealedMessage(Damageable player)
        {
            Player = player;
        }

        public Damageable Player { get; }
    }
}
