using UnityEngine;

namespace RandomShooter.Scripts
{
    public class LevelProgressManager : MonoBehaviour
    {
        [SerializeField] private int _maxDicesCount;
        
        [SerializeField] private int _oneStarCount;
        [SerializeField] private int _twoStarsCount;
        [SerializeField] private int _threeStarsCount;

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
        }

        private void AddDiceCount()
        {
            _dicesCount++;
            if (_dicesCount >= _maxDicesCount)
            {
                
            }
        }

        private void AddChipCount()
        {
            _chipsCount++;
            if (_chipsCount >= _chips.Length)
            {
                
            }
        }
    }
}
