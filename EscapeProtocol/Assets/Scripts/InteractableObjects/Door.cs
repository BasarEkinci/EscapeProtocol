using System;
using Data.UnityObjects;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace InteractableObjects
{
    public class Door : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] private Ease easeType;
        [SerializeField] private Transform upperDoorEndPoint;
        [SerializeField] private Transform lowerDoorEndPoint;
        [SerializeField] private float duration;
        
        [Header("References")]
        [SerializeField] private GameObject currentGuard;
        [SerializeField] private GameObject upperDoor;
        [SerializeField] private GameObject lowerDoor;
        [SerializeField] private SoundDataScriptable soundData;
        
        private AudioSource _audioSource;
        
        
        private string _requiredTag = "Player";

        private Vector3 _upperDoorFirstPosition;
        private Vector3 _lowerDoorFirstPosition;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _upperDoorFirstPosition = upperDoor.transform.position;
            _lowerDoorFirstPosition = lowerDoor.transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(_requiredTag) && !currentGuard.gameObject.activeSelf)
            {
                SoundManager.PLaySound(soundData,"Door",_audioSource,1);
                upperDoor.transform.DOMove(upperDoorEndPoint.position, duration).SetEase(easeType);
                lowerDoor.transform.DOMove(lowerDoorEndPoint.position, duration).SetEase(easeType);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(_requiredTag) && !currentGuard.activeSelf)
            {
                upperDoor.transform.DOMove(_upperDoorFirstPosition, duration).SetEase(easeType);
                lowerDoor.transform.DOMove(_lowerDoorFirstPosition, duration).SetEase(easeType);
            }
        }
    }
}
