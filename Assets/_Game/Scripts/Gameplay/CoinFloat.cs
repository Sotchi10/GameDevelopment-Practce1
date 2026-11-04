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
    
        startLocalPosition = transform.localPosition;
        
        timeOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
       
        float newY = startLocalPosition.y + Mathf.Sin((Time.time * floatSpeed) + timeOffset) * floatAmplitude;
        transform.localPosition = new Vector3(startLocalPosition.x, newY, startLocalPosition.z);

      
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
   
        if (other.CompareTag("Player"))
        {
           
            Destroy(gameObject);
        }
    }
}