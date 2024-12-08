using System;
using UnityEngine.InputSystem;
using Zenject;

namespace Asteroids
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
            
            playerInput.Player.Hyperspace.performed += OnHyperspaceInput;
        }
        
        private void OnForwardThrustInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                signalBus.TryFire(new ThrustInputEvent { ThrustValue = context.ReadValue<float>() });
            }
            else if (context.canceled)
            {
                signalBus.TryFire(new ThrustInputEvent { ThrustValue = 0 });
            }
        }
        
        private void OnRotateInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                signalBus.TryFire(new RotateInputEvent { RotationalValue = context.ReadValue<float>() });
            }
            else if (context.canceled)
            {
                signalBus.TryFire(new RotateInputEvent { RotationalValue = 0 });
            }
        }

        private void OnShootInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                signalBus.TryFire<ShootInputEvent>();
            }
        }
        
        private void OnHyperspaceInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                signalBus.TryFire<HyperspaceInputEvent>();
            }
        }

        public void Dispose()
        {
            if (playerInput == null)
                return;

            playerInput.Player.ForwardThrust.performed -= OnForwardThrustInput;
            playerInput.Player.ForwardThrust.canceled -= OnForwardThrustInput;

            playerInput.Player.Rotate.performed -= OnRotateInput;
            playerInput.Player.Rotate.canceled -= OnRotateInput;

            playerInput.Player.Shoot.performed -= OnShootInput;
            
            playerInput.Player.Hyperspace.performed -= OnHyperspaceInput;
            
            playerInput.Disable();
        }
    }
}