using TMPro;
using UnityEngine;
using System.Collections;

namespace RandomShooter.Scripts
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private TextMeshProUGUI _floatingText;
        public int lastSide;
        public int dicesCount;

        private Vector3 _initialPosition;
        private Color _initialColor;

        private void Start()
        {
            _initialPosition = _floatingText.rectTransform.anchoredPosition;
            _initialColor = _floatingText.color;
        }

        private void Update()
        {
            _text.text = $"Dices used: {dicesCount}";
        }

        public void AnimateText()
        {
            StartCoroutine(FadeAndMoveText());
        }

        private IEnumerator FadeAndMoveText()
        {
            _floatingText.text = $"{lastSide}";
            
            float duration = 1.5f;
            float elapsed = 0f;

            Vector3 targetPosition = _initialPosition + new Vector3(0f, 100f, 0f);
            Color startColor = new Color(_initialColor.r, _initialColor.g, _initialColor.b, 0f);
            Color endColor = new Color(_initialColor.r, _initialColor.g, _initialColor.b, 1f);

            _floatingText.rectTransform.anchoredPosition = _initialPosition;
            _floatingText.color = startColor;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                
                _floatingText.rectTransform.anchoredPosition = Vector3.Lerp(_initialPosition, targetPosition, t);
                _floatingText.color = Color.Lerp(startColor, endColor, t);

                yield return null;
            }
            
            _floatingText.rectTransform.anchoredPosition = _initialPosition;
            _floatingText.color = startColor;
        }
    }
}