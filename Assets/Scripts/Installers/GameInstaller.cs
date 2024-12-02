using Events.Input;
using Input;
using Systems.Projectiles;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Debug.Log($"[{nameof(GameInstaller)}.{nameof(InstallBindings)}]");

			Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle();

			Container.Bind<IProjectileBehaviourFactory>().To<ProjectileBehaviourFactory>().AsSingle();

			InstallSignals(Container);

			Debug.Log($"[{nameof(GameInstaller)}.{nameof(InstallBindings)}] Bindings registered.");
		}

		private static void InstallSignals(DiContainer diContainer)
		{
			SignalBusInstaller.Install(diContainer);

			diContainer.DeclareSignal<ThrustInputEvent>();
			diContainer.DeclareSignal<RotateInputEvent>();
			diContainer.DeclareSignal<ShootInputEvent>();
		}
	}
}