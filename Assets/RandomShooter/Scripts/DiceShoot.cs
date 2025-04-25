using System;
using UnityEngine;

namespace RandomShooter.Scripts
{
    public class DiceShoot : MonoBehaviour
    {
        public event Action OnShoot;
        
        public AudioSource _audioSource;
        public AudioClip _audioClipShot;
        public AudioClip _audioClipExplode;
        public Transform cameraTransform;
        public DiceAnimationController diceAnimationController;
        public UIController uiController;
        [SerializeField] private float shootForce = 10f;
        [SerializeField] private float upwardForce = 2f;

        private bool _isShot;
        
        private Transform _startParent;
        
        private Rigidbody _rigidbody;

        private Vector3 _oldPosition;
        private Vector3 _oldRotate;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _startParent = transform.parent;
        }

        public void Shoot()
        {
            if (_rigidbody != null && !_isShot)
            {
                _isShot = true;
                OnShoot?.Invoke();
                _audioSource.PlayOneShot(_audioClipShot);
                uiController.dicesCount++;
                _oldPosition = gameObject.transform.localPosition;
                _rigidbody.isKinematic = false;
                diceAnimationController.SetRotating(false);
                
                Vector3 forceDirection = cameraTransform.forward + cameraTransform.up * upwardForce;

                _rigidbody.AddForce(forceDirection * shootForce, ForceMode.Impulse);
                transform.parent = null;
            }
        }

        public void ResetState()
        {
            transform.parent = _startParent;
            _isShot = false;
            _audioSource.PlayOneShot(_audioClipExplode);
            gameObject.transform.localPosition = _oldPosition;
            _rigidbody.isKinematic = true;
            diceAnimationController.SetRotating(true);
        }
    }
}