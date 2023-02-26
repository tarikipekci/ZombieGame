using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 30f;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private AmmoController ammoSlot;
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private float timeBetweenShots = 0.5f;

    // ReSharper disable once ConvertToConstant.Local
    private bool _canShoot = true;

    private void Update()
    {
        if (gameObject.CompareTag("MachineGun"))
        {
            if (Time.frameCount % 5 == 0)
            {
                if (Input.GetMouseButton(0))
                {
                    StartCoroutine(Shoot());
                }
            }
        }
        else if (gameObject.CompareTag("Pistol"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Shoot());
            }
        }
        else if (gameObject.CompareTag("Shotgun"))
        {
            if (Input.GetMouseButtonDown(0) && _canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        _canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }

        yield return new WaitForSeconds(timeBetweenShots);
        _canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out var hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
    }

    private void CreateHitImpact(RaycastHit hitInfo)
    {
        GameObject hitImpact = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        Destroy(hitImpact, .1f);
    }
}