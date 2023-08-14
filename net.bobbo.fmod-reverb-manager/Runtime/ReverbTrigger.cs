using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BobboNet.Audio
{
    [RequireComponent(typeof(ZenjectBinding))]
    public class ReverbTrigger : MonoBehaviour
    {
        public ReverbSettings settings;
        public float radius = 16f;

        private ReverbManager reverbManager;
        private LayerMask mask;
        private bool containsPlayer = false;
        private string customKey = null;

        //
        //  Init
        //

        [Inject]
        public void Inject(ReverbManager reverbManager)
        {
            this.reverbManager = reverbManager;
        }

        private void Awake()
        {
            mask |= 1 << LayerMask.NameToLayer("Local Player");
            customKey = GetInstanceID().ToString(); // Generate key from this monobehaviour instance
        }

        private void OnDestroy()
        {
            reverbManager.RemoveReverb(customKey);
        }

        //
        //  Tick
        //

        private void Update()
        {
            bool isPlayerInTrigger = Physics.CheckSphere(transform.position, radius, mask, QueryTriggerInteraction.Ignore);

            // If the player wasn't in the trigger and now is, or vice versa...
            if (containsPlayer != isPlayerInTrigger)
            {
                SetPlayerInTrigger(isPlayerInTrigger);
            }
        }

        private void SetPlayerInTrigger(bool isPlayerInTrigger)
        {
            containsPlayer = isPlayerInTrigger;

            if (containsPlayer)
            {
                reverbManager.AddReverb(customKey, settings);
            }
            else
            {
                reverbManager.RemoveReverb(customKey);
            }
        }

        //
        //  Gizmos
        //

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
