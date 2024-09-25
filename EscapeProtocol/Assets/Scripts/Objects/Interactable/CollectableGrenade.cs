using DG.Tweening;
using UnityEngine;

namespace Objects.Interactable
{
    public class CollectableGrenade : MonoBehaviour
    {
        private Tween _tween;
        private void OnEnable()
        {
            _tween = transform.DORotate(Vector3.up * 90,0.5f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }

        private void OnTriggerEnter(Collider other)
        {
            GrenadeLauncher player = other.GetComponent<GrenadeLauncher>();
            if (player != null)
            {
                player.IncreaseGrenadeAmount(1);
                Destroy(gameObject,0.1f);
            }
        }

        private void OnDisable()
        {
            _tween?.Kill();
        }
    }
}
