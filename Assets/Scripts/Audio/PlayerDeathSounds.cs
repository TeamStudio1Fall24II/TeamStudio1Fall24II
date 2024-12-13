using UnityEngine;

public class PlayerDeathSounds : SFXMaker
{
    [SerializeField] Player player;

    private void Start()
    {
        player.PlayerDeathEvent += PlayerDie;
    }

    public void PlayerDie()
    {
        base.MakeSound();
    }
}
