using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ProjectileLauncher : MonoBehaviour
{
     public GameObject Projectile;
     public Transform ProjectileSpawnPoint;
     public float ProjectileSpeed = 10.0f;
     public float ProjectileSpawnDistance = 1.0f;
     public int MaxAmmo = 12;
     public float reloadTime = 2.5f;
     public float reloadTimePerBullet = 0.2f;

     public int CurrentAmmo { get; private set; }
     public bool canFire = true;

     public UnityAction AmmoChangeEvent;
     public UnityAction CannotFireEvent;

     private void Start()
     {
          CurrentAmmo = MaxAmmo;
          AmmoChangeEvent?.Invoke();
     }

     public void Launch()
     {
          if(!canFire) 
          {
               CannotFireEvent?.Invoke();
               return; 
          }

          Vector3 spawnPosition = ProjectileSpawnPoint.transform.position + ProjectileSpawnPoint.transform.forward * ProjectileSpawnDistance;
          GameObject projectileObj =  Instantiate(Projectile, spawnPosition, transform.rotation);
          Rigidbody rb = projectileObj.GetComponent<Rigidbody>();
          rb.linearVelocity = projectileObj.transform.forward * ProjectileSpeed;
          Vector3 randomAngularVelocity = Random.insideUnitSphere * 50f; 
          rb.angularVelocity = randomAngularVelocity;

          CurrentAmmo -= 1;
          AmmoChangeEvent?.Invoke();
          if(CurrentAmmo == 0)
          {
               StartCoroutine(ReloadIterative());
          }
     }
     public void StartReload()
     {
          StartCoroutine(Reload());
     }
     public void StartReloadIterative()
     {
          StartCoroutine(ReloadIterative());
     }

     // TODO: Need some visual indication of reload
     IEnumerator ReloadIterative()
     {
          if (CurrentAmmo != MaxAmmo)
          {
               canFire = false;
               while (CurrentAmmo < MaxAmmo)
               {
                    yield return new WaitForSeconds(reloadTimePerBullet);
                    CurrentAmmo += 1;
                    AmmoChangeEvent?.Invoke();
               }
               canFire = true;
          }
     }

     IEnumerator Reload()
     {
          if (CurrentAmmo != MaxAmmo)
          {
               yield return new WaitForSeconds(reloadTime);
               CurrentAmmo = MaxAmmo;
               AmmoChangeEvent?.Invoke();
               canFire = true;
          }
     }
}
