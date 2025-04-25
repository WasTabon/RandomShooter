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

        private bool _wasWin;

        private bool _isStar1;
        private bool _isStar2;
        private bool _isStar3;

        private DiceShoot _diceShoot;

        private void Start()
        {
            _chips = GameObject.FindObjectsOfType<Chip>();
            foreach (Chip chip in _chips)
            {
                chip.OnExplode += AddChipCount;
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

            Debug.Log(_dicesCount <= _threeStarsCount);
            Debug.Log(_dicesCount > _oneStarCount);
            Debug.Log(_dicesCount >= _twoStarsCount && _dicesCount <= _threeStarsCount);
            
            if (_chipsCount >= _chips.Length && !_wasWin)
            {
                _wasWin = true;
                if (_dicesCount <= _threeStarsCount)
                {
                    StarsManager.Instance.AddStarsCount(3);
                    _isStar3 = true;
                    _isStar2 = true;
                    _isStar1 = true;
                }
                else if (_dicesCount > _oneStarCount)
                {
                    StarsManager.Instance.AddStarsCount(1);
                    _isStar1 = true;
                }
                else if (_dicesCount >= _twoStarsCount && _dicesCount <= _threeStarsCount)
                {
                    StarsManager.Instance.AddStarsCount(2);
                    _isStar2 = true;
                    _isStar1 = true;
                }

                Debug.Log("PanelActive");

                _winPanel.SetActive(true);
                StartCoroutine(ShowStars());
            }
        }

        private IEnumerator ShowStars()
        {
            if (_star1)
            {
                yield return FadeIn(_star1);
                yield return new WaitForSeconds(0.1f);
            }

            if (_star2)
            {
                yield return FadeIn(_star2);
                yield return new WaitForSeconds(0.1f);
            }

            if (_star3)
            {
                yield return FadeIn(_star3);
            }
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
