using System;
using Entities.Player;
using Events.Input;
using UnityEngine;
using Zenject;

namespace Systems.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private PlayerMovementData data;

        [Inject] private SignalBus signalBus;

        private Rigidbody2D rigidbody2d;

        private float thrustInput;
        private float rotationalInput;
        
        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<ThrustInputEvent>(OnThrustInput);
            signalBus.Subscribe<RotateInputEvent>(OnRotateInput);
        }
        
        private void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        private void OnThrustInput(ThrustInputEvent evt)
        {
            thrustInput = evt.thrustValue;
        }
        
        private void OnRotateInput(RotateInputEvent evt)
        {
            rotationalInput = evt.rotationalValue;
        }

        private void FixedUpdate()
        {
            DoThrust();
            DoRotate();
        }

        private void DoThrust()
        {
            if (rigidbody2d == null)
                return;
            
            rigidbody2d.velocity = Vector3.ClampMagnitude(rigidbody2d.velocity, data.MaxForwardSpeed);

            var direction = thrustInput * data.ForwardThrust * Time.deltaTime * transform.up;
            rigidbody2d.AddForce(direction);
        }
        
        private void DoRotate()
        {
            if (rigidbody2d == null)
                return;
            
            var rotation = rotationalInput * data.RotationalThrust * Time.deltaTime;
            rotation = Mathf.Clamp(rotation, -data.MaxRotationalSpeed, data.MaxRotationalSpeed);
            rigidbody2d.AddTorque(-rotation);
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<ThrustInputEvent>(OnThrustInput);
            signalBus.TryUnsubscribe<RotateInputEvent>(OnRotateInput);
        }
    }
}