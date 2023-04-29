﻿using Assets.Scripts;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.IoC
{
    internal class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private ObjectLocator _objectLocator;
        [SerializeField] private GlobalSettings _globalSettings;

        public override void InstallBindings()
        {
            Container.Bind<ObjectLocator>().FromInstance(_objectLocator);
            Container.Bind<GlobalSettings>().FromInstance(_globalSettings);
            Container.Bind<JumpSettings>().FromMethod(x => x.Container.Resolve<GlobalSettings>().JumpSettings);

            Container.Bind<IMessageBus>().To<MessageBus>().AsSingle();
            Container.Bind<ObjectRegistry>().ToSelf().AsSingle();
            Container.Bind<Instantiator>().ToSelf().AsSingle();
        }
    }
}
