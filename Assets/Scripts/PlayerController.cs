using Cysharp.Threading.Tasks;
using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private bool _grounded = true;

        private float _shootCooldown;

        private float _currentShootCooldown;

        [Inject] private IMessageBus _messageBus;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (_grounded && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A)))
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
            _messageBus.Publish(new ShootMessage()).Forget();
        }
    }
}
