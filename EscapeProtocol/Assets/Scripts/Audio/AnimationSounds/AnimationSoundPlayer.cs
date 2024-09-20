using System.Collections.Generic;
using UnityEngine;

namespace Audio.AnimationSounds
{
    public class AnimationSoundPlayer : MonoBehaviour
    {
        [SerializeField] protected List<AudioClip> soundClips;
        protected AudioSource AudioSource;

        private void Awake()
        {
            AudioSource = GetComponentInParent<AudioSource>();
        }
    }
}