using System.Collections;
using UnityEngine;

namespace RandomShooter.Scripts
{
    public class Chip : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration = 2f;
        [SerializeField] private Renderer _customRenderer;
        [SerializeField] private Material _customMaterial;
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;

        private Material _instanceMaterial;
        private bool _isExploding;

        private void Awake()
        {
        }

        [ContextMenu("Explode")]
        public void Explode()
        {
            if (_isExploding) return;
            _isExploding = true;

            _collider.enabled = false;
            _rigidbody.isKinematic = true;
            _instanceMaterial = new Material(_customMaterial);
            _customRenderer.material = _instanceMaterial;
            
            StartCoroutine(FadeOutAndDisable());
        }

        private IEnumerator FadeOutAndDisable()
        {
            // Задаём начальный цвет — жёлтый с альфой 1
            Color startColor = Color.yellow;
            startColor.a = 1f;

            // Конечный цвет — жёлтый с альфой 0
            Color endColor = startColor;
            endColor.a = 0f;

            float elapsed = 0f;

            // Устанавливаем режим прозрачности (на случай если забыли в инспекторе)
            if (_instanceMaterial.HasProperty("_Mode"))
            {
                _instanceMaterial.SetFloat("_Mode", 3); // Transparent
                _instanceMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                _instanceMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                _instanceMaterial.SetInt("_ZWrite", 0);
                _instanceMaterial.DisableKeyword("_ALPHATEST_ON");
                _instanceMaterial.EnableKeyword("_ALPHABLEND_ON");
                _instanceMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                _instanceMaterial.renderQueue = 3000;
            }

            while (elapsed < _fadeDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / _fadeDuration;
                _instanceMaterial.color = Color.Lerp(startColor, endColor, t);
                yield return null;
            }

            // Прозрачный цвет и деактивация
            _instanceMaterial.color = endColor;
            gameObject.SetActive(false);
        }
    }
}
