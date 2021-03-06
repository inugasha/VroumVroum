using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    public int CheckpointNumber;
    public Transform[] RespawnPoints;

    private void OnTriggerEnter(Collider other)
    {
        VehiculeController controller = other.GetComponent<VehiculeController>();
        if (controller != null)
        {
            GameManager.Instance.CheckpointPass(CheckpointNumber, controller.DeviceId);
        }
    }
}
