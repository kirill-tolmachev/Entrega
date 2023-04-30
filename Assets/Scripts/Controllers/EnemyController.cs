using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Assets.Scripts.Util;
using Cysharp.Threading.Tasks;
using Scripts;
using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    internal abstract class EnemyBehaviour
    {
        public abstract bool Finished { get; }

        protected Enemy Self { get; private set; }
        protected EnemyState State { get; private set; }

        public void Init(Enemy enemy, EnemyState state)
        {
            Self = enemy;
            State = state;
        }
        public abstract void Update();
    }

    internal abstract class EnemyBehaviourMove : EnemyBehaviour
    {
        private float _speed = 2f;

        public override bool Finished => _finished;

        private bool _finished = false;

        public override void Update()
        {
            var pos = Self.transform.position;
            var target = GetTargetY();
            var y = Mathf.Lerp(pos.y, target, _speed * Time.deltaTime);

            Self.transform.position = new Vector3(pos.x, y, pos.z);
            if (Mathf.Abs(target - y) < 0.01)
                _finished = true;
        }

        protected abstract float GetTargetY();
    }

    internal class EnemyBehaviourApproaching : EnemyBehaviourMove
    {
        private readonly ObjectLocator _objectLocator;

        public EnemyBehaviourApproaching(ObjectLocator objectLocator)
        {
            _objectLocator = objectLocator;
        }

        protected override float GetTargetY()
        {
            return _objectLocator.PlayerTransform.position.y;
        }
    }

    internal class EnemyBehaviourHeadingForward : EnemyBehaviourMove
    {
        protected override float GetTargetY()
        {
            return -8f;
        }
    }

    internal class EnemyBehaviourHeadingBack : EnemyBehaviourMove
    {
        protected override float GetTargetY()
        {
            return 8f;
        }
    }

    internal class EnemyBehaviourAttacking : EnemyBehaviour
    {
        private readonly IMessageBus _messageBus;
        private readonly ObjectLocator _objectLocator;

        private readonly float _maxDuration = 5f;
        private float _currentDuration;
        private readonly float _maxCooldown = 0.8f;

        private float _currentCooldown;

        public override bool Finished => _finished;

        private bool _finished;

        public EnemyBehaviourAttacking(IMessageBus messageBus, ObjectLocator objectLocator)
        {
            _messageBus = messageBus;
            _objectLocator = objectLocator;
        }

        public override void Update()
        {
            if (_currentDuration >= _maxDuration)
            {
                _finished = true;
                return;
            }

            _currentDuration += Time.deltaTime;
            _currentCooldown += Time.deltaTime;
            if (_currentCooldown < _maxCooldown)
            {
                return;
            }

            _currentCooldown = 0f;

            float direction = Mathf.Sign(_objectLocator.PlayerTransform.position.x);
            _messageBus.Publish(new ShootMessage(false, Self.transform.position, direction)).Forget();
        }
    }

    internal class EnemyState
    {
        private readonly DiContainer _container;

        private Enemy _enemy;

        private static readonly Dictionary<Type, IList<Type>> _behaviours = MakeBehaviourGraph();

        private static Dictionary<Type, IList<Type>> MakeBehaviourGraph()
        {
            var r = new Dictionary<Type, IList<Type>>();

            void Add<TFrom, TTo>()
            {
                if (!r.TryGetValue(typeof(TFrom), out var values))
                    values = r[typeof(TFrom)] = new List<Type>();

                values.Add(typeof(TTo));
            }

            Add<EnemyBehaviourApproaching, EnemyBehaviourAttacking>();
            
            Add<EnemyBehaviourAttacking, EnemyBehaviourHeadingForward>();
            Add<EnemyBehaviourAttacking, EnemyBehaviourHeadingBack>();
            Add<EnemyBehaviourAttacking, EnemyBehaviourApproaching>();

            Add<EnemyBehaviourHeadingForward, EnemyBehaviourApproaching>();
            Add<EnemyBehaviourHeadingBack, EnemyBehaviourApproaching>();

            return r;
        }

        public EnemyBehaviour Behaviour { get; set; }

        public EnemyState(DiContainer container)
        {
            _container = container;
        }

        public void Init(Enemy enemy)
        {
            _enemy = enemy;

            var beh = _container.Resolve<EnemyBehaviourApproaching>();
            beh.Init(_enemy, this);

            Behaviour = beh;
        }

        public void Update()
        {
            Behaviour.Update();

            if (Behaviour.Finished)
            {
                var nextBehaviourType = _behaviours[Behaviour.GetType()].PickRandom();
                var behaviour = (EnemyBehaviour)_container.Resolve(nextBehaviourType);
                behaviour.Init(_enemy, this);
                Behaviour = behaviour;

                Debug.Log("State is now: " + nextBehaviourType.Name);
            }
        }
    }

    internal class EnemyController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;
        [Inject] private DiContainer _container;

        private readonly Dictionary<Enemy, EnemyState> _enemies = new Dictionary<Enemy, EnemyState>();

        private void OnEnable()
        {
            _messageBus.Subscribe<ObjectCreatedMessage>(OnObjectCreated);
            _messageBus.Subscribe<ObjectDestroyedMessage>(OnObjectDestroyed);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<ObjectCreatedMessage>(OnObjectCreated);
            _messageBus.Unsubscribe<ObjectDestroyedMessage>(OnObjectDestroyed);
        }

        private void OnObjectCreated(ObjectCreatedMessage message)
        {
            if (message.Object.TryGetComponent(out Enemy enemy))
            {
                var state = _container.Resolve<EnemyState>();
                state.Init(enemy);
                _enemies.Add(enemy, state);
            }
        }

        private void OnObjectDestroyed(ObjectDestroyedMessage message)
        {
            if (message.Object.TryGetComponent(out Enemy enemy))
            {
                if (enemy)
                {
                    _enemies.Remove(enemy);
                }
            }
        }

        private void Update()
        {
            foreach (var (enemy, enemyState) in _enemies)
            {
                if (enemy.IsDead)
                    continue;

                enemyState.Update();
            }
        }
    }
}
