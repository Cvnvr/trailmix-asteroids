using UnityEngine;
using Zenject;

namespace Asteroids
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private ScreenBoundsCalculator screenBoundsCalculator;
		
		public override void InstallBindings()
		{
			Debug.Log($"[{nameof(GameInstaller)}.{nameof(InstallBindings)}]");
			
			Container.Bind<ScreenBoundsCalculator>().FromInstance(screenBoundsCalculator).AsSingle();

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