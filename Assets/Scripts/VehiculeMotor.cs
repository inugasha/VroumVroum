using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VehiculeMotor : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private Wheel[] _wheels;
    [SerializeField] private Transform _centerOfMass;

    [HideInInspector] public float MotorTorque;
    [HideInInspector] public float BrakeTorque;
    [HideInInspector] public float SteerAngle;


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
            wheel.MotorTorque = MotorTorque;
            wheel.BrakeTorque = BrakeTorque;
            wheel.SteerAngle = SteerAngle;
        }
    }
}
