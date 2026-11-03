using UnityEngine;

namespace GameDevelopmentPractice.Player
{
    public sealed class CameraFollow : MonoBehaviour
    {
        [Header("Follow Settings")]
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 3f, -6f);
        [SerializeField, Min(0f)] private float followSmoothness = 5f;

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

            // Start in the correct position instead of moving there slowly.
            transform.position = target.position + offset;
            LookAtTarget();
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                WarnAboutMissingTarget();
                return;
            }

            // The offset is in world space, so it does not rotate with the Player.
            Vector3 desiredPosition = target.position + offset;
            float followAmount = followSmoothness * Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, desiredPosition, followAmount);
            LookAtTarget();
        }

        private void LookAtTarget()
        {
            transform.LookAt(target.position + lookAtOffset);
        }

        private void WarnAboutMissingTarget()
        {
            if (missingTargetWarningShown)
            {
                return;
            }

            Debug.LogWarning(
                "CameraFollow needs a Target. Drag the Player into the Target field in the Inspector.",
                this);
            missingTargetWarningShown = true;
        }
    }
}
