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
			
			// Game
			Container.Bind<ScreenBoundsCalculator>().FromInstance(screenBoundsCalculator).AsSingle();
			
			// Player
			Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle();
			
			// Projectiles
			Container.Bind<IWeaponBehaviourFactory>().To<WeaponBehaviourFactory>().AsSingle();
			Container.Bind<IProjectileBehaviourFactory>().To<ProjectileBehaviourFactory>().AsSingle();

			InstallSignals(Container);

			Debug.Log($"[{nameof(GameInstaller)}.{nameof(InstallBindings)}] Bindings registered.");
		}

		private static void InstallSignals(DiContainer diContainer)
		{
			SignalBusInstaller.Install(diContainer);
			
			// Game
			diContainer.DeclareSignal<SpawnNewWaveEvent>();

			// Input
			diContainer.DeclareSignal<ThrustInputEvent>();
			diContainer.DeclareSignal<RotateInputEvent>();
			diContainer.DeclareSignal<ShootInputEvent>();
			diContainer.DeclareSignal<HyperspaceInputEvent>();
			
			// Player
			diContainer.DeclareSignal<PlayerDestroyedEvent>();
			
			// Enemies
			diContainer.DeclareSignal<AsteroidSpawnEvent>();
			diContainer.DeclareSignal<AsteroidDestroyedEvent>();
			
			// Power-ups
			diContainer.DeclareSignal<PowerUpCollectedEvent>();
			diContainer.DeclareSignal<PowerUpSpawnEvent>();
		}
	}
}