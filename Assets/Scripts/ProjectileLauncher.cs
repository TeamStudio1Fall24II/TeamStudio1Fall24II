using UnityEditor;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
     public GameObject Projectile;
     public Transform ProjectileSpawnPoint;
     public float ProjectileSpeed = 10.0f;

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
          GameObject projectileObj =  Instantiate(Projectile, ProjectileSpawnPoint.transform.position + ProjectileSpawnPoint.transform.forward, transform.rotation);
          projectileObj.GetComponent<Rigidbody>().linearVelocity = projectileObj.transform.forward * ProjectileSpeed;
     }
}
