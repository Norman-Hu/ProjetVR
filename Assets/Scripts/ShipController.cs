using System;
using UnityEngine;
using UnityEngine.Serialization;

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

    [SerializeField]
    private Transform joystick;
    [SerializeField]
    private Transform joystickBase;
    
    private Rigidbody _rb;

    private bool _isUsingLinearPropulsion = false;
    private bool _isUsingAngularPropulsion = false;

    private ConfigurableJoint _joint;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _joint = joystick.GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        Vector3 joystickUp = joystickBase.InverseTransformDirection(joystick.transform.up);
        Vector3 eulerAngles = Quaternion.FromToRotation(Vector3.up, joystickUp).eulerAngles;
        float x = eulerAngles.x;
        float z = eulerAngles.z;
        x = x > 180.0f ? x - 360.0f : x;
        z = z > 180.0f ? z - 360.0f : z;
        x = Math.Clamp(x, -40.0f, 40.0f);
        z = Math.Clamp(z, -40.0f, 40.0f);
        z = -z/40.0f;
        x = x/40.0f;
        x = Math.Abs(x) < .5f ? 0.0f : x;
        z = Math.Abs(z) < .5f ? 0.0f : z;
        HandleInput(x, z);
        if (flightAssistance)
            HandleFlightAssist();
    }

    private void HandleInput(float vertical, float horizontal)
    {
        // Adds angular and linear accelerations according to the inputs.
        // The checks make sure that the ship cannot accelerate if it is already over the speed limit.
        
        // Rotation handling
        _isUsingAngularPropulsion = false;
        var yawInput = horizontal;
        var pitchInput = vertical;
        // print("" + yawInput + " " + pitchInput);
        var pitchAcceleration = -pitchInput * rotationForce * Time.deltaTime;
        var yawAcceleration = yawInput * rotationForce * Time.deltaTime;
        
        var localAngularVelocity = Quaternion.Inverse(transform.rotation) * _rb.angularVelocity;
        if ((pitchAcceleration > 0.0f && localAngularVelocity.x < maxAngularVelocity.x)
            || (pitchAcceleration < 0.0f && localAngularVelocity.x > -maxAngularVelocity.x))
        {
            _rb.AddRelativeTorque(Vector3.right * pitchAcceleration, ForceMode.Force);
            _isUsingAngularPropulsion = true;
        }
        if ((yawAcceleration > 0.0f && localAngularVelocity.y < maxAngularVelocity.y)
            || (yawAcceleration < 0.0f && localAngularVelocity.y > -maxAngularVelocity.y))
        {
            _rb.AddRelativeTorque(Vector3.up * yawAcceleration, ForceMode.Force);
            _isUsingAngularPropulsion = true;
        }

        // Translation handling
        // _isUsingLinearPropulsion = false;
        // var throttleInput = Input.GetAxis("Throttle");
        // var throttleAcceleration = throttleInput * throttleForce * Time.deltaTime;
        //
        // var localVelocity = Quaternion.Inverse(transform.rotation) * _rb.velocity;
        // if ((throttleAcceleration > 0.0f && localVelocity.z < maxForwardVelocity)
        //     || (throttleAcceleration < 0.0f && localVelocity.z > -maxForwardVelocity))
        // {
        //     _rb.AddForce(transform.forward * throttleAcceleration, ForceMode.Acceleration);
        //     _isUsingLinearPropulsion = true;
        // }
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
