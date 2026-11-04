using UnityEngine;

public class CoinFloat : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float floatSpeed = 3f;      
    [SerializeField] private float floatAmplitude = 0.1f; 
    [SerializeField] private float rotationSpeed = 100f;   

    private Vector3 startLocalPosition;
    private float timeOffset;

    void Start()
    {
        // Cache initial local position
        startLocalPosition = transform.localPosition;
        
        // Optional: random offset so all coins don't bob in mechanical unison
        timeOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        // Bob up and down on the local Y-axis
        float newY = startLocalPosition.y + Mathf.Sin((Time.time * floatSpeed) + timeOffset) * floatAmplitude;
        transform.localPosition = new Vector3(startLocalPosition.x, newY, startLocalPosition.z);

        // Spin locally around its own up-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the player
        if (other.CompareTag("Player"))
        {
            // Add score logic here if needed (e.g., GameManager.instance.AddScore(1);)

            // Makes the coin vanish
            Destroy(gameObject);
        }
    }
}