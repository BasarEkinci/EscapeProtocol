using UnityEngine;
using UnityEngine.Audio;

namespace Data.ValueObjects
{
    [System.Serializable]
    public struct SoundList
    {
        public string Name;
        [Range(0,1)] public float Volume;
        public AudioMixerGroup MixerGroup;
        public AudioClip[] Sounds;
    }
}
