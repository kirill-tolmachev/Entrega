using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scripts.Infrastructure.Messages;
using UnityEngine;

namespace Assets.Scripts.MessageImpl
{
    internal class ShotMissedMessage : IMessage
    {
        public ShotMissedMessage(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; }
    }
}
