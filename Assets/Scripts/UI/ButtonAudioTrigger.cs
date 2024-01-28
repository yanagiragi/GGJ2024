using System;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonAudioTrigger : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            AudioManager.Instance.PlaySE(SE.Button);
        }
    }
}