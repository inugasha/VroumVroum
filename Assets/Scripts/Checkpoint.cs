using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    public int CheckpointNumber;

    private void OnTriggerEnter(Collider other)
    {
        VehiculeMotor motor = other.GetComponent<VehiculeMotor>();
        if (motor != null)
        {
            GameManager.Instance.CheckpointPass(CheckpointNumber);
        }
    }
}
