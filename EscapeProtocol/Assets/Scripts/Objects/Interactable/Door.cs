using System;
using System.Collections.Generic;
using Data.UnityObjects;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Objects.Interactable
{
    public class Door : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] private Ease easeType;
        [SerializeField] private Transform upperDoorEndPoint;
        [SerializeField] private Transform lowerDoorEndPoint;
        [SerializeField] private float duration;

        [SerializeField] private List<GameObject> redLights;
        [SerializeField] private List<GameObject> greenLights;
        
        [Header("References")]
        [SerializeField] private GameObject currentGuard;
        [SerializeField] private GameObject upperDoor;
        [SerializeField] private GameObject lowerDoor;
        [SerializeField] private SoundDataScriptable soundData;

        [SerializeField] private bool isRequiredGuard;
        private AudioSource _audioSource;
        
        private bool _canOpen;
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

        private void Update()
        {
            SetDoorLightColor();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isRequiredGuard)
            {
                if (other.gameObject.CompareTag(_requiredTag) && !currentGuard.gameObject.activeSelf)
                {
                    upperDoor.transform.DOMove(upperDoorEndPoint.position, duration).SetEase(easeType);
                    lowerDoor.transform.DOMove(lowerDoorEndPoint.position, duration).SetEase(easeType);
                    SoundManager.PLaySound(soundData,"Door",_audioSource,1);
                }    
            }
            else
            {
                if (other.gameObject.CompareTag(_requiredTag))
                {
                    upperDoor.transform.DOMove(upperDoorEndPoint.position, duration).SetEase(easeType);
                    lowerDoor.transform.DOMove(lowerDoorEndPoint.position, duration).SetEase(easeType);
                    SoundManager.PLaySound(soundData,"Door",_audioSource,1);
                }  
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (isRequiredGuard)
            {
                if (other.gameObject.CompareTag(_requiredTag) && !currentGuard.activeSelf)
                {
                    upperDoor.transform.DOMove(_upperDoorFirstPosition, duration).SetEase(easeType);
                    lowerDoor.transform.DOMove(_lowerDoorFirstPosition, duration).SetEase(easeType);
                    SoundManager.PLaySound(soundData,"Door",_audioSource);
                }
            }
            else
            {
                if (other.gameObject.CompareTag(_requiredTag))
                {
                    upperDoor.transform.DOMove(_upperDoorFirstPosition, duration).SetEase(easeType);
                    lowerDoor.transform.DOMove(_lowerDoorFirstPosition, duration).SetEase(easeType);
                    SoundManager.PLaySound(soundData,"Door",_audioSource);
                }
            }
        }
        
        private void SetDoorLightColor()
        {
            if (isRequiredGuard)
            {
                if (!currentGuard.gameObject.activeSelf)
                {
                    redLights.ForEach(light => light.SetActive(false));
                    greenLights.ForEach(light => light.SetActive(true));   
                }
                else
                {
                    greenLights.ForEach(light => light.SetActive(false));
                    redLights.ForEach(light => light.SetActive(true));
                }
            }
            else
            {
                redLights.ForEach(light => light.SetActive(false));
                greenLights.ForEach(light => light.SetActive(true));
            }
        }
    }
}
