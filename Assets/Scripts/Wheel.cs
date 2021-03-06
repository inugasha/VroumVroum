using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class Wheel : MonoBehaviour
{
    [SerializeField] private Transform _wheelTransform;
    private WheelCollider _wheelCollider;

    [HideInInspector] public float MotorTorque;
    [HideInInspector] public float BrakeTorque;
    [HideInInspector] public float SteerAngle;

    public bool IsDirectionalWheel;
    public float rpm;

    private void Start()
    {
        _wheelCollider = GetComponent<WheelCollider>();
        _wheelTransform = GetComponentInChildren<MeshRenderer>().GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        PerformMovement();

        if (IsDirectionalWheel)
        {
            PerformAngle();
        }
        _wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        _wheelTransform.position = pos;
        _wheelTransform.rotation = rot;
    }

    private void PerformMovement()
    {
        _wheelCollider.motorTorque = MotorTorque;
        _wheelCollider.brakeTorque = BrakeTorque;
        rpm = _wheelCollider.rpm;
    }

    private void PerformAngle()
    {
        _wheelCollider.steerAngle = SteerAngle;
    }
}
