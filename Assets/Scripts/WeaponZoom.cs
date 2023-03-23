using Cinemachine;
using StarterAssets;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] public Camera fpsCamera;
    public FirstPersonController firstPersonController;
    [SerializeField] public float zoomedOutFOV = 40f;
    [SerializeField] public float zoomedInFOVMachineGun = 20f;
    private bool _zoomedInToggle;
    [SerializeField] private float zoomInRotationSpeed = 0.2f;
    [SerializeField] private float zoomOutRotationSpeed = 1f;
    public Animator animForMachineGun, animForShotgun, animForPistol;

    private void Awake()
    {
        fpsCamera.fieldOfView = zoomedOutFOV;
        animForMachineGun.SetBool("zoomIn", false);
        animForShotgun.SetBool("zoomIn", false);
        animForPistol.SetBool("zoomIn",false);
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        if (_zoomedInToggle == false)
        {
            _zoomedInToggle = true;
            fpsCamera.fieldOfView = zoomedInFOVMachineGun;

            firstPersonController.RotationSpeed = zoomInRotationSpeed;
            animForMachineGun.SetBool("zoomIn", true);
            animForShotgun.SetBool("zoomIn", true);
            animForPistol.SetBool("zoomIn",true);
        }
        else
        {
            _zoomedInToggle = false;
            fpsCamera.fieldOfView = zoomedOutFOV;

            firstPersonController.RotationSpeed = zoomOutRotationSpeed;
            animForMachineGun.SetBool("zoomIn", false);
            animForShotgun.SetBool("zoomIn", false);
            animForPistol.SetBool("zoomIn",false);
        }
    }
}