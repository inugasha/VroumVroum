using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VehiculeMotor : MonoBehaviour
{
    [SerializeField] private Wheel[] _wheels;

    [HideInInspector] public float motorTorque;
    [HideInInspector] public float brakeTorque;
    [HideInInspector] public float steerAngle;
    [SerializeField] Transform _centerOfMass;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = _centerOfMass.transform.localPosition;
    }

    private void Update()
    {
        ApplyForceOnWheels();
    }

    private void ApplyForceOnWheels()
    {
        foreach (Wheel wheel in _wheels)
        {
            wheel.motorTorque = motorTorque;
            wheel.brakeTorque = brakeTorque;
            wheel.steerAngle = steerAngle;
        }
    }
}
