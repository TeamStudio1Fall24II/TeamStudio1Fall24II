using UnityEngine;

public class PlayerDamageSounds : SFXMaker
{
    [SerializeField] Player player;
    int currentHealth;


    private void Start()
    {
        player.PlayerHealthChangeEvent += PlayerDamage;
    }

    public void PlayerDamage()
    {
        if(player.currentHealth < currentHealth)
        {
            base.MakeSound();
            currentHealth = player.currentHealth;
        }
    }
}
