using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
     public Player Player;
     public GameManager GameManager;

     public HealthBar healthBar;
     public Image StamFill;
     public TextMeshProUGUI ammoDisplay;
     public PauseMenu pauseMenu;
     public DeathMenu deathMenu;
     public VictoryMenu victoryMenu;

     public bool isPaused = false;

     private void Awake()
     {
          Player.PlayerHealthChangeEvent += OnPlayerHealthChange;
          Player.PlayerDeathEvent += OnPlayerDeath;
          Player.PlayerStaminaChangeEvent += OnPlayerStaminaChange;
          Player.m_Controller.StartPressed += OnStartPressed;
          GameManager.VictoryEvent += OnVictory;
     }

     private void OnDisable()
     {
          Player.PlayerHealthChangeEvent -= OnPlayerHealthChange;
          Player.PlayerDeathEvent -= OnPlayerDeath;
          Player.PlayerStaminaChangeEvent -= OnPlayerStaminaChange;
          Player.m_Controller.launcher.AmmoChangeEvent -= OnAmmoChange;
          Player.m_Controller.StartPressed -= OnStartPressed;
          GameManager.VictoryEvent -= OnVictory;
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

     private void OnStartPressed()
     {
          pauseMenu.TogglePause();
     }

     private void OnPlayerDeath()
     {
          deathMenu.OpenDeathMenu();
     }

    private void OnVictory()
    {
        victoryMenu.OpenVictoryMenu();
    }
}
