using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Util;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Controllers
{
    internal class PickableSpawnerController : MonoBehaviour
    {
        [SerializeField]
        private float _minInterval;

        [SerializeField]
        private float _maxInterval;

        [SerializeField] private Pickable[] _prefabs;

        [SerializeField] private Transform _position;
        [SerializeField] private Transform _parent;

        [Inject] private Instantiator _instantiator;

        private float _currentCooldown;
        private float _maxCooldown;

        private float GetNextInterval() => Random.Range(_minInterval, _maxInterval);

        private void Start()
        {
            _maxCooldown = GetNextInterval();
        }

        private void Update()
        {
            _currentCooldown += Time.deltaTime;

            if (_currentCooldown < _maxCooldown)
            {
                return;
            }

            _currentCooldown = 0f;
            _maxCooldown = GetNextInterval();

            SpawnNext();
        }

        private void SpawnNext()
        {
            var item = _prefabs.PickRandom();

            _instantiator.InstantiatePrefabWorldSpace<Pickable>(item, new Vector3(0f, -50, _position.position.z),
                Quaternion.identity, _parent);
        }


    }
}
