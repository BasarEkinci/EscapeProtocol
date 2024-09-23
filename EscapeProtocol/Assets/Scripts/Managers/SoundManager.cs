using Data.UnityObjects;
using Data.ValueObjects;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;
        private AudioSource _audioSource;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                _audioSource = GetComponent<AudioSource>(); 
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Plays a sound from the soundData scriptable object
        /// </summary>
        /// <param name="soundData"> Scriptable object for a sound</param>
        /// <param name="soundName"> The given name on sound in scriptable object that include</param>
        /// <param name="source"> Audio source in the gameobject. It can be null</param>
        /// <param name="volume"> Volume parameter for sound (For gameobjects that do not have audiosource, default value is "1")</param>
        public static void PLaySound(SoundDataScriptable soundData, string soundName, AudioSource source = null, float volume = 1)
        {
            SoundList soundList = FindSoundListByName(soundData, soundName);
            AudioClip[] clips = soundList.Sounds;
            AudioClip randomClip = clips[Random.Range(0, clips.Length)];

            if (source)
            {
                source.outputAudioMixerGroup = soundList.MixerGroup;
                source.clip = randomClip;
                source.Play();
            }
            else
            {
                _instance._audioSource.outputAudioMixerGroup = soundList.MixerGroup;
                _instance._audioSource.PlayOneShot(randomClip, volume * soundList.Volume);
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
