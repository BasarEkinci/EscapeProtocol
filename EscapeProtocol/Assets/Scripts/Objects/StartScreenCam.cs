using DG.Tweening;
using UnityEngine;

namespace Objects
{
    public class StartScreenCam : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.DORotate(Vector3.up * 90,20f).SetLoops(-1,LoopType.Incremental).SetEase(Ease.Linear);
        }
    }
}
