using UnityEngine;
using System.Collections;

namespace RandomShooter.Scripts
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Initial Move Sequence")]
        public DiceMovement diceMovement;
        public Transform targetPosition1;
        public Transform targetPosition2;
        public Transform movePos1;
        public Transform movePos2;
        public float moveSpeed = 2f;
        public float moveSpeedHeight = 2f;

        [Header("Follow & Joystick Control")]
        public Transform centralPoint;
        public Joystick joystick;
        public float maxOffsetAngle = 20f;
        public float returnSpeed = 100f;

        private float currentYawOffset = 0f;
        private float currentPitchOffset = 0f;

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
                transform.position = Vector3.MoveTowards(
                    transform.position, target,
                    moveSpeed * Time.deltaTime
                );
                yield return null;
            }
            transform.position = target;
        }

        private IEnumerator ChangeHeight()
        {
            while (true)
            {
                yield return SmoothHeightChange(movePos1.position.y);
                yield return SmoothHeightChange(movePos2.position.y);
            }
        }

        private IEnumerator SmoothHeightChange(float targetY)
        {
            while (Mathf.Abs(transform.position.y - targetY) > 0.01f)
            {
                float newY = Mathf.MoveTowards(
                    transform.position.y, targetY,
                    moveSpeedHeight * Time.deltaTime
                );
                transform.position = new Vector3(
                    transform.position.x, newY, transform.position.z
                );
                yield return null;
            }
            transform.position = new Vector3(
                transform.position.x, targetY, transform.position.z
            );
        }

        private void LateUpdate()
        {
            float targetYaw = 0f;
            float targetPitch = 0f;
            if (joystick != null)
            {
                Vector2 input = joystick.Direction;
                if (input.sqrMagnitude > 0f)
                {
                    targetYaw   = input.x * maxOffsetAngle;
                    targetPitch = -input.y * maxOffsetAngle;
                }
            }

            currentYawOffset   = Mathf.MoveTowards(
                currentYawOffset,   targetYaw,
                returnSpeed * Time.deltaTime
            );
            currentPitchOffset = Mathf.MoveTowards(
                currentPitchOffset, targetPitch,
                returnSpeed * Time.deltaTime
            );

            Vector3 baseDir = centralPoint.position - transform.position;
            Vector3 pitchedDir = Quaternion.AngleAxis(
                currentPitchOffset, transform.right
            ) * baseDir;
            Vector3 finalDir = Quaternion.AngleAxis(
                currentYawOffset, Vector3.up
            ) * pitchedDir;
            transform.rotation = Quaternion.LookRotation(finalDir, Vector3.up);
        }
    }
}
