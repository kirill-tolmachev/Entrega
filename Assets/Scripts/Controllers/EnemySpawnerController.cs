﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class EnemySpawnerController : MonoBehaviour
    {
        private class EnemyInfo
        {
            public EnemyInfo(int slot)
            {
                Slot = slot;
            }

            public int Slot { get; }
        }

        [Inject] private IMessageBus _messageBus;
        [Inject] private Instantiator _instantiator;

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _container;
        [SerializeField] private Enemy _enemyPrefab;

        private readonly Dictionary<Enemy, EnemyInfo> _enemies = new Dictionary<Enemy, EnemyInfo>();
        private float _frequency = 1f;

        private float _spawnCooldown;

        private void OnEnable()
        {
            _messageBus.Subscribe<LevelShouldChangeMessage>(OnLevelChanged);
            _messageBus.Subscribe<ObjectDestroyedMessage>(OnObjectDestroyed);
            _messageBus.Subscribe<ResetMessage>(OnReset);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<LevelShouldChangeMessage>(OnLevelChanged);
            _messageBus.Unsubscribe<ObjectDestroyedMessage>(OnObjectDestroyed);
            _messageBus.Unsubscribe<ResetMessage>(OnReset);
        }

        private void OnReset(ResetMessage _)
        {
            _enemies.Clear();
            _spawnCooldown = 0f;
            _frequency = 1f;
        }

        private void OnObjectDestroyed(ObjectDestroyedMessage message)
        {
            if (message.Object.TryGetComponent(out Enemy enemy))
                _enemies.Remove(enemy);
        }

        private void OnLevelChanged(LevelShouldChangeMessage message)
        {
            _frequency = message.EnemyFrequency;
        }

        private void Update()
        {
            float cooldown = Mathf.Max(0.1f,10f / _frequency);
            _spawnCooldown += Time.deltaTime;

            if (_spawnCooldown < cooldown)
            {
                return;
            }

            Spawn();
            _spawnCooldown = 0f;
        }

        private void Spawn()
        {
            var slot = GetFreeSlot();
            if (slot == null)
                return;

            var position = GetPositionX(slot.Value);
            var worldPosition = new Vector3(position, _spawnPoint.position.y, _spawnPoint.position.z);

            var enemy = _instantiator.InstantiatePrefabWorldSpace<Enemy>(_enemyPrefab, worldPosition, Quaternion.identity, _container);

            _enemies.Add(enemy, new EnemyInfo(slot.Value));
        }

        private float GetPositionX(int slot)
        {
            if (slot is < 0 or >= 4)
            {
                throw new ArgumentOutOfRangeException(nameof(slot));
            }

            var positions = new[]
            {
                -3f,
                -1.5f,
                1.5f,
                3f
            };
            
            return positions[slot];
        }

        private int? GetFreeSlot()
        {
            for (int i = 0; i < 4; i++)
            {
                if (_enemies.Any(x => x.Value.Slot == i))
                    continue;

                return i;
            }

            return null;
        }


    }
}
