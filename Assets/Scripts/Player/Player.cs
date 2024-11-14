using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
     public FPSController m_Controller;
     public PlayerData m_PlayerData;
     public int currentHealth { get; private set; }
     public float currentStam { get; private set; }
     
     private Vector3 prevPos = Vector3.zero;
     private Vector3 currPos = Vector3.zero;
     public bool running => m_Controller.isRunning && prevPos != currPos;

     private Coroutine stamRecharge;
     private Coroutine healthRecharge;
     private bool isStamRecharging = false;

     public UnityAction PlayerHealthChangeEvent;
     public UnityAction PlayerStaminaChangeEvent;
     public UnityAction PlayerDeathEvent;
     


     private void Awake()
     {
          m_Controller = GetComponent<FPSController>();
     }

     private void OnEnable()
     {
          currentHealth = m_PlayerData.MaxHealth;
          currentStam = m_PlayerData.maxStam;
     }

     private void Update()
     {
          currPos = transform.position;

          if (running)
          {
               currentStam -= m_PlayerData.RunCost * Time.deltaTime;
               if (currentStam < 0) currentStam = 0;
               PlayerStaminaChangeEvent?.Invoke();

               if (stamRecharge != null) StopCoroutine(stamRecharge);
               isStamRecharging = false;
          }
          else if (currentStam < m_PlayerData.maxStam && !isStamRecharging)
          {
               stamRecharge = StartCoroutine(RechargeStamina());
          }
          m_Controller.canSprint = currentStam > 0;

          prevPos = currPos;
     }

     public void TakeDamage(int damage)
     {
          Debug.Log("Player has been hit! " + damage + " Damage");
          currentHealth -= damage;
          PlayerHealthChangeEvent.Invoke();
          if(currentHealth <= 0)
          {
               PlayerDeathEvent?.Invoke();
          }
          if (healthRecharge != null) StopCoroutine(healthRecharge);
          healthRecharge = StartCoroutine(RechargeHealth());
     }

     private IEnumerator RechargeHealth()
     {
          yield return new WaitForSeconds(4f);

          while (currentHealth < 30 && currentHealth > 0)
          {
               currentHealth += m_PlayerData.HealthChargeRate;
               if (currentHealth > m_PlayerData.MaxHealth) currentHealth = m_PlayerData.MaxHealth;
               PlayerHealthChangeEvent?.Invoke();
               yield return new WaitForSeconds(.1f);
          }

          while (currentHealth < 60 && currentHealth > 30)
          {
               currentHealth += m_PlayerData.HealthChargeRate;
               if (currentHealth > m_PlayerData.MaxHealth) currentHealth = m_PlayerData.MaxHealth;
               PlayerHealthChangeEvent?.Invoke();
               yield return new WaitForSeconds(.1f);
          }

          while (currentHealth < 90 && currentHealth > 60)
          {
               currentHealth += m_PlayerData.HealthChargeRate;
               if (currentHealth > m_PlayerData.MaxHealth) currentHealth = m_PlayerData.MaxHealth;
               PlayerHealthChangeEvent?.Invoke();
               yield return new WaitForSeconds(.1f);
          }
     }
     private IEnumerator RechargeStamina()
     {
          isStamRecharging = true;
          yield return new WaitForSeconds(2f);

          while (currentStam < m_PlayerData.maxStam)
          {
               currentStam += m_PlayerData.StamChargeRate / 10f;
               if (currentStam > m_PlayerData.maxStam) currentStam = m_PlayerData.maxStam;
               PlayerStaminaChangeEvent?.Invoke();
               yield return new WaitForSeconds(.1f);
          }
          isStamRecharging = false;
     }
}
