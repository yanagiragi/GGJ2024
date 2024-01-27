using System;
using DefaultNamespace.Player;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    internal enum ImageChildIndex
    {
        CHAIR = 0,
        PLAYER = 1
    }

    [Serializable]
    public class PlayerSetting
    {
        public Transform transform;
        public PlayerPicture playerPicture;
        public Image chair;
        public Image image;

        public bool IsSleep = false;

        public void init()
        {
            image = transform.GetChild((int)ImageChildIndex.PLAYER).GetComponent<Image>();
            chair = transform.GetChild((int)ImageChildIndex.CHAIR).GetComponent<Image>();

            chair.sprite = playerPicture.chair[0];
            IsSleep = false;
        }

        public void Slap()
        {
            image.sprite = playerPicture.slap[Random.Range(0, playerPicture.slap.Count)];
        }

        public void Normal()
        {
            image.sprite = playerPicture.normal[Random.Range(0, playerPicture.normal.Count)];
            IsSleep = false;
        }

        public void Sleep()
        {
            image.sprite = playerPicture.sleep[Random.Range(0, playerPicture.sleep.Count)];
            IsSleep = true;
        }
    }

    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerSetting playerOne;
        [SerializeField] private PlayerSetting playerTwo;

        private void Start()
        {
            GameManager.Instance.PlayerManager = this;

            playerOne.init();
            playerTwo.init();
        }

        public PlayerSetting GetSleepedPlayer()
        {
            if (playerOne.IsSleep)
            {
                return playerOne;
            }
            else
            {
                return playerTwo;
            }
        }

        public void Sleep(int index)
        {
            var target = index == 0 ? playerOne : playerTwo;
            target.Sleep();
        }
    }
}