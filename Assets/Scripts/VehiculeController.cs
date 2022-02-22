using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(VehiculeMotor),typeof(Vehicule))]
public class VehiculeController : MonoBehaviour
{
    private VehiculeMotor _motor;
    private Vehicule _vehicule;
    private float _movementX;
    private float _movementY;

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
        if (_movementY > 0.0f)
        {
            _motor.MotorTorque = _movementY * MotorTorque;
            _motor.BrakeTorque = 0.0f;
        }
        else
        {
            _motor.BrakeTorque = _movementY < 0.0f ? (-_movementY) * MotorTorque * 10 : MotorTorque;
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
