﻿using System.Collections.Generic;
using Assets.Scripts;
using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using UnityEngine;
using Zenject;

namespace Scripts.Controllers
{
    internal class PackageMovementController : MonoBehaviour
    {
        [SerializeField] 
        private PackageVelocitySettings _packageVelocitySettings;

        [Inject]
        private IMessageBus _messageBus;

        private readonly Dictionary<Package, (Vector3 position, float launchTime)> _packages = new();

        private void OnEnable()
        {
            _messageBus.Subscribe<ObjectCreatedMessage>(OnObjectCreated);
            _messageBus.Subscribe<ObjectDestroyedMessage>(OnObjectRemoved);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<ObjectCreatedMessage>(OnObjectCreated);
            _messageBus.Unsubscribe<ObjectDestroyedMessage>(OnObjectRemoved);
        }

        private void OnObjectCreated(ObjectCreatedMessage message)
        {
            if (message.Object.TryGetComponent(out Package package))
                _packages.Add(package, (package.transform.localPosition, Time.time));

        }

        private void OnObjectRemoved(ObjectDestroyedMessage message)
        {
            if (message.Object.TryGetComponent(out Package package))
                _packages.Remove(package);
        }

        private void Update()
        {
            foreach (var (package, (originalPosition, launchTime)) in _packages)
            {
                var dt = Time.time - launchTime;
                var position = _packageVelocitySettings.GetPosition(dt) * 10 * package.Direction + originalPosition.x;

                var pos = package.transform.localPosition;
                package.transform.localPosition = new Vector3(position, pos.y, pos.z);
            }
        }
    }
}
