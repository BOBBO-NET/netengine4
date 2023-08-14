using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BobboNet.Audio
{
    public class ReverbManager : IInitializable
    {
        private FMOD.Studio.PARAMETER_DESCRIPTION reverbVolumeParam;
        private FMOD.Studio.PARAMETER_DESCRIPTION reverbTimeParam;
        private Dictionary<string, ReverbSettings> currentReverbs = new Dictionary<string, ReverbSettings>();

        //
        //  Initialize
        //

        public void Initialize()
        {
            FMODUnity.RuntimeManager.StudioSystem.getParameterDescriptionByName("Reverb_Volume", out reverbVolumeParam);
            FMODUnity.RuntimeManager.StudioSystem.getParameterDescriptionByName("Reverb_Time", out reverbTimeParam);
            UpdateReverb();
        }

        //
        //  Public Methods
        //

        public void AddReverb(string reverbKey, ReverbSettings settings)
        {
            currentReverbs.Add(reverbKey, settings);
            UpdateReverb();
        }

        public bool RemoveReverb(string reverbKey)
        {
            bool result = currentReverbs.Remove(reverbKey);
            UpdateReverb();
            return result;
        }

        public bool GetReverb(string reverbKey, out ReverbSettings settings)
        {
            return currentReverbs.TryGetValue(reverbKey, out settings);
        }

        public void ClearReverb()
        {
            currentReverbs.Clear();
            UpdateReverb();
        }

        //
        //  Private Methods
        //

        private void UpdateReverb()
        {
            ReverbSettings currentSettings = CalculateCurrentSettings();

            FMODUnity.RuntimeManager.StudioSystem.setParameterByID(reverbVolumeParam.id, currentSettings.volume);   // Set Volume
            FMODUnity.RuntimeManager.StudioSystem.setParameterByID(reverbTimeParam.id, currentSettings.reverbTime); // Set Reverb Time

            Debug.Log($"Updated Verb {currentSettings.volume} {currentSettings.reverbTime}");
        }

        private ReverbSettings CalculateCurrentSettings()
        {
            // If there's no reverbs... Return the default reverb settings
            if (currentReverbs.Count == 0) return ReverbSettings.Default;

            // OTHERWISE... There ARE reverbs so... get the max values for each setting from the elements in currentReverbValues.
            float currentVolume = Mathf.NegativeInfinity;
            float currentReverbTime = Mathf.NegativeInfinity;
            foreach (ReverbSettings verb in currentReverbs.Values)
            {
                if (verb.volume > currentVolume)
                {
                    currentVolume = verb.volume;
                }

                if (verb.reverbTime > currentReverbTime)
                {
                    currentReverbTime = verb.reverbTime;
                }
            }

            return new ReverbSettings(currentVolume, currentReverbTime);
        }
    }
}

