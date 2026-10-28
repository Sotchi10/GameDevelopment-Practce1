using UnityEngine;

namespace GameDevelopmentPractice.Gameplay
{
    public sealed class RotatingObstacle : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float degreesPerSecond = 75f;
        [SerializeField] private Vector3 localAxis = Vector3.up;

        private void Update()
        {
            transform.Rotate(localAxis, degreesPerSecond * Time.deltaTime, Space.Self);
        }
    }
}
