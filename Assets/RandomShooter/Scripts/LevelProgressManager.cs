using System.Collections;
using UnityEngine;

namespace RandomShooter.Scripts
{
    public class LevelProgressManager : MonoBehaviour
    {
        [SerializeField] private int _maxDicesCount;

        [SerializeField] private int _oneStarCount;
        [SerializeField] private int _twoStarsCount;
        [SerializeField] private int _threeStarsCount;

        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;

        [SerializeField] private CanvasGroup _star1;
        [SerializeField] private CanvasGroup _star2;
        [SerializeField] private CanvasGroup _star3;

        private Chip[] _chips;
        private int _dicesCount;
        private int _chipsCount;

        private DiceShoot _diceShoot;

        private void Start()
        {
            _chips = GameObject.FindObjectsOfType<Chip>();
            foreach (Chip chip in _chips)
            {
                chip.OnExplode += AddDiceCount;
            }

            _diceShoot = GameObject.FindObjectOfType<DiceShoot>();
            _diceShoot.OnShoot += AddDiceCount;
            
            _star1.alpha = 0f;
            _star2.alpha = 0f;
            _star3.alpha = 0f;
        }

        private void AddDiceCount()
        {
            _dicesCount++;
            if (_dicesCount >= _maxDicesCount)
            {
                _losePanel.SetActive(true);
            }
        }

        private void AddChipCount()
        {
            _chipsCount++;
            if (_chipsCount >= _chips.Length)
            {
                _winPanel.SetActive(true);
                StartCoroutine(ShowStars());
            }
        }

        private IEnumerator ShowStars()
        {
            yield return FadeIn(_star1);
            yield return new WaitForSeconds(0.1f);

            yield return FadeIn(_star2);
            yield return new WaitForSeconds(0.1f);

            yield return FadeIn(_star3);
        }

        private IEnumerator FadeIn(CanvasGroup cg)
        {
            float duration = 0.3f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                cg.alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            cg.alpha = 1f;
        }
    }
}
