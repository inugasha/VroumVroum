using System.Linq;
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

    [HideInInspector] public int DeviceId;
    private Gamepad _gamepad;

    private void Start()
    {
        _motor = GetComponent<VehiculeMotor>();
        _vehicule = GetComponent<Vehicule>();
    }

    private void Update()
    {
        if (!GameManager.Instance.GameStart) return;
        CalculateTorque();
        CalculateWheelsRotation();
    }

    public void SetDeviceId(int deviceId)
    {
        DeviceId = deviceId;

        _gamepad = Gamepad.all.ToList().FirstOrDefault(x => x.deviceId == DeviceId);
        if (_gamepad == null)
        {
            Debug.LogError("No device found with this Id : " + deviceId);
            this.enabled = false;
        }
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
            if (_moveBackward < 0f)
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
    }

    private void CalculateWheelsRotation()
    {
        _motor.SteerAngle = _movementX * Rotation;
    }

    private void OnMove()
    {
        _movementX = _gamepad.leftStick.ReadValue().x;
    }

    private void OnAcceleration()
    {
        _moveForward = _gamepad.rightTrigger.ReadValue();
    }

    private void OnDecceleration()
    {
        _moveBackward = -_gamepad.leftTrigger.ReadValue();
    }


    private void OnRespawn()
    {
        if (_gamepad.buttonNorth.IsPressed(1f))
        {
            _vehicule.Respawn();
        }
    }
}
