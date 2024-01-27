using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Homework
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Arrow : MonoBehaviour
    {
        [SerializeField] private ArrowColor color;

        [Button("Set dir")]
        public void SetDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    GetComponent<Image>().color = color.upColor;
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                case Direction.Down:
                    GetComponent<Image>().color = color.downColor;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Direction.Left:
                    GetComponent<Image>().color = color.leftColor;
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                    break;
                case Direction.Right:
                    GetComponent<Image>().color = color.rightColor;
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
            }
        }
    }
}