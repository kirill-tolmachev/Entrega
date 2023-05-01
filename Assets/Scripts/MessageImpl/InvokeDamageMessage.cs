using Scripts.Infrastructure.Messages;

namespace Assets.Scripts.MessageImpl
{
    internal class InvokeDamageMessage : IMessage
    {
        public InvokeDamageMessage(bool toPlayer, int value)
        {
            ToPlayer = toPlayer;
            Value = value;
        }

        public bool ToPlayer { get; }

        public int Value { get;  }
    }
}