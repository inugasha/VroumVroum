using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class Wheel : MonoBehaviour
{
    private WheelCollider _wheelCollider;
    [HideInInspector] public float motorTorque;
    [HideInInspector] public float brakeTorque;

    private void Start()
    {
        _wheelCollider = GetComponent<WheelCollider>();
    }

    private void FixedUpdate()
    {
        PerformMovement();
    }

    private void PerformMovement()
    {
        _wheelCollider.motorTorque = motorTorque;
        _wheelCollider.brakeTorque = brakeTorque;
    }
}
