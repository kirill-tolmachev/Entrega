using Scripts.Infrastructure.Messages;
using UnityEngine;

namespace Scripts.MessageImpl
{
    internal class ObjectDestroyedMessage : IMessage 
    {
        public ObjectDestroyedMessage(Transform o)
        {
            Object = o;
        }

        public Transform Object { get; }
    }
}