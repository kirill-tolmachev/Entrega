using Scripts;
using Scripts.Infrastructure.Messages;

namespace Assets.Scripts.MessageImpl
{
    internal class PlayerTargetCollisionMessage : IMessage
    {
        public Target Target { get; }

        public PlayerTargetCollisionMessage(Target target)
        {
            Target = target;
        }
    }
}