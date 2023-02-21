using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fpCamera;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 30f;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject hitEffect;

    private void Update()
    {
        if (gameObject.CompareTag("MachineGun"))
        {
            if (Time.frameCount % 5 == 0)
            {
                if (Input.GetButton("Fire1"))
                {
                    Shoot();
                }
            }
        }
    }

    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();
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
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hitInfo)
    {
        GameObject hitImpact = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        Destroy(hitImpact, .1f);
    }
}