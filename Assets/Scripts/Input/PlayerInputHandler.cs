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

            playerInput.Player.ForwardThrust.performed += OnForwardThrustPressed;
            playerInput.Player.Rotate.performed += OnRotatePressed;
            playerInput.Player.Rotate.canceled += OnRotatePressed;
            playerInput.Player.Shoot.performed += OnShootPressed;
        }
        
        private void OnForwardThrustPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                signalBus.TryFire<ThrustInputEvent>();
            }
        }
        
        private void OnRotatePressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                signalBus.TryFire(new RotateInputEvent { Value = context.ReadValue<float>() });
            }
            else if (context.canceled)
            {
                signalBus.TryFire(new RotateInputEvent { Value = 0 });
            }
        }

        private void OnShootPressed(InputAction.CallbackContext context)
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

            playerInput.Player.ForwardThrust.performed -= OnForwardThrustPressed;
            playerInput.Player.Rotate.performed -= OnRotatePressed;
            playerInput.Player.Shoot.performed -= OnShootPressed;
            
            playerInput.Disable();
        }
    }
}