using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SFXCollision : SFXMaker
{
    [SerializeField] float cooldown = 1f;
    float cooldownTimer = 0f;

    private void Update()
    {
        if(cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision" + collision.collider.gameObject.name);
        if(cooldownTimer <= 0) 
        {
            base.MakeSound(out float clipLength);
            cooldownTimer = cooldown;
        }
    }
}
