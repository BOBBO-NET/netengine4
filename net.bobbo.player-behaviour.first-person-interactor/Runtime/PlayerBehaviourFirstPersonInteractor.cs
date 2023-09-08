using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BobboNet.PlayerBehaviours
{
    public class PlayerBehaviourFirstPersonInteractor : PlayerBehaviour
    {
        [Header("Required References")]
        // Where in space to position and pivot the interaction raycast
        public Transform transformRaycastPivot;

        [Header("Settings")]
        public LayerMask layerMaskInteraction = Physics.DefaultRaycastLayers;
        public float reachDistance = 5f;

        // The interactable object we're looking at, if any
        private InteractableMonobehaviour focusedInteractable = null;





        protected override Vector3 OnUpdateMovement()
        {
            // Update our focused interactable
            InteractableMonobehaviour foundInteractable = CastForFocusedInteractable();
            if (focusedInteractable != foundInteractable)
            {
                if (focusedInteractable != null)
                {
                    focusedInteractable.OnHoverStop();
                }

                if (foundInteractable != null)
                {
                    foundInteractable.OnHoverStart();
                }

                focusedInteractable = foundInteractable;
            }


            // If we're hovering over an interactable...
            if (focusedInteractable != null)
            {
                focusedInteractable.OnHoverUpdate();

                // If we've pressed the interact button, and our interactions aren't locked...
                if (Input.GetButtonDown("Interact") && !playerController.lockInteraction.IsLocked() && focusedInteractable.CanInteract())
                {
                    focusedInteractable.OnInteract();
                }
            }





            return base.OnUpdateMovement();
        }

        private InteractableMonobehaviour CastForFocusedInteractable()
        {
            Ray ray = new Ray(transformRaycastPivot.position, transformRaycastPivot.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, reachDistance, layerMaskInteraction))
            {
                return hit.transform.GetComponent<InteractableMonobehaviour>();
            }
            else
            {
                return null;
            }
        }

        private void OnDrawGizmos()
        {
            if (transformRaycastPivot == null)
            {
                return;
            }


            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transformRaycastPivot.position, transformRaycastPivot.position + transformRaycastPivot.forward * reachDistance);
        }






        public InteractableMonobehaviour GetFocusedInteractable()
        {
            return focusedInteractable;
        }
    }
}