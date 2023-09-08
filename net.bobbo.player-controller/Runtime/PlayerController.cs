using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using BobboNet;
using BobboNet.Extensions;
using BobboNet.PlayerBehaviours;

namespace BobboNet.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<PlayerController> { }

        [Header("Settings")]
        // How quickly external forces decay to zero
        public float naturalForceDecayRate = 1;
        public float groundSnapDistance = 0.1f;
        public float maxVelocityMagnitude = 50;

        public MultiLock lockMovement
        {
            get => _lockMovement;
        }
        private MultiLock _lockMovement = new MultiLock();

        public MultiLock lockInteraction
        {
            get => _lockInteraction;
        }
        private MultiLock _lockInteraction = new MultiLock();

        public MultiLock lockItemInteraction
        {
            get => _lockItemInteraction;
        }
        private MultiLock _lockItemInteraction = new MultiLock();

        public MultiLock lockCamera
        {
            get => _lockCamera;
        }
        private MultiLock _lockCamera = new MultiLock();

        public MultiLock lockGravity
        {
            get => _lockGravity;
        }
        private MultiLock _lockGravity = new MultiLock();


        private CharacterController characterController;
        // The behaviours on this player controller that determine how this controller acts
        private PlayerBehaviour[] behaviours;
        // Main forces that influence this character controller, and naturally decay over time
        private Vector3 mainVelocity = Vector3.zero;
        private GroundedState groundedState = GroundedState.InAir;
        private Vector3 lastGroundNormal = Vector3.up;
        private float timeCanBeGrounded = 0;
        private float stateAirTime = 0;
        private float stateSlideTime = 0;
        private float stateGroundedTime = 0;

        //
        //  Setup & Config
        //

        [Inject]
        public void Inject(List<PlayerBehaviour> playerBehaviours)
        {
            this.behaviours = playerBehaviours.ToArray();
        }

        private void ChooseSpawnPoint()
        {
            // TODO - Re-implement
            // if (GameInstance.selfReference != null && GameInstance.selfReference.spawnPoints != null && GameInstance.selfReference.spawnPoints.Length > 0)
            // {
            //     Transform spawnPoint = GameInstance.selfReference.spawnPoints[Random.Range(0, GameInstance.selfReference.spawnPoints.Length)];
            //     Teleport(spawnPoint);
            // }
        }

        private void Awake()
        {
            ChooseSpawnPoint();
            characterController = GetComponent<CharacterController>();
        }



        //
        //  Gizmos
        //

        private void OnDrawGizmos()
        {
            // TODO - Re-implement
            // NPCGizmos.DrawCharacterGizmo(transform, -1f);

            // Gizmos.color = Color.cyan;
            // Gizmos.DrawLine(transform.position, transform.position + mainVelocity);
        }



        //
        //  Update
        //

        private void Update()
        {
            UpdateStateTimings();
            UpdateGroundedStatus();
            UpdateExternalForces();
            Vector3 behaviourMovement = UpdateBehaviourMovement();

            if (!lockMovement.IsLocked())
            {
                characterController.Move((behaviourMovement + mainVelocity) * Time.deltaTime);
            }
        }

        private void UpdateStateTimings()
        {
            switch (groundedState)
            {
                case GroundedState.Grounded:
                    stateGroundedTime += Time.deltaTime;
                    stateAirTime = 0;
                    stateSlideTime = 0;
                    break;

                case GroundedState.Sliding:
                    stateGroundedTime = 0;
                    stateAirTime = 0;
                    stateSlideTime += Time.deltaTime;
                    break;

                case GroundedState.InAir:
                    stateGroundedTime = 0;
                    stateAirTime += Time.deltaTime;
                    stateSlideTime = 0;
                    break;
            }
        }

        private void UpdateGroundedStatus()
        {
            // If the character controller thinks we're not grounded...
            if (!characterController.isGrounded)
            {
                // Spherecast downward to find out if we're actually grounded
                Ray ray = new Ray(transform.position, -transform.up);
                float castDistance = (characterController.height * 0.5f - characterController.radius + groundSnapDistance);
                RaycastHit hit;

                if (Physics.SphereCast(ray, characterController.radius, out hit, castDistance) && Time.time - timeCanBeGrounded >= 0)
                {
                    FixGroundState(hit.normal);
                    characterController.Move(new Vector3(0, -hit.distance, 0));
                }
                else
                {
                    groundedState = GroundedState.InAir;
                    lastGroundNormal = Vector3.up;
                }
            }
        }

        // Process any updates on external forces (naturally decay)
        private void UpdateExternalForces()
        {
            Vector3 newlyDecayed = mainVelocity.Decay(naturalForceDecayRate * Time.deltaTime);
            mainVelocity = Vector3.ClampMagnitude(new Vector3(newlyDecayed.x, mainVelocity.y, newlyDecayed.z), maxVelocityMagnitude);
        }

        // Process and player behaviour movement code and return the result of all movements
        private Vector3 UpdateBehaviourMovement()
        {
            Vector3 movement = Vector3.zero;

            foreach (PlayerBehaviour behaviour in behaviours)
            {
                movement += behaviour.UpdateMovement();
            }

            return movement;
        }




        //
        //  Collision
        //

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            FixGroundState(hit.normal);

            foreach (PlayerBehaviour behaviour in behaviours)
            {
                behaviour.HandleControllerColliderHit(hit);
            }
        }

        private void FixGroundState(Vector3 hitNormal)
        {
            // Check to see if the ground slope is within acceptable limits
            if (Vector3.Angle(Vector3.up, hitNormal) <= characterController.slopeLimit && Time.time - timeCanBeGrounded >= 0)
            {
                groundedState = GroundedState.Grounded;
            }
            else
            {
                groundedState = GroundedState.Sliding;
            }

            lastGroundNormal = hitNormal;
        }



        //
        //  Methods
        //

        // Teleport this player to match a specific transform
        public void Teleport(Transform location)
        {
            Teleport(location.position, location.eulerAngles.y);
        }

        // Teleport this player to match a specific position and rotation
        public void Teleport(Vector3 position, float rotation)
        {
            Debug.Log($"Teleported using rotation: {rotation}");

            // Temporarily disable the underlying controller
            //  (we do this because for some reason the character controller doesn't like being manually teleported...
            //  and when we manually turn it off then on it fixes itself)
            characterController.enabled = false;

            // Set the position and rotation
            transform.position = position;
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, rotation, transform.eulerAngles.y);

            // Restart the char controller
            characterController.enabled = true;

            // Tell PlayerBehaviours about this teleport
            foreach (PlayerBehaviour behaviour in behaviours)
            {
                behaviour.HandleTeleport(position, rotation);
            }
        }

        public void ForceUngroundedTime(float amountOfTimeUngrounded)
        {
            timeCanBeGrounded = Time.time + amountOfTimeUngrounded;
        }


        //
        //  Velocity Method
        //

        /// <summary>
        /// Adds a force to the player's velocity, UNLESS movement is locked.
        /// </summary>
        /// <param name="force">How much force to add</param>
        public void AddForce(Vector3 force)
        {
            if (lockMovement.IsLocked())
            {
                return;
            }

            mainVelocity += force;
        }

        public void SetVelocity(Vector3 velocity)
        {
            mainVelocity = velocity;
        }

        public Vector3 GetVelocity()
        {
            return mainVelocity;
        }


        //
        //  Getters
        //

        // Get a specific PlayerBehaviour from this Player by type. If it doesn't exist, returns null
        public BehaviourType GetBehaviour<BehaviourType>() where BehaviourType : PlayerBehaviour
        {
            foreach (PlayerBehaviour behaviour in behaviours)
            {
                if (behaviour is BehaviourType)
                {
                    return behaviour as BehaviourType;
                }
            }

            return null;
        }

        public GroundedState GetIsGrounded()
        {
            return groundedState;
        }

        public Vector3 GetGroundNormal()
        {
            return lastGroundNormal;
        }

        public float GetStateTime(GroundedState state)
        {
            switch (state)
            {
                case GroundedState.Grounded:
                    return stateGroundedTime;

                case GroundedState.Sliding:
                    return stateSlideTime;

                case GroundedState.InAir:
                    return stateAirTime;


                default:
                    return -1;
            }
        }
    }
}

