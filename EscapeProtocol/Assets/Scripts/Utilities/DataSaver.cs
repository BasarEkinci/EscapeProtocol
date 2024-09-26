using UnityEngine;

namespace Utilities
{
    public class DataSaver : MonoSingleton<DataSaver>
    {
        public void SaveDataFloat(string key, float volume)
        {
            PlayerPrefs.SetFloat(key, volume);
            PlayerPrefs.Save();
        }

        public void SavaDataInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }
        
        public float GetDataFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }
        
        public int GetDataInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
    }
}
