using Scripts.Infrastructure.Messages;
using UnityEngine;

namespace Scripts.MessageImpl
{
    internal class ObjectCreatedMessage : IMessage
    {
        public ObjectCreatedMessage(Transform o)
        {
            Object = o;
        }

        public Transform Object { get; }

        
    }
}
