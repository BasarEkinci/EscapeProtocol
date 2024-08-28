using Controllers.Player;
using DG.Tweening;
using UnityEngine;

namespace Objects.Interactable
{
    public class Medkit : MonoBehaviour
    {
        [SerializeField] private int healthAmount = 15;
        [SerializeField] private Ease easeType;
        private void OnEnable()
        {
            transform.DORotate(Vector3.up * 90,0.5f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerHealthController player = other.GetComponent<PlayerHealthController>();
            if (player != null)
            {
                player.IncreaseHealth(healthAmount);
                Destroy(gameObject,0.1f);
            }
        }
    }
}