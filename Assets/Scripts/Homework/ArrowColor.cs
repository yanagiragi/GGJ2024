using UnityEngine;

namespace DefaultNamespace.Homework
{
    [CreateAssetMenu(fileName = "ArrowColor", menuName = "ScriptableObject/ArrowColor", order = 0)]
    public class ArrowColor : ScriptableObject
    {
        public Color upColor;
        public Color downColor;
        public Color leftColor;
        public Color rightColor;
    }
}