using UnityEngine;

namespace RandomShooter.Scripts
{
    public class DiceShoot : MonoBehaviour
    {
        public Transform cameraTransform;
        public DiceAnimationController diceAnimationController;
        public UIController uiController;
        [SerializeField] private float shootForce = 10f;
        [SerializeField] private float upwardForce = 2f;

        private Rigidbody _rigidbody;

        private Vector3 _oldPosition;
        private Vector3 _oldRotate;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Shoot()
        {
            if (_rigidbody != null)
            {
                uiController.dicesCount++;
                _oldPosition = gameObject.transform.localPosition;
                _rigidbody.isKinematic = false;
                diceAnimationController.SetRotating(false);
                
                Vector3 forceDirection = cameraTransform.forward + cameraTransform.up * upwardForce;

                _rigidbody.AddForce(forceDirection * shootForce, ForceMode.Impulse);
            }
        }

        public void ResetState()
        {
            gameObject.transform.localPosition = _oldPosition;
            _rigidbody.isKinematic = true;
            diceAnimationController.SetRotating(true);
        }
    }
}