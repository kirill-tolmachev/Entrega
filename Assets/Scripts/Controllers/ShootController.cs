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
        [SerializeField] private Package _packagePrefab;

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

        private void OnShoot(ShootMessage _)
        {
            var playerPosition = _objectLocator.PlayerTransform.position;
            var container = _objectLocator.PackagesContainer;
            var direction = -Mathf.Sign(_objectLocator.PlayerTransform.position.x);
            var position = container.InverseTransformPoint(playerPosition) + new Vector3(direction, 0f);
            Debug.Log("Shooting from: " + position);

            var package = _instantiator.InstantiatePrefab<Package>(_packagePrefab, position, Quaternion.identity, _objectLocator.PackagesContainer);
            package.Direction = direction;
        }
    }
}
