using Utilities;
using Cinemachine;
using UnityEngine;

namespace Objects
{
    public class CinemachineShake : MonoSingleton<CinemachineShake>
    {
        private CinemachineVirtualCamera _virtualCamera;
        private float _shakeTimer;
        protected override void Awake()
        {
            base.Awake();
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
        
        public void ShakeCamera(float intensity, float time)
        {
            CinemachineBasicMultiChannelPerlin perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            perlin.m_AmplitudeGain = intensity;
            _shakeTimer = time;
        }
        
        private void Update()
        {
            if (_shakeTimer > 0)
            {
                _shakeTimer -= Time.deltaTime;
                if (_shakeTimer <= 0)
                {
                    CinemachineBasicMultiChannelPerlin perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    perlin.m_AmplitudeGain = 0;
                }
            }
        }
    }
}
