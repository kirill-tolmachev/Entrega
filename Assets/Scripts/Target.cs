using Assets.Scripts;
using Assets.Scripts.MessageImpl;
using Assets.Scripts.Util;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Scripts
{
    internal class Target : MonoBehaviour
    {
        private bool _isActiveTarget = true;
        [SerializeField] private LayerMask _packageLayerMask;
        [SerializeField] private LayerMask _playerLayerMask;
        [SerializeField] private float _shrinkDuration;

        [Inject] private IMessageBus _messageBus;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isActiveTarget)
                return;

            bool playerCollision = _playerLayerMask.HasLayer(other.gameObject.layer);
            bool packageCollision = _packageLayerMask.HasLayer(other.gameObject.layer);

            if (!playerCollision && !packageCollision)
                return;
            
            if (other.TryGetComponent(out Package package))
            {
                package.FoundTarget = true;
            }

            _isActiveTarget = false;

            var scale = transform.localScale;
            transform.DOScale(new Vector3(scale.x, 0f, scale.z), _shrinkDuration).SetEase(Ease.InOutBounce);

            if (packageCollision)
            {
                _messageBus.Publish(new PackageDeliveredMessage(package, this)).Forget();
                _messageBus.Publish(new PlayerHealRequestMessage(5)).Forget();
            }

            if (playerCollision)
                _messageBus.Publish(new PlayerTargetCollisionMessage(this)).Forget();
        }

    }
}
