using Scripts.Infrastructure.Messages;
using UnityEngine;

namespace Assets.Scripts.MessageImpl
{
    internal class DeathMessage : IMessage
    {
        public DeathMessage(Damageable target)
        {
            Target = target;
        }
        
        public Damageable Target { get; }
    }
}