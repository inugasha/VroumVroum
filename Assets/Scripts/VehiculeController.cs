using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(VehiculeMotor))]
public class VehiculeController : MonoBehaviour
{
    private VehiculeMotor _motor;
    private float _movementX;
    private float _movementY;

    public float MotorTorque = 0.0f;
    public float BrakeTorque;
    public float Rotation;

    private void Start()
    {
        _motor = GetComponent<VehiculeMotor>();
    }

    private void Update()
    {
        if (_movementY  > 0.0f)
        {
            _motor.MotorTorque = _movementY * MotorTorque;
            _motor.BrakeTorque = 0.0f;
        }
        else
        {
            if (_movementY < 0.0f)
            {
                _motor.BrakeTorque = (-_movementY) * MotorTorque * 10;
            }
            else
            {
                _motor.BrakeTorque = MotorTorque;
            }
        }
        _motor.SteerAngle = _movementX * Rotation;
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;
        Debug.Log(_movementY);
    }

    /*
    private void OnAcceleration(AxisControl inputValue)
    {
        Debug.Log(inputValue);
    }
    */
}
