using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(VehiculeMotor))]
public class VehiculeController : MonoBehaviour
{
    private VehiculeMotor _motor;
    private float _movementX;
    private float _movementY;

    [Header("Motor speed")]public float MotorTorque = 25f;
    [Header("Motor braking")] public float BrakeTorque = 50f;
    [Header("Wheel rotation")] public float Rotation = 30f;

    private void Start()
    {
        _motor = GetComponent<VehiculeMotor>();
    }

    private void Update()
    {
        CalculateTorque();
        CalculateWheelsRotation();
    }

    private void CalculateTorque()
    {
        //if (_movementY > 0.0f)
        //{
        //    _motor.MotorTorque = _movementY * MotorTorque;
        //    _motor.BrakeTorque = 0.0f;
        //}
        //else
        //{
        //    //if (_movementY < 0.0f)
        //    //{
        //    //    _motor.BrakeTorque = (-_movementY) * MotorTorque * 10;
        //    //}
        //    //else
        //    //{
        //    //    _motor.BrakeTorque = MotorTorque;
        //    //}

        //    _motor.BrakeTorque = _movementY < 0.0f ? (-_movementY) * MotorTorque * 10 : MotorTorque;
        //}


        if(_movementY != 0.0f)
        {
            //Accelère ou recule
            _motor.MotorTorque = _movementY * MotorTorque;
            _motor.BrakeTorque = 0f;
        }
        else
        {
            //n'accelere pas
            _motor.MotorTorque = 0f;
            _motor.BrakeTorque = BrakeTorque;
        }
    }

    private void CalculateWheelsRotation()
    {
        _motor.SteerAngle = _movementX * Rotation;
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;
        //Debug.Log(_movementY);
    }

    /*
    private void OnAcceleration(AxisControl inputValue)
    {
        Debug.Log(inputValue);
    }
    */
}
