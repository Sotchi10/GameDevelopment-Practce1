using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoorController : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private Transform doorTransform;     // Drag your door parent object here
    [SerializeField] private Vector3 openOffset = new Vector3(0f, 3f, 0f); // Direction/distance the door slides
    [SerializeField] private float openDuration = 2f;   // Time in seconds the door stays open

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;

    void Start()
    {
        if (doorTransform != null)
        {
            closedPosition = doorTransform.position;
            openPosition = closedPosition + openOffset;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the rolling player hit the lever
        if (other.CompareTag("Player") && !isOpen)
        {
            StartCoroutine(OpenAndCloseDoor());
        }
    }

    private IEnumerator OpenAndCloseDoor()
    {
        isOpen = true;

        // Slide open over 0.5 seconds
        float elapsed = 0f;
        while (elapsed < 0.5f)
        {
            doorTransform.position = Vector3.Lerp(closedPosition, openPosition, elapsed / 0.5f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        doorTransform.position = openPosition;

        // Wait for 2 seconds
        yield return new WaitForSeconds(openDuration);

        // Slide back closed over 0.5 seconds
        elapsed = 0f;
        while (elapsed < 0.5f)
        {
            doorTransform.position = Vector3.Lerp(openPosition, closedPosition, elapsed / 0.5f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        doorTransform.position = closedPosition;

        isOpen = false;
    }
} 