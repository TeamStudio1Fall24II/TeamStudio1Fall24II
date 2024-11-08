using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
     public int Damage = 1;
     public float TimeToLive = 5.0f;
     private bool isLive = true;

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

     // TODO: player can shoot themself in the belly
     private void OnCollisionEnter(Collision other)
     {
          // if not live, don't do anything
          if(!isLive) { return; }
          isLive = false;
          // do damage
          IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
          if(damageable != null)
          {
               damageable.TakeDamage(Damage);
          }
          // Slow momentum
          GetComponent<Rigidbody>().linearVelocity *= 0.5f;
          // set short life timer after collision.
          TimeToLive = 2.0f;
     }
}
