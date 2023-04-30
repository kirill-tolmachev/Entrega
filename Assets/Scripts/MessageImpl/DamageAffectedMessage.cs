using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scripts.Infrastructure.Messages;
using UnityEngine;

namespace Assets.Scripts.MessageImpl
{
    internal class DamageAffectedMessage : IMessage
    {
        public DamageAffectedMessage(Transform affector)
        {
            Affector = affector;
        }

        public Transform Affector { get; }
    }
}
