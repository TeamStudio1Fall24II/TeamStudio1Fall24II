using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
     public Player Player;

     public HealthBar healthBar;
     public Image StamFill;
     public TextMeshProUGUI ammoDisplay;

     private void Awake()
     {
          Player.PlayerHealthChangeEvent += OnPlayerHealthChange;
          Player.PlayerStaminaChangeEvent += OnPlayerStaminaChange;
     }

     private void OnDisable()
     {
          Player.PlayerHealthChangeEvent -= OnPlayerHealthChange;
          Player.PlayerStaminaChangeEvent -= OnPlayerStaminaChange;
          Player.m_Controller.launcher.AmmoChangeEvent -= OnAmmoChange;
     }

     private void Start()
     {
          // Needs to wait for Player to initialize its controller
          Player.m_Controller.launcher.AmmoChangeEvent += OnAmmoChange; 
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

     private void OnAmmoChange() 
     {
          ammoDisplay.text = Player.m_Controller.launcher.CurrentAmmo.ToString();
     }
}
