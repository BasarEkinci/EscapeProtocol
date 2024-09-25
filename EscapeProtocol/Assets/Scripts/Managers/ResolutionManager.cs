using System;
using System.Collections.Generic;
using Audio;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ResolutionManager : MonoBehaviour
    {
        
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        
        private Resolution[] _resolutions;

        private void Start()
        {
            _resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            int resolutionIndex = 0;
            for (var i = 0; i < _resolutions.Length; i++)
            {
                string option = _resolutions[i].width + "x" + _resolutions[i].height;
                if (!options.Contains(option))
                {
                    options.Add(option);
                }
                if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
                {
                    resolutionIndex = i;
                }
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = resolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        private void OnEnable()
        {
            /*int resolutionIndex = DataSaver.Instance.GetDataInt("Resolution");
            Resolution resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);*/
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            DataSaver.Instance.SavaDataInt("Resolution", resolutionIndex);
        }
        public void SetWindowType(bool isFulScreen)
        {
            Screen.fullScreen = isFulScreen;
        }
    }
}