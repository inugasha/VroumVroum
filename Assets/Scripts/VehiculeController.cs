using UnityEngine;

[RequireComponent(typeof(VehiculeMotor))]
public class VehiculeController : MonoBehaviour
{
    private VehiculeMotor _motor;

    public float motorTorque;
    public float brakeTorque;

    private void Start()
    {
        _motor = GetComponent<VehiculeMotor>();
    }

    private void Update()
    {
        _motor.motorTorque = motorTorque;
        _motor.brakeTorque = brakeTorque;
        _motor.steerAngle = 25f;
    }
}
