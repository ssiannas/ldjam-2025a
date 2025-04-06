using System;
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
        [SerializeField] private Image dashImageEmpty;
        [SerializeField] private Image dashImageFull;
        [SerializeField] private RectTransform mask;
        
        [Header("Sprites")]
        [SerializeField] private Sprite fullHeart;
        [SerializeField] private Sprite emptyHeart;
        
        private int health = 3;
        [Header("Channel")]
        [SerializeField] private UiManagerChannel uiManagerChannel;
        
        private bool isDashCooldown = false;
        private float dashCooldownTimer = 0f;
        private float dashCooldownDuration = 3f;

        private void Update()
        {
            if (isDashCooldown)
            {
                dashCooldownTimer -= Time.deltaTime;
                float fillAmount = Mathf.Clamp01(1 - (dashCooldownTimer / dashCooldownDuration));
                mask.sizeDelta = new Vector2(mask.sizeDelta.x, fillAmount * dashImageEmpty.rectTransform.rect.height);
                if (dashCooldownTimer <= 0)
                {
                    isDashCooldown = false;
                }
            }

        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            if (uiManagerChannel == null)
            {
                throw new System.Exception("No UI Manager Channel Assigned");
            }
            uiManagerChannel.OnUpdateHearts += UpdateHearts;
            uiManagerChannel.OnDashCooldown += DoDashCooldown;
        }


        void UpdateHearts(int health)
        {
            heart1.sprite = (health >= 1) ? fullHeart : emptyHeart;
            heart2.sprite = (health >= 2) ? fullHeart : emptyHeart;
            heart3.sprite = (health == 3) ? fullHeart : emptyHeart;
        }
        
        void DoDashCooldown(float cooldown)
        {
            mask.sizeDelta = new Vector2(mask.sizeDelta.x, 0);
            dashCooldownTimer = cooldown;
            dashCooldownDuration = cooldown;
            isDashCooldown = true;
        }
    }
}

