using UnityEngine;

namespace Homework
{
    public class HwRiver : MonoBehaviour
    {
        private GameObject Keyboard;
        private GameObject leftArrow;
        private GameObject rightArrow;

        private void Awake()
        {
            leftArrow = transform.Find("hint/LeftArrow").gameObject;
            rightArrow = transform.Find("hint/RightArrow").gameObject;
            Keyboard = transform.Find("hint/Keyboard").gameObject;
        }

        private void Update()
        {
            UpdateArrowAnimation();
        }

        private void UpdateArrowAnimation()
        {
            var secFromStart = Time.timeSinceLevelLoad;
            var angle = secFromStart * 3.14f;
            var tosin = Mathf.Abs(Mathf.Sin(angle)) + 0.5f;
            leftArrow.transform.localScale = new Vector3(tosin, tosin, 1);
            rightArrow.transform.localScale = new Vector3(tosin, tosin, 1);
        }
    }
}