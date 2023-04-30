using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Assets.Scripts.Util;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class PackageDeliveredEffectsController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _particleSystemsPrefabs;
        [Inject] private IMessageBus _messageBus;
        [SerializeField] private Transform _parent;

        [Inject] private Instantiator _instantiator;

        private void OnEnable()
        {
            _messageBus.Subscribe<PackageDeliveredMessage>(OnPackageDelivered);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<PackageDeliveredMessage>(OnPackageDelivered);
        }

        private void OnPackageDelivered(PackageDeliveredMessage message)
        {
            var position = message.Package.transform.position;
            var effect = _particleSystemsPrefabs.PickRandom();

            _instantiator.InstantiatePrefabWorldSpace(effect, position, Quaternion.identity, _parent);
        }
    }
}
