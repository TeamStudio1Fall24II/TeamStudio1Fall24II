using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour, IDamageable
{
     public PlayerData m_PlayerData;
     private int currentHealth;

     private void OnEnable()
     {
          currentHealth = m_PlayerData.MaxHealth;
     }

     public void TakeDamage(int damage)
     {
          Debug.Log("Player has been hit! " + damage + " Damage");
          currentHealth -= damage;
          if(currentHealth <= 0)
          {
               // TODO: implement death
               Debug.Log("0 health. I should be dead");
               currentHealth = m_PlayerData.MaxHealth;
          }
     }
}
