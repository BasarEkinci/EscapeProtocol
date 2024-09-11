using System.Collections.Generic;
using System.Linq;
using Data.UnityObjects;
using Inputs;
using Managers;
using TMPro;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;


namespace Objects
{
    public class GrenadeLauncher : MonoBehaviour
    {
        [Header("UI Settings")]
        [SerializeField] private TMP_Text grenadeAmountText;
        [SerializeField] private int maxGrenadeAmount;
        
        [Header("Grenade Settings")]
        [SerializeField] private GameObject grenadePrefab;
        [SerializeField] private List<Transform> grenadeSpawnPoints;
        [SerializeField] private float throwForce;
        
        [Header("Effects")]
        [SerializeField] private List<ParticleSystem> launchEffects;
        
        [Header("Data Settings")]
        [SerializeField] private SoundDataScriptable soundData;
        
        private InputHandler _inputHandler;
        private AudioSource _audioSource;
        private Transform _grenadeSpawnPoint;
        private Vector3 _targetPosition;
        private int _currentGrenadeCount;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _inputHandler = new InputHandler();
        }

        private void Start()
        {
            _currentGrenadeCount = maxGrenadeAmount;
            grenadeAmountText.text = _currentGrenadeCount.ToString();
        }

        private void Update()
        {
            if (_inputHandler.GetThrowGrenadeInput())
            {
                if (_currentGrenadeCount != 0)
                {
                    SoundManager.PLaySound(soundData,"ThrowGrenade",_audioSource);
                    PlayParticles();
                    ThrowGrenade();   
                }
                else
                {
                    SoundManager.PLaySound(soundData,"Empty",_audioSource);
                }
            }
        }
        
        /// <summary>
        /// performs the necessary calculations to launch the bomb parabolically
        /// </summary>
        private void ThrowGrenade()
        {
            if (_currentGrenadeCount == 0) return;
            _grenadeSpawnPoint = grenadeSpawnPoints[Random.Range(0,1)];
            _targetPosition = MouseToWorldPosition.Instance.GetCursorWorldPoint();
            Vector3 throwDirection = CalculateGrenadeThrowDirection(_grenadeSpawnPoint.position, _targetPosition);
            GameObject grenade = Instantiate(grenadePrefab, _grenadeSpawnPoint.position, Quaternion.identity);
            grenade.GetComponent<Rigidbody>().AddForce(throwDirection, ForceMode.Impulse);
            _currentGrenadeCount--;
            grenadeAmountText.text = _currentGrenadeCount.ToString();
        }
        
        private Vector3 CalculateGrenadeThrowDirection(Vector3 grenadeSpawnPoint, Vector3 targetPosition)
        {
            float xDirection = targetPosition.x - grenadeSpawnPoint.x;
            Vector3 throwDirection = new Vector3(xDirection, throwForce, 0);
            return throwDirection;
        }
        
        private void PlayParticles()
        {
            foreach (var launchEffect in launchEffects.Where(launchEffect => !launchEffect.isPlaying))
            {
                launchEffect.Play();
            }
        }
        
        public void IncreaseGrenadeAmount(int count)
        {
            SoundManager.PLaySound(soundData,"CollectGrenade");
            _currentGrenadeCount += count;
            grenadeAmountText.text = _currentGrenadeCount.ToString();
        }
    }
}
