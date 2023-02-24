using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private int currentWeapon;
    private bool _machineGunSelected, _shotgunSelected, _pistolSelected;
    public Animator animForMachinegun, animForShotgun, animForPistol;
    public WeaponZoom weaponZoom;

    private void Start()
    {
        SetWeaponActive();
        _machineGunSelected = true;
        _shotgunSelected = false;
        _pistolSelected = false;
    }

    private void Update()
    {
        var previousWeapon = currentWeapon;
        // ReSharper disable once Unity.PreferAddressByIdToGraphicsParams
        animForMachinegun.SetBool("selected", _machineGunSelected);
        // ReSharper disable once Unity.PreferAddressByIdToGraphicsParams
        animForShotgun.SetBool("selected", _shotgunSelected);
        // ReSharper disable once Unity.PreferAddressByIdToGraphicsParams
        animForPistol.SetBool("selected", _pistolSelected);
        ProcessKeyInput();
        //ProcessScrollWheel();

        if (previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }

    private void ProcessScrollWheel()
    {
    }

    private void ProcessKeyInput()
    {
        if (weaponZoom.fpsCamera.m_Lens.FieldOfView != weaponZoom.zoomedInFOVMachineGun)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentWeapon = 0;
                _machineGunSelected = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentWeapon = 1;
                _pistolSelected = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentWeapon = 2;
                _shotgunSelected = true;
            }
            else
            {
                return;
            }
        }
    }

    private void SetWeaponActive()
    {
        var weaponIndex = 0;

        foreach (Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            weaponIndex++;
        }
    }
}