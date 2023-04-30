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
    internal class SoundEffectsController : MonoBehaviour
    {
        [SerializeField] private AudioClip _enemyDeath;
        [SerializeField] private AudioClip _packageDelivered;
        [SerializeField] private AudioClip _shotMissed;
        [SerializeField] private AudioClip _playerHit;

        private List<AudioSource> _sources = new List<AudioSource>();

        private void Awake()
        {
            for (int i = 0; i < 10; i++)
            {
                var source = gameObject.AddComponent<AudioSource>();
                _sources.Add(source);
            }
        }

        [Inject] private IMessageBus _messageBus;

        private void OnDeath(DeathMessage obj)
        {
            PlayClip(_enemyDeath);
        }

        private void OnPackageDelivered(PackageDeliveredMessage obj) => PlayClip(_packageDelivered);
        private void OnDamageAffected(DamageAffectedMessage _) => PlayClip(_playerHit);
        private void OnShotCollision(ShotMissedMessage _) => PlayClip(_shotMissed);

        private void OnEnable()
        {
            _messageBus.Subscribe<DeathMessage>(OnDeath);
            _messageBus.Subscribe<PackageDeliveredMessage>(OnPackageDelivered);
            _messageBus.Subscribe<ShotMissedMessage>(OnShotCollision);
            _messageBus.Subscribe<DamageAffectedMessage>(OnDamageAffected);
        }


        private void OnDisable()
        {
            _messageBus.Unsubscribe<DeathMessage>(OnDeath);
        }

        private void PlayClip(AudioClip clip)
        {
            var source = _sources.FirstOrDefault(x => !x.isPlaying);
            if (source == null)
            {
                return;
            }
            
            source.clip = clip;
            source.Play();
        }
    }
}
