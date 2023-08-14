
namespace BobboNet.Audio
{
    [System.Serializable]
    public class ReverbSettings
    {
        public static ReverbSettings Default { get => new ReverbSettings(); }
        public const float defaultVolume = 0.5f;
        public const float defaultReverbTime = 1.0f;

        public float volume = defaultVolume;
        public float reverbTime = defaultReverbTime;

        public ReverbSettings(float volume = defaultVolume, float reverbTime = defaultReverbTime)
        {
            this.volume = volume;
            this.reverbTime = reverbTime;
        }
    }
}