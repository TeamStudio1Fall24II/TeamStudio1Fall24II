using UnityEditor;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
     public GameObject Projectile;
     public Transform ProjectileSpawnPoint;
     public float ProjectileSpeed = 10.0f;
     public float ProjectileSpawnDistance = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void Launch()
     {
          Vector3 spawnPosition = ProjectileSpawnPoint.transform.position + ProjectileSpawnPoint.transform.forward * ProjectileSpawnDistance;
          GameObject projectileObj =  Instantiate(Projectile, spawnPosition, transform.rotation);
          projectileObj.GetComponent<Rigidbody>().linearVelocity = projectileObj.transform.forward * ProjectileSpeed;
     }
}
