using System;
using DG.Tweening;
using UnityEngine;

namespace Objects.Interactable
{
    public class Elevator : MonoBehaviour
    {
        [SerializeField] private Transform floor1;
        [SerializeField] private Transform floor2;

        private int _currentFloor;
        
        private void Start()
        {
          _currentFloor = 1;   
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.parent = transform;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (_currentFloor == 1)
                    {
                        transform.DOMove(floor2.position, 1f).OnComplete(() =>
                        {
                            _currentFloor = 2; 
                        });
                    }
                    else if (_currentFloor == 2)
                    {
                        transform.DOMove(floor1.position, 1f).OnComplete(() =>
                        {
                            _currentFloor = 1; 
                        });
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.parent = null;
            }
        }
    }
}
