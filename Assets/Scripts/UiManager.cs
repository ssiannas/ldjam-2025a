using UnityEngine;
using UnityEngine.UI;

namespace ldjam_hellevator
{
    public class UiManager : MonoBehaviour
    {
        [Header("UI Objects")]
        [SerializeField] private Image heart1;
        [SerializeField] private Image heart2;
        [SerializeField] private Image heart3;
        [Header("Sprites")]
        [SerializeField] private Sprite fullHeart;
        [SerializeField] private Sprite emptyHeart;

        private int health = 3;
        [Header("Channel")]
        [SerializeField] private UiManagerChannel uiManagerChannel;

        private void Awake()
        {

            DontDestroyOnLoad(this);
            if (uiManagerChannel == null)
            {
                throw new System.Exception("No UI Manager Channel Assigned");
            }
            uiManagerChannel.OnUpdateHearts += UpdateHearts;
        }


        void UpdateHearts(int health)
        {
            heart1.sprite = (health >= 1) ? fullHeart : emptyHeart;
            heart2.sprite = (health >= 2) ? fullHeart : emptyHeart;
            heart3.sprite = (health == 3) ? fullHeart : emptyHeart;
        }
    }
}

