using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VehiculeMotor : MonoBehaviour
{
    private Rigidbody _rb;
    private Wheel[] _wheels;

    [SerializeField] private Transform _centerOfMass;

    [HideInInspector] public float MotorTorque;
    [HideInInspector] public float BrakeTorque;
    [HideInInspector] public float SteerAngle;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = _centerOfMass.transform.localPosition;

        _wheels = GetComponentsInChildren<Wheel>();
    }

    private void Update()
    {
        ApplyForceOnWheels();
    }

    private void ApplyForceOnWheels()
    {
        foreach (Wheel wheel in _wheels)
        {
            wheel.MotorTorque = MotorTorque;
            wheel.BrakeTorque = BrakeTorque;
            wheel.SteerAngle = SteerAngle;
        }
    }

    public void InstantStopVehicule()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    public float GetRPM()
    {
        float? rpm = _wheels.LastOrDefault(x => !x.IsDirectionalWheel)?.rpm;

        return rpm == null ? 0f : (float)rpm;
    }
}
