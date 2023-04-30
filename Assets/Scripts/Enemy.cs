using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Assets.Scripts.Util;
using DG.Tweening;
using Scripts;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    internal class Enemy : MovableObject
    {
        public bool IsDead { get; private set; }

        [SerializeField]
        private float _health = 2f;

        [SerializeField]
        private LayerMask _collisionLayerMask;

        [Inject]
        private IMessageBus _messageBus;

        [SerializeField] private ParticleSystem _onDestroyParticleSystem;

        [Inject] private Instantiator _instantiator;
        [Inject] private ObjectLocator _objectLocator;

        private void Awake()
        {
            IsMovable = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsDead)
                return;

            if (!_collisionLayerMask.HasLayer(other.gameObject.layer))
                return;


            _messageBus.Publish(new DamageAffectedMessage(other.transform));

            _health--;
            if (_health <= 0)
            {
                IsDead = true;
                transform.DOShakeRotation(3f, 90f);
                IsMovable = true;
            }
            else
            {
                transform.DOShakeRotation(0.4f);
            }
        }

        private void OnDestroy()
        {
            if (IsDead)
            {
                _instantiator.InstantiatePrefabWorldSpace(_onDestroyParticleSystem, transform.position,
                    Quaternion.identity, _objectLocator.FXsParent);
            }
        }
    }
}
