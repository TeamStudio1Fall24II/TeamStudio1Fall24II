using UnityEngine;

public class CombatAudio : SFXMaker, IDamageable
{
    public void TakeDamage(int damage)
    {
        Debug.Log("Taking Damage");
        base.MakeSound();
    }
}
