using UnityEngine;

namespace RandomShooter.Scripts
{
    public class DiceAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private void Start()
        {
            SetRotating(true);
        }

        public void SetRotating(bool state)
        {
            _animator.SetBool("IsRotating", state);
        }
    }
}
