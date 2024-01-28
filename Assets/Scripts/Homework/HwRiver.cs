using DefaultNamespace.Homework;
using UnityEngine;
using UnityEngine.Events;

namespace Homework
{
    public class HwRiver : MonoBehaviour
    {
        [SerializeField] private float moveDistance = 30f;
        private float currTime;
        private UnityAction doneAction;

        private GameObject hint;
        private GameObject Keyboard;
        private GameObject leftArrow;
        private GameObject rightArrow;
        private float timer;


        private void Awake()
        {
            leftArrow = transform.Find("hint/LeftArrow").gameObject;
            rightArrow = transform.Find("hint/RightArrow").gameObject;
            Keyboard = transform.Find("hint/Keyboard").gameObject;
            leftArrow.GetComponent<Arrow>().SetDirection(Direction.Left);
            rightArrow.GetComponent<Arrow>().SetDirection(Direction.Right);
            hint = transform.Find("hint").gameObject;
            timer = 10.0f;
            currTime = 0;
            Init();
        }

        private void Update()
        {
            var leftEnalbe = true;
            var rightEnalbe = true;

            if (GameManager.Instance.PlayerManager.IsAnyPlayerSlept())
            {
                rightEnalbe = !(GameManager.Instance.PlayerManager.GetSleptPlayer().chair.sprite.name != "Chair_Left");
                leftEnalbe = !(GameManager.Instance.PlayerManager.GetSleptPlayer().chair.sprite.name == "Chair_Left");
            }

            var distance = Vector2.Distance(Keyboard.transform.position, transform.position);
            var soCloseDistance = 50f;
            UpdateArrowAnimation();
            if (Input.GetKeyDown(KeyCode.A) && leftEnalbe)
                hint.transform.position += new Vector3(-moveDistance, 0, 0);
            if (Input.GetKeyDown(KeyCode.L) && rightEnalbe)
                hint.transform.position += new Vector3(moveDistance, 0, 0);
            if (soCloseDistance > distance)
            {
                currTime += Time.deltaTime;
                if (currTime > timer)
                {
                    Debug.Log("river done");
                    doneAction?.Invoke();
                    currTime = 0;
                }
            }
            else
            {
                currTime = 0;
            }

            //small move keyboard
            var dir = (hint.transform.position - transform.position).normalized;
            Debug.Log(distance < soCloseDistance * 1.2f);
            var moveScale = Time.deltaTime * 10.0f;
            var biggerScale = 4f;
            if (distance < soCloseDistance * biggerScale)
                hint.transform.position +=
                    dir * (Mathf.Lerp(0, soCloseDistance * biggerScale - distance, 0.2f) * Time.deltaTime);
        }

        public void Init()
        {
            hint.transform.position = transform.position - new Vector3(1, 0, 0);
            currTime = 0;
        }

        public void SetOnDoneAction(UnityAction action)
        {
            doneAction = action;
        }

        private void UpdateArrowAnimation()
        {
            var secFromStart = Time.timeSinceLevelLoad;
            var angle = secFromStart * 3.14f;
            var tosin = Mathf.Abs(Mathf.Sin(angle)) / 2f + 0.8f;
            leftArrow.transform.localScale = new Vector3(tosin, tosin, 1);
            rightArrow.transform.localScale = new Vector3(tosin, tosin, 1);
        }
    }
}