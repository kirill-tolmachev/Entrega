using Assets.Scripts.MessageImpl;
using Assets.Scripts.Util;
using DG.Tweening;
using Scripts;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    internal class Damageable : MonoBehaviour
    {
        [SerializeField] private bool _animateDamage;
        [SerializeField] private bool _animateDeath;
        [SerializeField] private bool _isPlayer;

        public bool IsDead { get; private set; }

        [SerializeField]
        private float _health = 2f;

        private float _currentHealth;

        public float MaxHealth => _health;
        public float Health => _currentHealth;

        [SerializeField]
        private LayerMask _collisionLayerMask;

        [Inject]
        private IMessageBus _messageBus;

        [SerializeField] private ParticleSystem _onDestroyParticleSystem;

        [Inject] private Instantiator _instantiator;
        [Inject] private ObjectLocator _objectLocator;

        private void Start()
        {
            Reset();
            _messageBus.Subscribe<ResetMessage>(OnReset);
        }

        private void OnReset(ResetMessage _) => Reset();
        
        public void Reset()
        {
            IsDead = false;
            _currentHealth = _health;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsDead)
                return;

            if (!_collisionLayerMask.HasLayer(other.gameObject.layer))
                return;

            _messageBus.Publish(new DamageAffectedMessage(_isPlayer, other.transform, this));

            _currentHealth--;
            if (_currentHealth <= 0)
            {
                IsDead = true;
                if (_animateDeath)
                    transform.DOShakeRotation(3f);

                if (_isPlayer)
                    _messageBus.Publish(new DeathMessage(this));
            }
            else
            {
                if (_animateDamage)
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