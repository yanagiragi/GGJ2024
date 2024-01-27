using System;
using MoreMountains.Tools;
using UnityEngine;

namespace UI.Component
{
    public class ClockUI : MonoBehaviour
    {
        [SerializeField] private MMProgressBar _progressBar;

        public float timeProgress;
        
        private void Update()
        {
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            _progressBar.SetBar(timeProgress, 0, 1);
        }
    }
}