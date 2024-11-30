using Events.Input;
using Input;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
	public class GameInstaller : MonoInstaller
	{
		//[SerializeField] private GridManager gridManager;

		public override void InstallBindings()
		{
			Debug.Log($"[{nameof(GameInstaller)}.{nameof(InstallBindings)}]");

			// Container.Bind<GridManager>().FromInstance(gridManager).AsSingle();
			
			Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle();

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