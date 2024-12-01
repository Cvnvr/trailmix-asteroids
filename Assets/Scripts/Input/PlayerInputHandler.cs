using System;
using Events.Input;
using UnityEngine.InputSystem;
using Zenject;

namespace Input
{
    public class PlayerInputHandler : IInitializable, IDisposable
    {
        [Inject] private SignalBus signalBus;

        private PlayerInput playerInput;

        public void Initialize()
        {
            playerInput = new PlayerInput();
            playerInput.Enable();

            playerInput.Player.ForwardThrust.performed += OnForwardThrustInput;
            playerInput.Player.ForwardThrust.canceled += OnForwardThrustInput;
            playerInput.Player.Rotate.performed += OnRotateInput;
            playerInput.Player.Rotate.canceled += OnRotateInput;
            playerInput.Player.Shoot.performed += OnShootInput;
        }
        
        private void OnForwardThrustInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                signalBus.TryFire(new ThrustInputEvent { thrustValue = context.ReadValue<float>() });
            }
            else if (context.canceled)
            {
                signalBus.TryFire(new ThrustInputEvent { thrustValue = 0 });
            }
        }
        
        private void OnRotateInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                signalBus.TryFire(new RotateInputEvent { rotationalValue = context.ReadValue<float>() });
            }
            else if (context.canceled)
            {
                signalBus.TryFire(new RotateInputEvent { rotationalValue = 0 });
            }
        }

        private void OnShootInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                signalBus.TryFire<ShootInputEvent>();
            }
        }

        public void Dispose()
        {
            if (playerInput == null)
            {
                return;
            }

            playerInput.Player.ForwardThrust.performed -= OnForwardThrustInput;
            playerInput.Player.Rotate.performed -= OnRotateInput;
            playerInput.Player.Shoot.performed -= OnShootInput;
            
            playerInput.Disable();
        }
    }
}