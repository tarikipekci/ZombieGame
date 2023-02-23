using System;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera fpsCamera;
    public FirstPersonController firstPersonController;
    [SerializeField] private float zoomedOutFOV = 40f;
    [SerializeField] private float zoomedInFOVMachineGun = 20f;
    private bool _zoomedInToggle;
    [SerializeField] private float zoomInRotationSpeed = 0.2f;
    [SerializeField] private float zoomOutRotationSpeed = 1f;
    public Animator animForMachineGun, animForShotgun;

    private void Awake()
    {
        fpsCamera.m_Lens.FieldOfView = zoomedOutFOV;
        animForMachineGun.SetBool("zoomIn", false);
        animForShotgun.SetBool("zoomIn", false);
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        if (_zoomedInToggle == false)
        {
            _zoomedInToggle = true;
            fpsCamera.m_Lens.FieldOfView = zoomedInFOVMachineGun;

            firstPersonController.RotationSpeed = zoomInRotationSpeed;
            animForMachineGun.SetBool("zoomIn", true);
            animForShotgun.SetBool("zoomIn", true);
        }
        else
        {
            _zoomedInToggle = false;
            fpsCamera.m_Lens.FieldOfView = zoomedOutFOV;

            firstPersonController.RotationSpeed = zoomOutRotationSpeed;
            animForMachineGun.SetBool("zoomIn", false);
            animForShotgun.SetBool("zoomIn", false);
        }
    }
}