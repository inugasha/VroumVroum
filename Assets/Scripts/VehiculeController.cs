using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(VehiculeMotor))]
public class VehiculeController : MonoBehaviour
{
    private VehiculeMotor _motor;
    private float movementX;

    public float motorTorque;
    public float brakeTorque;
    public float rotation;

    private void Start()
    {
        _motor = GetComponent<VehiculeMotor>();
    }

    private void Update()
    {
        _motor.motorTorque = motorTorque;
        _motor.brakeTorque = brakeTorque;
        _motor.steerAngle = movementX * rotation;
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
    }

    private void OnAcceleration(AxisControl inputValue)
    {
        Debug.Log(inputValue);
    }
}
