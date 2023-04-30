using System;
using Assets.Scripts;
using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using UnityEngine;
using Zenject;

namespace Scripts.Controllers
{
    internal class ShootController : MonoBehaviour
    {
        [SerializeField] private Bullet _packagePrefab;
        [SerializeField] private Bullet _enemyBulletPrefab;

        [Inject] private IMessageBus _messageBus;
        [Inject] private Instantiator _instantiator;
        [Inject] private ObjectLocator _objectLocator;

        private void OnEnable()
        {
            _messageBus.Subscribe<ShootMessage>(OnShoot);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<ShootMessage>(OnShoot);
        }

        private void OnShoot(ShootMessage message)
        {
            var container = _objectLocator.PackagesContainer;
            var position = container.InverseTransformPoint(message.Position) + new Vector3(message.Direction, 0f);
            Debug.Log("Shooting from: " + position);

            var prefab = message.IsPlayerOrigin ? _packagePrefab : _enemyBulletPrefab;
            var package = _instantiator.InstantiatePrefab<Bullet>(prefab, position, Quaternion.identity, _objectLocator.PackagesContainer);
            package.Direction = message.Direction;
        }
    }
}
