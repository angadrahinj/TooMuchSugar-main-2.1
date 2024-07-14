using Cinemachine;
using UnityEngine;
 
public class CameraRegister : MonoBehaviour
{
    public bool setAsActiveCamera = false;
    private void OnEnable()
    {
        CinemachineVirtualCamera cam = GetComponent<CinemachineVirtualCamera>();

        cam.Follow = GameObject.FindWithTag("Player").transform;
        if(!setAsActiveCamera)
        {
            SetNonPriority(cam);
        }

        CameraManager.Register(cam);
    }
    private void OnDisable()
    {
        CameraManager.Unregister(GetComponent<CinemachineVirtualCamera>());
    }

    private void SetNonPriority(CinemachineVirtualCamera camera)
    {
        camera.Priority = 0;
    }
}