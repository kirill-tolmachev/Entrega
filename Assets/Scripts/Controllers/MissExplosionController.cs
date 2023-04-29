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
    internal class MissExplosionController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        [SerializeField] private Transform _parent;

        [Inject] private IMessageBus _messageBus;

        [Inject] private Instantiator _instantiator;

        private void OnEnable()
        {
            _messageBus.Subscribe<ShotMissedMessage>(OnShotMissed);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<ShotMissedMessage>(OnShotMissed);
        }

        private void OnShotMissed(ShotMissedMessage message)
        {
            _instantiator.InstantiatePrefabWorldSpace<ParticleSystem>(_particleSystem, message.Position, Quaternion.identity, _parent);
        }
    }
}
