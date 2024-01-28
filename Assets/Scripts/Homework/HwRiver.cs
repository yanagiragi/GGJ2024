using DefaultNamespace.Homework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Homework
{
    public class HwRiver : MonoBehaviour
    {
        [SerializeField] private float moveDistance = 0.5f;
        private readonly float totalTime = 0.1f;
        private bool animationTime;
        private Image center;
        private ArrowColor color;
        private UnityAction doneAction;


        private Vector3 from;

        private GameObject hint;
        private GameObject Keyboard;
        private GameObject leftArrow;
        private GameObject rightArrow;
        private float timer;
        private Vector3 to;


        private void Awake()
        {
            center = transform.Find("Center").GetComponent<Image>();
            leftArrow = transform.Find("hint/LeftArrow").gameObject;
            rightArrow = transform.Find("hint/RightArrow").gameObject;
            Keyboard = transform.Find("hint/Keyboard").gameObject;
            leftArrow.GetComponent<Arrow>().SetDirection(Direction.Left);
            rightArrow.GetComponent<Arrow>().SetDirection(Direction.Right);
            hint = transform.Find("hint").gameObject;
            to = center.transform.position + new Vector3(0, 60, 0);

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
            var soCloseDistance = 30f;
            UpdateArrowAnimation();
            if (soCloseDistance > distance)
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    animationTime = true;
                    from = Keyboard.transform.position;
                }

            if (animationTime)
            {
                if (timer < totalTime)
                {
                    timer += Time.deltaTime;
                    Keyboard.transform.position = Vector3.Lerp(from, to, timer / totalTime);
                    return;
                }

                doneAction?.Invoke();
            }

            if (Input.GetKey(KeyCode.A) && leftEnalbe)
            {
                hint.transform.position += new Vector3(-moveDistance, 0, 0);
                return;
            }

            if (Input.GetKey(KeyCode.L) && rightEnalbe)
            {
                hint.transform.position += new Vector3(moveDistance, 0, 0);
                return;
            }

            //small move keyboard
            var dir = (hint.transform.position - transform.position).normalized;
            var moveScale = 2.0f;
            var biggerScale = 15f;
            if (distance < soCloseDistance * biggerScale)
                hint.transform.position +=
                    dir * (Mathf.Lerp(0, soCloseDistance * biggerScale - distance, 0.2f) * Time.deltaTime * moveScale);
        }

        public void Init()
        {
            var offset = Random.Range(0, 2) == 0 ? 1 : -1;
            var scale = 150f;
            hint.transform.position = transform.position - new Vector3(offset * scale, 0, 0);
            timer = 0f;
            animationTime = false;
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