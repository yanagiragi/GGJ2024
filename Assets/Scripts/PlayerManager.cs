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
        public DialogUI dialogUI;

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

        public PlayerSetting GetSleptPlayer()
        {
            if (playerOne.IsSleep)
            {
                return playerOne;
            }
            else if (playerTwo.IsSleep)
            {
                return playerTwo;
            }

            return null;
        }

        public PlayerSetting GetPlayer(int index)
        {
            if (index == 0)
            {
                return playerOne;
            }
            else if (index == 1)
            {
                return playerTwo;
            }

            return null;
        }

        public bool IsAnyPlayerSlept()
        {
            return playerTwo.IsSleep || playerOne.IsSleep;
        }

        public void Sleep(int index)
        {
            var target = index == 0 ? playerOne : playerTwo;
            target.Sleep();
        }
    }
}