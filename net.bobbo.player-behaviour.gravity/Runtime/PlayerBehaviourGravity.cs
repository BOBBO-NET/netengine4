using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using BobboNet.Player;

namespace BobboNet.PlayerBehaviours
{
    public class PlayerBehaviourGravity : PlayerBehaviour
    {
        [System.Serializable]
        public class Settings
        {
            public Vector3 gravityNormal = new Vector3(0, -1, 0);
            public float gravityMagnitude = 9.8f;
            public float slideFriction = 0f;
        }

        public Settings settings;
        private GroundedState lastGroundedState = GroundedState.InAir;

        //
        //  Init
        //

        [Inject]
        public PlayerBehaviourGravity(PlayerController playerController, Settings settings) : base(playerController)
        {
            this.settings = settings;
        }

        //
        //  Movement
        //

        protected override Vector3 OnUpdateMovement()
        {
            if (playerController.lockGravity.IsLocked())
            {
                return Vector3.zero;
            }


            // Update the grounded state that we're keeping track of
            // (This will handle any momentum conversions - with sliding for example)
            SetGroundedState(playerController.GetIsGrounded());

            switch (lastGroundedState)
            {
                // When we're grounded, apply no gravity!
                case GroundedState.Grounded:
                    playerController.SetVelocity(Vector3.zero);
                    break;

                // When we're in the air, drop our target velocity by the normal gravity force
                case GroundedState.InAir:
                    playerController.AddForce(settings.gravityNormal * settings.gravityMagnitude * Time.deltaTime);
                    break;

                // When we're sliding, use the ground normal to determine how to impact our target velocity so that we slide correctly
                case GroundedState.Sliding:
                    playerController.AddForce(Vector3.ProjectOnPlane(-Vector3.up, playerController.GetGroundNormal()) * settings.gravityMagnitude * Time.deltaTime);
                    break;
            }

            // Add vertical velocity
            return Vector3.zero;
        }

        private void SetGroundedState(GroundedState newGroundedState)
        {
            switch (newGroundedState)
            {
                // If we're sliding now and last frame we were in the air...
                case GroundedState.Sliding:
                    if (lastGroundedState == GroundedState.InAir)
                    {
                        // Convert our downard velocity to sliding velocity
                        float currentMagnitude = playerController.GetVelocity().magnitude;
                        Vector3 gravityVelocity = Vector3.ProjectOnPlane(-Vector3.up, playerController.GetGroundNormal()) * currentMagnitude;
                        playerController.SetVelocity(gravityVelocity);
                    }
                    break;
            }


            lastGroundedState = newGroundedState;
        }
    }
}