using System.Linq;
using Cysharp.Threading.Tasks;
using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private readonly KeyCode[] _jumpKeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

        private bool _grounded = true;

        private float _shootCooldown;

        private float _currentShootCooldown;

        [Inject] private IMessageBus _messageBus;

        [Inject] private ObjectLocator _objectLocator;
        
        // Update is called once per frame
        void Update()
        {
            if (_grounded && (_jumpKeys.Any(Input.GetKeyDown)))
            {
                Jump();
            }


            if (Input.GetKeyUp(KeyCode.Space))
            {
                var canShoot = _currentShootCooldown <= 0;
                if (canShoot)
                {
                    Shoot();
                    _currentShootCooldown = _shootCooldown;
                }
            }

            _currentShootCooldown = Mathf.Max(0, _currentShootCooldown - Time.deltaTime);
        }

        void Jump()
        {
            _messageBus.Publish(new StartJumpingMessage()).Forget();
        }

        void Shoot()
        {

            var playerPosition = _objectLocator.PlayerTransform.position;
            var direction = -Mathf.Sign(_objectLocator.PlayerTransform.position.x);

            _messageBus.Publish(new ShootMessage(true, playerPosition, direction)).Forget();
        }
    }
}
