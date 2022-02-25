using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class SetupCam : MonoBehaviour
{
    CinemachineVirtualCamera _cam;

    private void Start()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
    }

    public void Setup(GameObject target)
    {
        _cam.Follow = target.transform;
        _cam.LookAt = target.transform;
    }
}
