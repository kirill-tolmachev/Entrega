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

namespace Assets.Scripts.Controllers
{
    internal class EnemyShootController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;
        [Inject] private ObjectLocator _objectLocator;
        [Inject] private Instantiator _instantiator;
        [SerializeField] private Transform _bulletParent;

        [SerializeField] private GameObject _bulletPrefab;

        private void OnEnable()
        {
            _messageBus.Subscribe<EnemyShootMessage>(OnEnemyShoot);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<EnemyShootMessage>(OnEnemyShoot);
        }

        private void OnEnemyShoot(EnemyShootMessage message)
        {
            var enemy = message.Enemy;
            var bullet = _instantiator.InstantiatePrefabWorldSpace(_bulletPrefab, enemy.transform.position, Quaternion.identity, _bulletParent);


        }
    }
}
