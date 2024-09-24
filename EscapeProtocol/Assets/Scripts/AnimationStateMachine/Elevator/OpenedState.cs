using Objects.Interactable.Elevator;
using UnityEngine;

namespace AnimationStateMachine.Elevator
{
    public class OpenedState : IState<ElevatorDoorController>
    {
        private static readonly int IsClosing = Animator.StringToHash("IsClosing");

        public void EnterState(ElevatorDoorController door)
        {
            door.PlayOpenSound();
        }

        public void UpdateState(ElevatorDoorController door)
        {
            bool isClosing = !door.IsPlayerNearby || door.IsMoving();
            door.Animator.SetBool(IsClosing,isClosing);
            if (door.IsMoving() || !door.IsPlayerNearby)
            {
                door.ChangeState(new ClosedState());
            }       
        }

        public void ExitState(ElevatorDoorController door)
        {
        }
    }
}