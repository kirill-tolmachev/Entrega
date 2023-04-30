using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scripts.Infrastructure.Messages;
using UnityEngine;

namespace Assets.Scripts.MessageImpl
{
    internal class EnemyShootMessage : IMessage
    {
        public EnemyShootMessage(Enemy enemy)
        {
            Enemy = enemy;
        }

        public Enemy Enemy { get; }
    }
}
