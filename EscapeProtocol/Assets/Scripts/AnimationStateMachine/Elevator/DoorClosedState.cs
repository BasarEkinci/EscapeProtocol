using Objects.Interactable.Elevator;
using UnityEngine;

namespace AnimationStateMachine.Elevator
{
    public class DoorClosedState : IState<ElevatorDoorController>
    {
        private static readonly int CanOpen = Animator.StringToHash("CanOpen");

        public void EnterState(ElevatorDoorController door)
        {
            door.PlayCloseSound();
        }

        public void UpdateState(ElevatorDoorController door)
        {
            bool canOpen = door.IsPlayerNearby && !door.IsMoving();
            door.Animator.SetBool(CanOpen, canOpen);
            if (!door.IsMoving() && door.IsPlayerNearby)
            {
                door.ChangeState(new DoorOpenedState());
            }
        }

        public void ExitState(ElevatorDoorController door)
        {
        }
    }
}