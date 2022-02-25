using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(VehiculeMotor), typeof(Vehicule))]
public class VehiculeController : MonoBehaviour
{
    [SerializeField] private Transform _graphicParent;
    [SerializeField] private DamageData[] _damageDatas;

    private VehiculeMotor _motor;
    private Vehicule _vehicule;
    private float _moveForward;
    private float _moveBackward;
    private float _movementX;
    private Gamepad _gamepad;

    [Header("Motor speed")] public float MotorTorque = 25f;
    [Header("Motor braking")] public float BrakeTorque = 50f;
    [Header("Wheel rotation")] public float Rotation = 30f;
    [HideInInspector] public int DeviceId;

    public void SetDeviceId(int deviceId, int playerIndex)
    {
        DeviceId = deviceId;

        _gamepad = Gamepad.all.ToList().FirstOrDefault(x => x.deviceId == DeviceId);
        if (_gamepad == null)
        {
            Debug.LogError("No device found with this Id : " + deviceId);
            this.enabled = false;
        }

        int i = 0;

        foreach (Transform child in _graphicParent)
        {
            child.gameObject.SetActive(playerIndex == i);
            i++;
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

    #region Event

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

    private void OnCollisionEnter(Collision collision)
    {
        DamageData? data = _damageDatas.FirstOrDefault(x => x.Tag == collision.gameObject.tag);
        if (data == null) return;

        _vehicule.TakeDamage(((DamageData)data).Damage);
    }

    #endregion
}
