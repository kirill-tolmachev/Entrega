using Scripts.Infrastructure.Messages;
using UnityEngine;

namespace Scripts.MessageImpl
{
    internal class ShootMessage : IMessage
    {
        public ShootMessage(bool isPlayerOrigin, Vector3 position, float direction)
        {
            IsPlayerOrigin = isPlayerOrigin;
            Position = position;
            Direction = direction;
        }

        public bool IsPlayerOrigin { get; }

        public Vector3 Position { get; }
        public float Direction { get; }
    }
}