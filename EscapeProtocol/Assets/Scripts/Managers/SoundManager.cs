using System;
using Data.UnityObjects;
using Data.ValueObjects;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager Instance;
        private AudioSource _audioSource;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _audioSource = GetComponent<AudioSource>();
//                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        public static void PLaySound(SoundDataScriptable soundData, string soundName, AudioSource source = null, float volume = 1)
        {
            SoundList soundList = FindSoundListByName(soundData, soundName);
            AudioClip[] clips = soundList.Sounds;
            AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

            if (source)
            {
                source.outputAudioMixerGroup = soundList.MixerGroup;
                source.clip = randomClip;
                source.Play();
            }
            else
            {
                Instance._audioSource.outputAudioMixerGroup = soundList.MixerGroup;
                Instance._audioSource.PlayOneShot(randomClip, volume * soundList.Volume);
            }
        }
        private static SoundList FindSoundListByName(SoundDataScriptable soundData, string audioListName)
        {
            foreach (var soundList in soundData.sounds)
            {
                if (soundList.Name == audioListName)
                {
                    return soundList;
                }
            }
            return new SoundList();
        }
    }
}
