using UnityEngine;

public class PlayerDeathSounds : SFXMaker
{
    [SerializeField] Player player;
    bool hasDied = false;
    private void Start()
    {
        player.PlayerDeathEvent += PlayerDie;
    }

    private void OnDisable()
    {
        player.PlayerDeathEvent -= PlayerDie;
    }

    public void PlayerDie()
    {
        if(!hasDied)
        {
            //Debug.Log("Player die");
            base.MakeSound();
            hasDied = true;
        }
    }
}
