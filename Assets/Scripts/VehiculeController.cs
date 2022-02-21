using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(VehiculeMotor))]
public class VehiculeController : MonoBehaviour
{
    private VehiculeMotor _motor;
    private float movementX;
    private float movementY;

    public float motorTorque = 0.0f;
    public float brakeTorque;
    public float rotation;

    private void Start()
    {
        _motor = GetComponent<VehiculeMotor>();
    }

    private void Update()
    {
        if (movementY  > 0.0f)
        {
            _motor.motorTorque = movementY * motorTorque;
            _motor.brakeTorque = 0.0f;
        }
        else
        {
            if (movementY < 0.0f)
            {
                _motor.brakeTorque = (-movementY) * motorTorque * 10;
            }
            else
            {
                _motor.brakeTorque = motorTorque;
            }
        }
        _motor.steerAngle = movementX * rotation;
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
        Debug.Log(movementY);
    }

    /*
    private void OnAcceleration(AxisControl inputValue)
    {
        Debug.Log(inputValue);
    }
    */
}
