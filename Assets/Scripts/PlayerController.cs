using DefaultNamespace.Player;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    internal enum ImageChildIndex
    {
        CHAIR = 0,
        PLAYER = 1
    }

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerPicture playerPicture;
        private Image chair;
        private Image image;

        private void Awake()
        {
            image = transform.GetChild((int)ImageChildIndex.PLAYER).GetComponent<Image>();
            chair = transform.GetChild((int)ImageChildIndex.CHAIR).GetComponent<Image>();
        }

        private void Start()
        {
            chair.sprite = playerPicture.chair[0];

            GameManager.Instance.PlayerController = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) Slap();
            if (Input.GetKeyDown(KeyCode.S)) Normal();
            if (Input.GetKeyDown(KeyCode.D)) Sleep();
        }

        public void Slap()
        {
            image.sprite = playerPicture.slap[Random.Range(0, playerPicture.slap.Count)];
        }

        public void Normal()
        {
            image.sprite = playerPicture.normal[Random.Range(0, playerPicture.normal.Count)];
        }

        public void Sleep()
        {
            image.sprite = playerPicture.sleep[Random.Range(0, playerPicture.sleep.Count)];
        }
    }
}