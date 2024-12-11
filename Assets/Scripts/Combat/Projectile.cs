using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
     public int Damage = 1;
     public float TimeToLive = 5.0f;
     private bool isLive = true;
     public bool playerShot = false;

     private void OnEnable()
     {
          isLive = true;
     }

     // Update is called once per frame
     void Update()
    {
          TimeToLive -= Time.deltaTime;
          if(TimeToLive < 0.0f)
          {
               Destroy(gameObject);
          }
    }

     private void OnCollisionEnter(Collision other)
     {
          // if not live, don't do anything
          if(!isLive) { return; }
          isLive = false;
          // do damage
          IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
          if(damageable != null)
          {
               // If enemy shot or player shot and other is not player
               // prevents player from huring self
               if (!playerShot || !other.gameObject.CompareTag("Player"))
                    damageable.TakeDamage(Damage);
          }
          // Slow momentum
          GetComponent<Rigidbody>().linearVelocity *= 0.5f;
          // set short life timer after collision.
          TimeToLive = 2.0f;
     }
}
