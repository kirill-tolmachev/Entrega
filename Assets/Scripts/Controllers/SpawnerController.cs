using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scripts;
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

        private void Update()
        {
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
