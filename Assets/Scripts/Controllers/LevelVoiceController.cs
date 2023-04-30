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
    internal class LevelVoiceController : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _clips;

        [SerializeField] private AudioSource _source;

        [Inject] private IMessageBus _messageBus;

        private void OnEnable()
        {
            _messageBus.Subscribe<LevelShouldChangeMessage>(OnLevelChanging);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<LevelShouldChangeMessage>(OnLevelChanging);
        }

        private void OnLevelChanging(LevelShouldChangeMessage message)
        {
            var level = Mathf.Clamp(message.LevelIndex - 1, 0, _clips.Length - 1);
            var clip = _clips[level];

            _source.clip = clip;
            _source.Play();
        }

    }
}
