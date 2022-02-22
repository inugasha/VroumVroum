using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class Wheel : MonoBehaviour
{
    private WheelCollider _wheelCollider;

    [HideInInspector] public float MotorTorque;
    [HideInInspector] public float BrakeTorque;
    [HideInInspector] public float SteerAngle;

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
        _wheelCollider.motorTorque = MotorTorque;
        _wheelCollider.brakeTorque = BrakeTorque;
    }

    private void PerformAngle()
    {
        _wheelCollider.steerAngle = SteerAngle;
    }
}
