using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Scripts;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Controllers
{
    internal class SpawnerController : MonoBehaviour
    {
        [SerializeField] private float _spawnInterval;

        [SerializeField] private Target _targetPrefab;

        [SerializeField] private Transform _targetsParent;

        [Inject] private Instantiator _instantiator;

        [Inject] private ObjectLocator _objectLocator;

        private float _timer;

        [Inject] private IMessageBus _messageBus;

        private bool _isActive = true;

        private void OnEnable()
        {
            _messageBus.Subscribe<ResetMessage>(OnReset);
            _messageBus.Subscribe<OnPlayMessage>(OnPlay);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<ResetMessage>(OnReset);
            _messageBus.Unsubscribe<OnPlayMessage>(OnPlay);
        }

        private void OnReset(ResetMessage _)
        {
            _isActive = false;
            _timer = 0f;
        }

        private void OnPlay(OnPlayMessage obj)
        {
            _isActive = true;
        }


        private void Update()
        {
            if (!_isActive)
                return;

            _timer -= Time.deltaTime;
            if (_timer > 0)
            {
                return;
            }

            float direction = Random.value > 0.5f ? 1f : -1f;
            SpawnTarget(direction);
            _timer = _spawnInterval;
        }

        private void SpawnTarget(float direction)
        {
            var bg = _objectLocator.BackgroundRect;
            Vector3 position = new Vector3(bg.center.x + direction * bg.width / 2f, bg.yMin);

            _instantiator.InstantiatePrefabWorldSpace<Target>(_targetPrefab, position, Quaternion.identity, _targetsParent);
        }
    }
}
