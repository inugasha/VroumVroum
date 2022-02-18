using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class Wheel : MonoBehaviour
{
    private WheelCollider _wheelCollider;
    [HideInInspector] public float motorTorque;
    [HideInInspector] public float brakeTorque;
    [HideInInspector] public float steerAngle;
    public bool IsMotorWheel;

    private void Start()
    {
        _wheelCollider = GetComponent<WheelCollider>();
    }

    private void FixedUpdate()
    {
        if (IsMotorWheel)
        {
            PerformMovement();
        }
        else
        {
            PerformAngle();
        }
    }

    private void PerformMovement()
    {
        _wheelCollider.motorTorque = motorTorque;
        _wheelCollider.brakeTorque = brakeTorque;
    }

    private void PerformAngle()
    {
        _wheelCollider.steerAngle = steerAngle;
    }
}
