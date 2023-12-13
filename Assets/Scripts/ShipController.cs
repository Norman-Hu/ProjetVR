using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    [SerializeField]
    private float rotationForce = 25.0f;
    
    [SerializeField]
    private float throttleForce = 250.0f;
    
    [Space]
    
    [Tooltip("Angular velocity beyond which the ship will stop accelerating.")]    
    [SerializeField]
    private Vector2 maxAngularVelocity = new Vector2(1.0f, 1.0f);
    
    [Tooltip("Linear velocity beyond which the ship will stop accelerating.")]    
    [SerializeField]
    private float maxForwardVelocity = 10.0f;
    
    [Space]
    
    [Tooltip("Enable propellers to simulate drag.")]
    [SerializeField]
    private bool flightAssistance = false;
    
    [SerializeField]
    private float flightAssistRotationForce = 50.0f;

    [SerializeField]
    private float flightAssistThrottleForce = 50.0f;
    
    private Rigidbody _rb;

    private bool _isUsingLinearPropulsion = false;
    private bool _isUsingAngularPropulsion = false;
    
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
        if (flightAssistance)
            HandleFlightAssist();
    }

    private void HandleInput()
    {
        // Adds angular and linear accelerations according to the inputs.
        // The checks make sure that the ship cannot accelerate if it is already over the speed limit.
        
        // Rotation handling
        _isUsingAngularPropulsion = false;
        var yawInput = Input.GetAxis("Yaw");
        var pitchInput = Input.GetAxis("Pitch");
        var pitchAcceleration = -pitchInput * rotationForce * Time.deltaTime;
        var yawAcceleration = yawInput * rotationForce * Time.deltaTime;
        
        var localAngularVelocity = Quaternion.Inverse(transform.rotation) * _rb.angularVelocity;
        if ((pitchAcceleration > 0.0f && localAngularVelocity.x < maxAngularVelocity.x)
            || (pitchAcceleration < 0.0f && localAngularVelocity.x > -maxAngularVelocity.x))
        {
            _rb.AddRelativeTorque(Vector3.right * pitchAcceleration, ForceMode.Acceleration);
            _isUsingAngularPropulsion = true;
        }
        if ((yawAcceleration > 0.0f && localAngularVelocity.y < maxAngularVelocity.y)
            || (yawAcceleration < 0.0f && localAngularVelocity.y > -maxAngularVelocity.y))
        {
            _rb.AddRelativeTorque(Vector3.up * yawAcceleration, ForceMode.Acceleration);
            _isUsingAngularPropulsion = true;
        }

        // Translation handling
        _isUsingLinearPropulsion = false;
        var throttleInput = Input.GetAxis("Throttle");
        var throttleAcceleration = throttleInput * throttleForce * Time.deltaTime;
        
        var localVelocity = Quaternion.Inverse(transform.rotation) * _rb.velocity;
        if ((throttleAcceleration > 0.0f && localVelocity.z < maxForwardVelocity)
            || (throttleAcceleration < 0.0f && localVelocity.z > -maxForwardVelocity))
        {
            _rb.AddForce(transform.forward * throttleAcceleration, ForceMode.Acceleration);
            _isUsingLinearPropulsion = true;
        }
    }

    private void HandleFlightAssist()
    {
        // Linear
        if (!_isUsingLinearPropulsion)
            _rb.AddForce(flightAssistThrottleForce*Time.deltaTime*(-_rb.velocity));
        
        // Angular
        if (!_isUsingAngularPropulsion)
        {
            const float lowSpeedMultiplier = 5.0f;
            Vector3 angularVel = _rb.angularVelocity;
            float magnitude = angularVel.magnitude;
            Vector3 drag = flightAssistRotationForce * Time.deltaTime * (-angularVel);
            if (magnitude < .25f)
                drag *= lowSpeedMultiplier;
            _rb.AddTorque(drag, ForceMode.Acceleration);
        }
    }
}
