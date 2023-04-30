using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    internal class Enemy : MovableObject
    {
        public bool IsDead => _damageable.IsDead;

        private Damageable _damageable;
        
        private void Awake()
        {
            IsMovable = false;
            _damageable = GetComponent<Damageable>();
        }

        private void Update()
        {
            if (IsDead)
                IsMovable = true;
        }
    }
}
