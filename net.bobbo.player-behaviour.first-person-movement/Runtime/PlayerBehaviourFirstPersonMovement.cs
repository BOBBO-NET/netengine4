using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BobboNet.PlayerBehaviours
{
    [RequireComponent(typeof(PlayerBehaviourGravity))]
    public class PlayerBehaviourFirstPersonMovement : PlayerBehaviour
    {
        public enum MovementState
        {
            Grounded,
            Sliding,
            Air
        }


        [Header("Speed Settings")]
        public float speedMovementWalk = 10;
        public float speedMovementRun = 15;

        [Header("Decay Settings")]
        public float groundedSpeedAcceleration = 15;
        public float groundedSpeedDecay = 10;
        public float airSpeedAcceleration = 1;
        public float airSpeedDecay = 1;

        [Header("Jump Settings")]
        public float jumpHeight = 7;
        public float minimumUngroundedTime = 0.5f;

        private PlayerBehaviourGravity behaviourGravity;
        // The current state of this first person movement behaviour
        private MovementState currentState = MovementState.Grounded;
        // The control velocity of this behaviour
        private Vector3 velocity = Vector3.zero;
        



        protected override void OnSetup()
        {
            behaviourGravity = playerController.GetBehaviour<PlayerBehaviourGravity>();
        }

        protected override Vector3 OnUpdateMovement()
        {
            currentState = UpdateCurrentState();

            switch (currentState)
            {
                case MovementState.Grounded:
                    ProcessStateGrounded();
                    break;

                case MovementState.Sliding:
                    ProcessStateSliding();
                    break;

                case MovementState.Air:
                    ProcessStateAir();
                    break;
            }

            return velocity;
        }



        // Check to see what the current movement state should be, and return it 
        private MovementState UpdateCurrentState()
        {
            switch (playerController.GetIsGrounded())
            {
                case GroundedState.Grounded:
                    return MovementState.Grounded;

                case GroundedState.Sliding:
                    return MovementState.Sliding;

                case GroundedState.InAir:
                    return MovementState.Air;
            }

            return MovementState.Grounded;
        }

        private void ProcessStateGrounded()
        {
            ProcessMovementVelocity(speedMovementWalk, speedMovementRun, groundedSpeedAcceleration, groundedSpeedDecay, Vector3.one);

            CheckForJump(Vector3.up);
        }

        private void ProcessStateSliding()
        {
            ProcessMovementVelocity(speedMovementWalk, speedMovementRun, groundedSpeedAcceleration, groundedSpeedDecay, new Vector3(1, 0, 1));

            CheckForJump(playerController.GetGroundNormal());
        }

        private void ProcessStateAir()
        {
            //velocity = velocity.GoTowardsSmooth(Vector3.zero, airSpeedDecay * Time.deltaTime);
            ProcessMovementVelocity(speedMovementWalk, speedMovementRun, airSpeedAcceleration, airSpeedDecay, Vector3.one);
        }

        




        // Set the current velocity by reading player inputs and using them to calculate the movement direction and speed
        private void ProcessMovementVelocity(float walkSpeed, float runSpeed, float acceleration, float decay, Vector3 movementMask)
        {
            Vector3 movementInput = GetMovementInput();

            // True if the player is currently trying to move, and movement is not locked
            bool isMoving = movementInput.sqrMagnitude > float.Epsilon && !playerController.lockMovement.IsLocked();

            if (isMoving)
            {
                // Get the desired speed (either the run speed or the walk speed)
                float desiredSpeed = IsRunning() ? runSpeed : walkSpeed;

                // Convert the movement input into world space directions
                Vector3 desiredVelocity = transform.TransformDirection(movementInput);
                desiredVelocity *= desiredSpeed;   // Apply speed to this velocity
                desiredVelocity = Vector3.ProjectOnPlane(desiredVelocity, playerController.GetGroundNormal());
                desiredVelocity.y = velocity.y;
                desiredVelocity.Scale(movementMask);

                velocity = velocity.GoTowardsSmooth(desiredVelocity, acceleration * Time.deltaTime);
            }
            else
            {
                velocity = velocity.GoTowardsSmooth(Vector3.zero, decay * Time.deltaTime);
            }
        }

        private Vector3 GetMovementInput()
        {
            // If interaction is locked, then don't capture input!
            if(playerController.lockInteraction.IsLocked())
            {
                return new Vector3();
            }

            // Get a local space vector for how the player should move according to WASD or Joystick
            Vector3 movementInput = new Vector3(
                Input.GetAxisRaw("Horizontal"),
                0,
                Input.GetAxisRaw("Vertical"));

            // Normalize the movement input to fix diagonal movement being quick
            movementInput.Normalize();

            return movementInput;
        }

        private void CheckForJump(Vector3 jumpNormal)
        {
            // If interaction is locked, then don't capture input!
            if (playerController.lockInteraction.IsLocked())
            {
                return;
            }

            if (Input.GetButtonDown("Jump") && !playerController.lockMovement.IsLocked())
            {
                //behaviourGravity.ImpulseGravity(jumpNormal * jumpHeight);
                playerController.SetVelocity(jumpNormal * jumpHeight);
                playerController.ForceUngroundedTime(minimumUngroundedTime);
            }
        }

        private bool IsRunning()
        {
            // If interaction is locked, then don't capture input!
            if (playerController.lockInteraction.IsLocked())
            {
                return false;
            }

            return Input.GetKey(KeyCode.LeftShift);
        }
    }
}