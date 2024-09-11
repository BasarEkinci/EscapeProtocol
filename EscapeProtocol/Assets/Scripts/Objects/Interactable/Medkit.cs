using System.Collections;
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
            if (transform != null)
            {
                transform.DORotate(Vector3.up * 90,0.5f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            }
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
        
        IEnumerator NullCheck()
        {
            yield return new WaitForSeconds(0.1f);
            if (transform != null)
            {
                transform.DORotate(Vector3.up * 90,0.5f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            }
        }
    }
}