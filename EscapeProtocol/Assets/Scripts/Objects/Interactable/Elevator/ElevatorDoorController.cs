using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Objects.Interactable.Elevator
{
    public class ElevatorDoorController : MonoBehaviour
    {
        [SerializeField] private List<Transform> upperDoorsOpenPos;
        [SerializeField] private List<Transform> lowerDoorsOpenPos;
        
        [SerializeField] private List<GameObject> upperDoors;
        [SerializeField] private List<GameObject> lowerDoors;
        
        [SerializeField] private List<Transform> upperDoorsClosedPos;
        [SerializeField] private List<Transform> lowerDoorsClosedPos;
        
        [SerializeField] private float duration;
        
        
        private ElevatorMovementController _elevatorMovementController;

        private Transform _upperDoor0FirstPos;
        private Transform _upperDoor1FirstPos;
        private Transform _lowerDoor0FirstPos;
        private Transform _lowerDoor1FirstPos;
        
        private bool _canOpen;
        private string _playerTag = "Player";
        
        private void Awake()
        {
            _elevatorMovementController = GetComponentInParent<ElevatorMovementController>();
        }

        private void Start()
        {
            _upperDoor0FirstPos = upperDoors[0].transform;
            _upperDoor1FirstPos = upperDoors[1].transform;
            _lowerDoor0FirstPos = lowerDoors[0].transform;
            _lowerDoor1FirstPos = lowerDoors[1].transform;
        }

        private void Update()
        {
            if (_elevatorMovementController.IsMoving)
            {
                _canOpen = false;
                CloseDoors();
            }
            else
            {
                _canOpen = true;
            }   
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag) && _canOpen)
            {
               OpenDoors(); 
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_playerTag) && _canOpen)
            {
                CloseDoors();
            }
        }

        private void OpenDoors()
        {
            upperDoors[0].transform.DOMove(upperDoorsOpenPos[0].transform.position, duration).OnComplete(() =>
            {
                upperDoors[1].transform.DOMove(upperDoorsOpenPos[1].transform.position, duration);
            });
            lowerDoors[0].transform.DOMove(lowerDoorsOpenPos[0].transform.position, duration).OnComplete(() =>
            {
                lowerDoors[1].transform.DOMove(lowerDoorsOpenPos[1].transform.position, duration);
            });
        }
        
        private void CloseDoors()
        {
            upperDoors[1].transform.DOMove(_upperDoor1FirstPos.position, duration).OnComplete(() =>
            {
                upperDoors[0].transform.DOMove(_upperDoor0FirstPos.position, duration);
            });
            lowerDoors[1].transform.DOMove(_lowerDoor1FirstPos.position, duration).OnComplete(() =>
            {
                lowerDoors[0].transform.DOMove(_lowerDoor0FirstPos.position, duration);
            });
        }
    }
    
}
