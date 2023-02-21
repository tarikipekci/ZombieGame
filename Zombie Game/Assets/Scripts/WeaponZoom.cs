using Cinemachine;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera fpsCamera;
    [SerializeField] private float zoomedOutFOV = 40f;
    [SerializeField] private float zoomedInFOVMachineGun = 20f;
    private bool _zoomedInToggle;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_zoomedInToggle == false)
            {
                _zoomedInToggle = true;
                fpsCamera.m_Lens.FieldOfView = zoomedInFOVMachineGun;
            }
            else
            {
                _zoomedInToggle = false;
                fpsCamera.m_Lens.FieldOfView = zoomedOutFOV;
            }
        }
    }
}