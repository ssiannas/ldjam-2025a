using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace ldjam_hellevator
{
    public class ButtonChanger : MonoBehaviour
    {

        [Header("TextMeshPro Settings")]
        public TMP_Text buttonText;
        public Color normalColor = Color.white;
        public Color hoverColor = Color.red;

        private void Awake()
        {
            if (buttonText == null)
                buttonText = GetComponentInChildren<TMP_Text>();
        }

        public void OnPointerEnter()
        {
            ChangeColor(hoverColor);
        }

        public void OnPointerExit()
        {
            ChangeColor(normalColor);
        }

        void ChangeColor(Color targetColor)
        {
            buttonText.color = targetColor;
        }
    }
}
