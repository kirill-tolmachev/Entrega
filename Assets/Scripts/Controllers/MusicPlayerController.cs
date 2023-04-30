using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using DG.Tweening;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class MusicPlayerController : MonoBehaviour
    {
        [SerializeField] private AudioClip _startupClip;

        [SerializeField] private AudioClip _loopClip;

        [SerializeField] private AudioSource _audioSource;

        [Inject] private IMessageBus _messageBus;

        private float _maxVolume;

        private void OnEnable()
        {
            _maxVolume = _audioSource.volume;
            _audioSource.clip = _startupClip;
            _audioSource.Play();

            _messageBus.Subscribe<ResetMessage>(OnReset);
            _messageBus.Subscribe<OnPlayMessage>(OnPlay);
        }


        private void OnDisable()
        {
            _audioSource.Stop();

            _messageBus.Unsubscribe<ResetMessage>(OnReset);
            _messageBus.Unsubscribe<OnPlayMessage>(OnPlay);
        }

        private void OnReset(ResetMessage _)
        {
            _audioSource.DOFade(0f, 0.2f);
        }

        private void OnPlay(OnPlayMessage _)
        {
            _audioSource.DOFade(_maxVolume, 0.2f);
        }

        void Update()
        {
            if (_audioSource.isPlaying == false)
            {
                _audioSource.clip = _loopClip;
                _audioSource.loop = true;
                _audioSource.Play();
            }
        }

    }
}
