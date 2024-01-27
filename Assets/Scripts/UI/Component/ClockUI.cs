using System;
using MoreMountains.Tools;
using UnityEngine;

namespace UI.Component
{
    public class ClockUI : MonoBehaviour
    {
        [SerializeField] private MMProgressBar _progressBar;

        [SerializeField] private Transform pointer;

        public float timeProgress;
        
        private void Update()
        {
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            timeProgress = Timer.Instance.GetTimeRadio();
            _progressBar.SetBar(timeProgress, 0, 1);
            
            pointer.eulerAngles = new Vector3(0, 0, timeProgress * 360f);
        }
    }
}