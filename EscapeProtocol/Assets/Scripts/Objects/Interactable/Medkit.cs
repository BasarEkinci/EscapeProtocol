using Controllers.Player;
using Data.UnityObjects;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Objects.Interactable
{
    public class Medkit : MonoBehaviour
    {
        [SerializeField] private int healthAmount = 15;
        [SerializeField] private SoundDataScriptable soundData;
        
        private Tween _tween;
        private void OnEnable()
        {
            _tween = transform.DORotate(Vector3.up * 90,0.5f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerHealthController player = other.GetComponent<PlayerHealthController>();
            if (player != null)
            {
                SoundManager.PLaySound(soundData,"Medkit");
                player.IncreaseHealth(healthAmount);
                Destroy(gameObject,0.1f);
            }
        }
        
        private void OnDisable()
        {
            _tween?.Kill();
        }
    }
}