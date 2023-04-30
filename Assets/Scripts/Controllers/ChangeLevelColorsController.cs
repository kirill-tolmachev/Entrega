using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Scripts.Infrastructure.Messages;
using Scripts.MessageImpl;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class ChangeLevelColorsController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;

        private HashSet<Colorizable> _colorizables = new HashSet<Colorizable>();

        private void OnEnable()
        {
            _messageBus.Subscribe<ObjectCreatedMessage>(OnObjectCreated);
            _messageBus.Subscribe<ObjectDestroyedMessage>(OnObjectDestroyed);

            _messageBus.Subscribe<LevelShouldChangeMessage>(OnLevelShouldChange);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<ObjectCreatedMessage>(OnObjectCreated);
            _messageBus.Unsubscribe<ObjectDestroyedMessage>(OnObjectDestroyed);
            _messageBus.Unsubscribe<LevelShouldChangeMessage>(OnLevelShouldChange);
        }

        private void OnObjectCreated(ObjectCreatedMessage message)
        {
            if (message.Object.TryGetComponent(out Colorizable colorizable))
            {
                _colorizables.Add(colorizable);
            }
        }

        private void OnObjectDestroyed(ObjectDestroyedMessage message)
        {
            if (message.Object.TryGetComponent(out Colorizable colorizable))
            {
                _colorizables.Remove(colorizable);
            }
        }


        private void OnLevelShouldChange(LevelShouldChangeMessage message)
        {
            var info = message.Level;

            foreach (var colorizable in _colorizables)
            {
                if (colorizable)
                {
                    colorizable.SetColor(info);
                }
            }
        }

    }
}
