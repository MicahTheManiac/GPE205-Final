using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 20, -15);

    // Start is called before the first frame update
    void Start()
    {
        // Set Position at the Start (Avoids panning to Player at Start)
        transform.position = target.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate our Position and Smoothness
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Always look at target
        transform.LookAt(target);
    }
}
