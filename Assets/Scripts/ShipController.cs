using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float rotationSpeed = 10.0f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float yaw = horizontalInput * rotationSpeed * Time.deltaTime;
        float pitch = -verticalInput * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, yaw);
        transform.Rotate(Vector3.right, pitch);
    }
}
