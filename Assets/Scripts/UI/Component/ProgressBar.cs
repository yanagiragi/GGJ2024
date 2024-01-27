using MoreMountains.Tools;
using UnityEngine;

namespace UI.Component
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private MMProgressBar _mmProgressBar;
        
        public void SetValue(float newProgress)
        {
            _mmProgressBar.SetBar(newProgress, 0, 1);
        }
        
        public void Plus(float progress)
        {
            float newProgress = _mmProgressBar.BarTarget + progress;
            newProgress = Mathf.Clamp(newProgress, 0f, 1f);
            _mmProgressBar.UpdateBar01(newProgress);
        }

        public void Minus(float progress)
        {
            float newProgress = _mmProgressBar.BarTarget - progress;
            newProgress = Mathf.Clamp(newProgress, 0f, 1f);
            _mmProgressBar.UpdateBar01(newProgress);
        }
    }
}