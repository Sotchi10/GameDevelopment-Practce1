using UnityEngine;

namespace GameDevelopmentPractice.Gameplay
{
    public sealed class PingPongObstacle : MonoBehaviour
    {
        [SerializeField] private Vector3 localDirection = Vector3.right;
        [SerializeField, Min(0f)] private float distance = 3f;
        [SerializeField, Min(0.01f)] private float speed = 1.5f;

        private Vector3 startPosition;

        private void Awake()
        {
            startPosition = transform.localPosition;
        }

        private void Update()
        {
            Vector3 direction = localDirection.sqrMagnitude > 0f
                ? localDirection.normalized
                : Vector3.right;
            float offset = Mathf.PingPong(Time.time * speed, distance * 2f) - distance;
            transform.localPosition = startPosition + direction * offset;
        }
    }
}
