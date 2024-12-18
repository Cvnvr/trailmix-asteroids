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
			Container.Bind<PlayerLocator>().AsSingle();
			
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
			diContainer.DeclareSignal<GameOverEvent>();
			
			// Score
			diContainer.DeclareSignal<ScoreAwardedEvent>();
			
			// Input
			diContainer.DeclareSignal<ThrustInputEvent>().OptionalSubscriber();
			diContainer.DeclareSignal<RotateInputEvent>().OptionalSubscriber();
			diContainer.DeclareSignal<ShootInputEvent>().OptionalSubscriber();
			diContainer.DeclareSignal<HyperspaceInputEvent>().OptionalSubscriber();
			
			// Player
			diContainer.DeclareSignal<PlayerTriggerSpawnEvent>();
			diContainer.DeclareSignal<PlayerNewSpawnEvent>();
			diContainer.DeclareSignal<PlayerDestroyedEvent>();
			diContainer.DeclareSignal<PlayerLivesCountUpdatedEvent>();
			
			// Enemies
			diContainer.DeclareSignal<AsteroidSpawnEvent>();
			diContainer.DeclareSignal<AsteroidDestroyedEvent>();
			diContainer.DeclareSignal<UfoSpawnEvent>();
			diContainer.DeclareSignal<UfoDestroyedEvent>();
			diContainer.DeclareSignal<UfoRemovedSelfEvent>();
			
			// Power-ups
			diContainer.DeclareSignal<PowerUpCollectedEvent>();
			diContainer.DeclareSignal<PowerUpSpawnEvent>();
		}
	}
}