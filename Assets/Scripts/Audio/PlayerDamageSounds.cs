using UnityEngine;

public class PlayerDamageSounds : SFXMaker
{
    [SerializeField] Player player;
    int currentHealth;


    private void Start()
    {
        player.PlayerHealthChangeEvent += PlayerDamage;
        currentHealth = player.currentHealth;
    }

    private void OnDisable()
    {
        player.PlayerHealthChangeEvent -= PlayerDamage;
    }

    public void PlayerDamage()
    {
        //Debug.Log("Player Damage: " + "Last hp: " + currentHealth + ", Current hp:" + player.currentHealth);
        if (player.currentHealth <= 0f) { return; }
        if (player.currentHealth < currentHealth)
        {
            //Debug.Log("Player  took   damage");
            base.MakeSound();
        }
        currentHealth = player.currentHealth;
    }
}
