using UnityEngine;
using Utilities;

namespace Audio
{
    public class AudioDataSaver : MonoSingleton<AudioDataSaver>
    {
        public void SaveVolume(string key, float volume)
        {
            PlayerPrefs.SetFloat(key, volume);
            PlayerPrefs.Save();
        }
        
        public float GetVolume(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }
    }
}
