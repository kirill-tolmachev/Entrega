using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class StartupController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;

        private void Awake()
        {
            _messageBus.Publish(new StartupMessage());
        }
    }
}
