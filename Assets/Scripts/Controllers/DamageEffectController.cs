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
    internal class DamageEffectController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;
        [SerializeField] private ParticleSystem _damageParticleSystem;
        [SerializeField] private Transform _fxsParent;
        [Inject] private Instantiator _instantiator;

        private void OnEnable()
        {
            _messageBus.Subscribe<DamageAffectedMessage>(OnDamageAffected);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<DamageAffectedMessage>(OnDamageAffected);
        }

        private void OnDamageAffected(DamageAffectedMessage message)
        {
            var position = message.Affector.position;
            _instantiator.InstantiatePrefabWorldSpace(_damageParticleSystem, position, Quaternion.identity, _fxsParent);
        }
    }
}
