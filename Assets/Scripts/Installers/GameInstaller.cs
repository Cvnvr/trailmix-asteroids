using Systems.Weapons;
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

			Container.Bind<IWeaponBehaviourFactory>().To<WeaponBehaviourFactory>().AsSingle();
			Container.Bind<IProjectileBehaviourFactory>().To<ProjectileBehaviourFactory>().AsSingle();

			InstallSignals(Container);

			Debug.Log($"[{nameof(GameInstaller)}.{nameof(InstallBindings)}] Bindings registered.");
		}

		private static void InstallSignals(DiContainer diContainer)
		{
			SignalBusInstaller.Install(diContainer);

			// Input
			diContainer.DeclareSignal<ThrustInputEvent>();
			diContainer.DeclareSignal<RotateInputEvent>();
			diContainer.DeclareSignal<ShootInputEvent>();
			
			// Player
			diContainer.DeclareSignal<PlayerDestroyedEvent>();
			
			// Enemies
			diContainer.DeclareSignal<AsteroidSpawnMoreEvent>();
			
			// Power-ups
			diContainer.DeclareSignal<PowerUpCollectedEvent>();
			diContainer.DeclareSignal<PowerUpSpawnEvent>();
		}
	}
}