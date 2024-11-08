using UnityEngine;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
     public Player Player;

     public HealthBar healthBar;
     public Image StamFill;

     private void Awake()
     {
          Player.PlayerHealthChangeEvent += OnPlayerHealthChange;
          Player.PlayerStaminaChangeEvent += OnPlayerStaminaChange;
     }

     private void OnDisable()
     {
          Player.PlayerHealthChangeEvent -= OnPlayerHealthChange;
          Player.PlayerStaminaChangeEvent -= OnPlayerStaminaChange;
     }

     private void Start()
     {
          healthBar.SetMaxHealth(Player.m_PlayerData.MaxHealth);
     }

     private void OnPlayerHealthChange()
     {
          healthBar.SetHealth(Player.currentHealth);
     }
     private void OnPlayerStaminaChange()
     {
          StamFill.fillAmount = Player.currentStam / Player.m_PlayerData.maxStam;
     }
}
