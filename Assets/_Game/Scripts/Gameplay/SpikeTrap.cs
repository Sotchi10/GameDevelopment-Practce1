using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [Header("Spike Settings")]
    [SerializeField] private float interval = 1f;       // Time in seconds between switches
    [SerializeField] private float hideDistance = 1.5f; // How far down they go to hide
    [SerializeField] private float popSpeed = 10f;      // How fast they snap up and down

    private Vector3 upPosition;
    private Vector3 downPosition;
    private bool isUp = true;
    private float timer;

    void Start()
    {
        // Record the starting position (spikes up) and calculate the hidden position
        upPosition = transform.position;
        downPosition = transform.position + (Vector3.down * hideDistance);
        
        timer = interval;
    }

    void Update()
    {
        // Count down the timer using Time.deltaTime
        timer -= Time.deltaTime;

        // When the timer hits zero, toggle the state and reset the timer
        if (timer <= 0f)
        {
            isUp = !isUp;
            timer = interval;
        }

        // Smoothly move the spikes to the current target position
        Vector3 targetPosition = isUp ? upPosition : downPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, popSpeed * Time.deltaTime);
    }
}