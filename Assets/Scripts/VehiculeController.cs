using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(VehiculeMotor), typeof(Vehicule))]
public class VehiculeController : MonoBehaviour
{
    private VehiculeMotor _motor;
    private Vehicule _vehicule;
    private float _moveForward;
    private float _moveBackward;
    private float _movementX;

    [Header("Motor speed")] public float MotorTorque = 25f;
    [Header("Motor braking")] public float BrakeTorque = 50f;
    [Header("Wheel rotation")] public float Rotation = 30f;

    private void Start()
    {
        _motor = GetComponent<VehiculeMotor>();
        _vehicule = GetComponent<Vehicule>();
    }

    private void Update()
    {
        CalculateTorque();
        CalculateWheelsRotation();
    }

    private void CalculateTorque()
    {
        float movement = _moveForward > 0 ? _moveForward : _moveBackward * .5f;

        //_motor.MotorTorque = movement * MotorTorque;

       float rpm = _motor.GetRPM();

        if (rpm < 10f && rpm > -10f)
        {
            //bouge pas


            if (_moveBackward < 0f || _moveForward > 0f)
            {
                _motor.BrakeTorque = 0f;
                _motor.MotorTorque = movement * MotorTorque;
            }
            else
            {
                _motor.BrakeTorque = BrakeTorque;
                _motor.MotorTorque = 0f;
            }


        }
        else if (rpm > 10f)
        {
            //avance
            if(_moveBackward < 0f)
            {
                //avance et on appuie pour reculer
                _motor.BrakeTorque = BrakeTorque;
                _motor.MotorTorque = 0f;
            }
            else
            {
                //avance
                _motor.BrakeTorque = 0f;
                _motor.MotorTorque = movement * MotorTorque;
            }
        }
        else
        {
            //recule
            if (_moveForward > 0f)
            {
                //recule et on appuie pour avancer
                _motor.BrakeTorque = BrakeTorque;
                _motor.MotorTorque = 0f;
            }
            else
            {
                //recule
                _motor.BrakeTorque = 0f;
                _motor.MotorTorque = movement * MotorTorque;
            }
        }



        /*
        if(rpm > 0)
        {
            //avance

            if(_moveBackward == 0f)
            {
                _motor.BrakeTorque = 0f;
            }

        }
        else if(rpm < 0)
        {
            //recule

            if (_moveForward == 0f)
            {
                _motor.BrakeTorque = 0f;
            }
        }
        */
    }

    private void CalculateWheelsRotation()
    {
        _motor.SteerAngle = _movementX * Rotation;
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
    }

    private void OnAcceleration()
    {
        _moveForward = Gamepad.current.rightTrigger.ReadValue();
    }

    private void OnDecceleration()
    {
        _moveBackward = -Gamepad.current.leftTrigger.ReadValue();
    }


    private void OnRespawn()
    {
        _vehicule.Respawn();
    }

    private void OnDamage()
    {
        _vehicule.TakeDamage(25);
    }
}
