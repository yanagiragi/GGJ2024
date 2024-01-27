using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.Player
{
    [CreateAssetMenu(fileName = "PlayerPicture", menuName = "Scriptable/PlayerPicture", order = 0)]
    public class PlayerPicture : ScriptableObject
    {
        [FormerlySerializedAs("sprites")] [SerializeField]
        public List<Sprite> normal;

        [SerializeField] public List<Sprite> slap;

        [SerializeField] public List<Sprite> sleep;
        [SerializeField] public List<Sprite> chair;
    }
}