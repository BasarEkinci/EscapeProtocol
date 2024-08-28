using System.Collections.Generic;
using System.Linq;
using Data.UnityObjects;
using Inputs;
using Managers;
using UnityEngine;
using Utilities;


namespace Objects
{
    public class GrenadeLauncher : MonoBehaviour
    {
        [SerializeField] private GameObject grenadePrefab;
        [SerializeField] private float throwForce;
        [SerializeField] private List<Transform> grenadeSpawnPoints;
        [SerializeField] private List<ParticleSystem> launchEffects;
        [SerializeField] private SoundDataScriptable soundData;
        private InputHandler _inputHandler;
        private Transform _grenadeSpawnPoint;
        private Vector3 _targetPosition;
        private void Awake()
        {
            _inputHandler = new InputHandler();
        }

        private void Update()
        {
            if (_inputHandler.GetThrowGrenadeInput())
            {
                SoundManager.PLaySound(soundData,"ThrowGrenade",null,1f);
                PlayParticles();
                ThrowGrenade();
            }
        }
        private void ThrowGrenade()
        {
            _grenadeSpawnPoint = grenadeSpawnPoints[Random.Range(0,1)];
            _targetPosition = MouseToWorldPosition.Instance.GetCursorWorldPoint();
            Vector3 throwAngle = CalculateGrenadeThrowDirection(_grenadeSpawnPoint.position, _targetPosition);
            GameObject grenade = Instantiate(grenadePrefab, _grenadeSpawnPoint.position, Quaternion.identity);
            grenade.GetComponent<Rigidbody>().AddForce(throwAngle * throwForce, ForceMode.Impulse);
        }
        
        private Vector3 CalculateGrenadeThrowDirection(Vector3 grenadeSpawnPoint, Vector3 targetPosition)
        {
            float horizontalDistance = targetPosition.x - grenadeSpawnPoint.x;
            float verticalDistance = targetPosition.y - grenadeSpawnPoint.y;
            Vector3 throwAngle = new Vector3(horizontalDistance, verticalDistance, 0);
            return throwAngle;
        }
        
        private void PlayParticles()
        {
            foreach (var launchEffect in launchEffects.Where(launchEffect => !launchEffect.isPlaying))
            {
                launchEffect.Play();
            }
        }
        
    }
}
