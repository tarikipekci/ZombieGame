using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public float minX, maxX;
    public float minY, maxY;
    private Vector3 rot;
    public GameObject Player;
    
    private bool _canShoot = true;
    
    private void Update()
    {
        rot = Player.transform.localRotation.eulerAngles;
        if (rot.x != 0 || rot.y != 0)
        {
            Player.transform.localRotation = Quaternion.Slerp(Player.transform.rotation, Quaternion.Euler(0f,rot.y,0f), Time.deltaTime * 3f);
        }
        
        if (gameObject.CompareTag("MachineGun"))
        {
            if (Time.frameCount % 10 == 0)
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
            Recoil();
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }

        yield return new WaitForSeconds(timeBetweenShots);
        _canShoot = true;
    }

    private void Recoil()
    {
        float recX = Random.Range(minX, maxX);
        float recY = Random.Range(minY, maxY);
        Player.transform.localRotation = Quaternion.Euler(rot.x - recY , rot.y , rot.z);
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