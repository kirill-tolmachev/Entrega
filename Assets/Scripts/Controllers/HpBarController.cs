using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Scripts.Infrastructure.Messages;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class HpBarController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;

        [SerializeField] private float _minScale;
        [SerializeField] private float _maxScale;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void OnEnable()
        {
            _messageBus.Subscribe<DamageAffectedMessage>(OnDamageAffected);
            _messageBus.Subscribe<ResetMessage>(OnReset);
        }


        private void OnDisable()
        {
            _messageBus.Unsubscribe<DamageAffectedMessage>(OnDamageAffected);
            _messageBus.Unsubscribe<ResetMessage>(OnReset);
        }

        private void OnDamageAffected(DamageAffectedMessage obj)
        {
            if (!obj.IsPlayer)
                return;

            var player = obj.Target;
            float scale = ChangeNorm(1f - player.Health / player.MaxHealth, _minScale, _maxScale);

            _spriteRenderer.transform.localScale = new Vector3(scale, scale, scale);
        }

        float ChangeNorm(float value, float min, float max)
        {
            return value * (max - min) + min;
        }

        private void OnReset(ResetMessage obj)
        {
            _spriteRenderer.transform.localScale = Vector3.one * _minScale;
        }

    }
}
