
using UnityEngine;

namespace GameDevelopmentPractice.Player
{
    public sealed class CameraFollow : MonoBehaviour
    {
        [Header("Follow Settings")]
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 3f, -6f);
        [SerializeField, Min(0f)] private float followSmoothness = 5f;

        [Header("Camera Direction")]
        [SerializeField] private float cameraYaw = 0f;

        [Header("Look Settings")]
        [SerializeField] private Vector3 lookAtOffset = new Vector3(0f, 1f, 0f);

        private bool missingTargetWarningShown;

        private void Start()
        {
            if (target == null)
            {
                WarnAboutMissingTarget();
                return;
            }

            UpdateCameraPosition(true);
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                WarnAboutMissingTarget();
                return;
            }

            UpdateCameraPosition(false);
        }

        private void UpdateCameraPosition(bool instant)
        {
            Vector3 rotatedOffset = Quaternion.Euler(0f, cameraYaw, 0f) * offset;
            Vector3 desiredPosition = target.position + rotatedOffset;

            if (instant)
            {
                transform.position = desiredPosition;
            }
            else
            {
                float followAmount = Mathf.Clamp01(
                    followSmoothness * Time.deltaTime
                );

                transform.position = Vector3.Lerp(
                    transform.position,
                    desiredPosition,
                    followAmount
                );
            }

            transform.LookAt(target.position + lookAtOffset);
        }

        private void WarnAboutMissingTarget()
        {
            if (missingTargetWarningShown)
            {
                return;
            }

            Debug.LogWarning(
                "CameraFollow needs a Target.",
                this
            );

            missingTargetWarningShown = true;
        }
    }
}
