using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using BobboNet.Player;

namespace BobboNet.PlayerBehaviours
{
    public abstract class PlayerBehaviour : IInitializable
    {
        public PlayerController playerController { private set; get; }

        private bool isEnabled = false;

        //
        //  Init
        //

        [Inject]
        public PlayerBehaviour(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public void Initialize()
        {
            OnSetup();
        }

        //
        //  Methods
        //

        public void SetEnabled(bool isEnabled)
        {
            this.isEnabled = isEnabled;
        }

        public bool GetEnabled() => this.isEnabled;

        public Vector3 UpdateMovement()
        {
            if (GetEnabled())
            {
                return OnUpdateMovement();
            }
            else
            {
                return Vector3.zero;
            }
        }

        public void HandleControllerColliderHit(ControllerColliderHit hit)
        {
            OnHandleControllerColliderHit(hit);
        }

        public void HandleTeleport(Vector3 newPosition, float newRotation)
        {
            OnTeleported(newPosition, newRotation);
        }


        //
        //  Virtual Methods
        //

        /// <summary>
        /// Called automatically by the PlayerController when this behaviour should be setup.
        /// </summary>
        protected virtual void OnSetup()
        {
            // Do nothing by default...
        }

        /// <summary>
        /// Execute logic that applies forces to the PlayerController.
        /// Called every frame by the PlayerController.
        /// </summary>
        /// <returns>A force to apply to the player.</returns>
        protected virtual Vector3 OnUpdateMovement()
        {
            return Vector3.zero;
        }

        /// <summary>
        /// Execute logic when the Player hits something, or gets hit by something.
        /// Called automatically on collision by the PlayerController
        /// </summary>
        /// <param name="hit"></param>
        protected virtual void OnHandleControllerColliderHit(ControllerColliderHit hit)
        {
            // Do nothing by default
        }

        /// <summary>
        /// Execute logic just after the Player has been teleported to some new location.
        /// Called automatically upon teleport by the PlayerController.
        /// </summary>
        /// <param name="newPosition">Where the player was teleported to, in world space.</param>
        /// <param name="newRotation">The local Y rotation of the player.</param>
        protected virtual void OnTeleported(Vector3 newPosition, float newRotation)
        {
            // Do nothing by default...
        }
    }
}