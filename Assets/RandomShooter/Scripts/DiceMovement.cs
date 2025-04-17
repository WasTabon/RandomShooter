using UnityEngine;

namespace RandomShooter.Scripts
{
    public class DiceMovement : MonoBehaviour
    {
        public bool canMove;
        public Transform centerPoint;
        public float radius = 2f;
        public float rotationSpeed = 50f;

        private float currentAngle;
        public float CurrentAngle => currentAngle;

        private bool initialized = false;

        private void Update()
        {
            if (!canMove)
                return;

            if (centerPoint == null)
                return;

            if (!initialized)
            {
                InitializeAngle();
                initialized = true;
            }

            currentAngle += rotationSpeed * Time.deltaTime;
            if (currentAngle >= 360f)
                currentAngle -= 360f;

            float radians = currentAngle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radians) * radius;
            float z = Mathf.Sin(radians) * radius;

            transform.position = new Vector3(centerPoint.position.x + x, transform.position.y, centerPoint.position.z + z);
        }

        private void InitializeAngle()
        {
            Vector3 dir = transform.position - centerPoint.position;
            currentAngle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        }
    }
}