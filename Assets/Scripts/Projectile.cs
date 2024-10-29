using UnityEngine;

public class Projectile : MonoBehaviour
{
     public float TimeToLive = 5.0f;

     // TODO: If first collision hits target, do damage.

    // Update is called once per frame
    void Update()
    {
          TimeToLive -= Time.deltaTime;
          if(TimeToLive < 0.0f)
          {
               Destroy(gameObject);
          }
    }
}
