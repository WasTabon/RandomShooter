using UnityEngine;
using System.Collections;

namespace RandomShooter.Scripts
{
    public class CameraMovement : MonoBehaviour
    {
        public DiceMovement diceMovement;
        public Transform targetPosition1;
        public Transform targetPosition2;
        public Transform movePos1;
        public Transform movePos2;
        public Transform player;
        public Transform centralPoint;
        public float moveSpeed = 2f;
        public float moveSpeedHeight = 2f;

        private Vector3 offset;
        private bool followRotation = false;

        private void Start()
        {
            StartCoroutine(MoveSequence());
        }

        private IEnumerator MoveSequence()
        {
            yield return MoveToPosition(targetPosition1.position);

            yield return MoveToPosition(targetPosition2.position);

            diceMovement.canMove = true;
            
            StartCoroutine(ChangeHeight());
        }

        private IEnumerator MoveToPosition(Vector3 target)
        {
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = target;
        }

        private void LateUpdate()
        {
            transform.LookAt(centralPoint);
        }
        
        private IEnumerator ChangeHeight()
        {
            while (true)
            {
                yield return StartCoroutine(SmoothHeightChange(movePos1.position.y));

                yield return StartCoroutine(SmoothHeightChange(movePos2.position.y));
            }
        }

        private IEnumerator SmoothHeightChange(float targetY)
        {
            float currentY = transform.position.y;
            while (Mathf.Abs(transform.position.y - targetY) > 0.01f)
            {
                float newY = Mathf.MoveTowards(transform.position.y, targetY, moveSpeedHeight * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                yield return null;
            }

            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        }
    }
}
