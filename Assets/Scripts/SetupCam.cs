using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class SetupCam : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cam;

    public void Setup(GameObject target)
    {
        _cam.Follow = target.transform;
        _cam.LookAt = target.transform;
    }
}
