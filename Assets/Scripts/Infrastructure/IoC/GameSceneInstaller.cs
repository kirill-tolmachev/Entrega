using Assets.Scripts;
using Cinemachine;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.IoC
{
    internal class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private ObjectLocator _objectLocator;
        [SerializeField] private GlobalSettings _globalSettings;
        [SerializeField] private CinemachineVirtualCamera _mainVirtualCamera;
 

        public override void InstallBindings()
        {
            Container.Bind<CinemachineVirtualCamera>().FromInstance(_mainVirtualCamera);
            Container.Bind<ObjectLocator>().FromInstance(_objectLocator);
            Container.Bind<GlobalSettings>().FromInstance(_globalSettings);
            Container.Bind<JumpSettings>().FromMethod(x => x.Container.Resolve<GlobalSettings>().JumpSettings);

            Container.Bind<IMessageBus>().To<MessageBus>().AsSingle();
            Container.Bind<ObjectRegistry>().ToSelf().AsSingle();
            Container.Bind<Instantiator>().ToSelf().AsSingle();
            
            Container.Bind<EnemyState>().ToSelf().AsTransient();
            Container.Bind<EnemyBehaviourApproaching>().ToSelf().AsTransient();
            Container.Bind<EnemyBehaviourAttacking>().ToSelf().AsTransient();
            Container.Bind<EnemyBehaviourHeadingBack>().ToSelf().AsTransient();
            Container.Bind<EnemyBehaviourHeadingForward>().ToSelf().AsTransient();
        }
    }
}
