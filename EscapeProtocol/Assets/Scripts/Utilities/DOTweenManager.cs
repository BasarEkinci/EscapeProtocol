using DG.Tweening;

namespace Utilities
{
    public class DoTweenManager : MonoSingleton<DoTweenManager>
    {
        protected override void Awake()
        {
            base.Awake();
            DOTween.Init(true, true, LogBehaviour.ErrorsOnly);
        }
    }
}