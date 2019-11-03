using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {

    public Vector3 position;
    public Vector3 direction;
    public float distance = 3.0f;
    public float height = 1.50f;
    public float heightDamping = 2.0f;
    public float positionDamping = 2.0f;
    public float rotationDamping = 2.0f;

    // Update is called once per frame
    void LateUpdate()
    {
        // Early exit if there’s no target
        float wantedHeight = position.y + height;
        float currentHeight = transform.position.y;

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight,
                                heightDamping * Time.deltaTime);

        // Set the position of the camera
        Vector3 wantedPosition = position - direction * distance;
        transform.position = Vector3.Lerp(transform.position, wantedPosition,
                                        Time.deltaTime * positionDamping);

        // Adjust the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight,
                                        transform.position.z);

        // Set the forward to rotate with time
        transform.forward = Vector3.Lerp(transform.forward, direction,
                                        Time.deltaTime * rotationDamping);
    }
}
