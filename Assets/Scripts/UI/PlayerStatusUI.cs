using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UI_Component;
using UnityEngine;

namespace UI
{
    public class PlayerStatusUI : MonoBehaviour
    {

        [SerializeField] private ProgressBar _player1ProgressBar;
        // [SerializeField] private MMProgressBar _player1ProgressBar;

        [Button("設定進度條")]
        public void SetPlayerProgress(float progress)
        {
            _player1ProgressBar.SetValue(progress);
        }

        [Button("增加進度條")]
        public void AddPlayerProgress(float add)
        {
            _player1ProgressBar.Plus(add);
        }
        

    }
}