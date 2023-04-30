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
    internal class HpBarController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;

        [SerializeField] private SpriteRenderer _spriteRenderer;

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
            if (!obj.IsPlayer)
                return;

            var player = obj.Target;
            float scale = player.Health / player.MaxHealth;

            _spriteRenderer.transform.localScale = new Vector3(_spriteRenderer.transform.localScale.x, scale, _spriteRenderer.transform.localScale.z);
        }

    }
}
