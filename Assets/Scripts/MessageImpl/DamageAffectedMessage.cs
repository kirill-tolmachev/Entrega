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
        public DamageAffectedMessage(bool isPlayer, Transform affector, Damageable target)
        {
            IsPlayer = isPlayer;
            Affector = affector;
            Target = target;
        }

        public bool IsPlayer { get; }
        public Transform Affector { get; }
        public Damageable Target { get; }
    }
}
