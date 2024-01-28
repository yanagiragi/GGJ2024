using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Homework
{
    public enum Direction
    {
        Left = 1,
        Down = 2,
        Right = 3,
        Up = 4
    }

    public class Arrow : MonoBehaviour
    {
        [SerializeField] private ArrowColor color;
        private Image image;


        private void Awake()
        {
            image = GetComponent<Image>();
        }

        [Button("Set dir")]
        public void SetDirection(Direction direction)
        {
            if (image == null)
                image = GetComponent<Image>();
            switch (direction)
            {
                case Direction.Up:
                    image.color = color.upColor;
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                case Direction.Down:
                    image.color = color.downColor;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Direction.Left:
                    Debug.Log(image);
                    Debug.Log(color);
                    image.color = color.leftColor;
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                    break;
                case Direction.Right:
                    image.color = color.rightColor;
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
            }
        }
    }
}