using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class DestroyBulletsController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;

        private List<Bullet> _bullets = new();

        private void OnEnable()
        {
            _messageBus.Subscribe<DamageAffectedMessage>(OnDamageAffected);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<DamageAffectedMessage>(OnDamageAffected);
        }


        private void OnDamageAffected(DamageAffectedMessage obj)
        {
            if (obj.Affector.TryGetComponent(out Bullet bullet))
                _bullets.Add(bullet);
        }

        private void LateUpdate()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                var bullet = _bullets[i];
                _bullets.RemoveAt(i);
                if (bullet)
                {
                    Destroy(bullet.gameObject);
                }
            }
        }
    }
}
