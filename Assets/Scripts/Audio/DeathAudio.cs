using UnityEngine;

public class DeathAudio : SFXMaker
{
    [SerializeField] EnemyAI myEnemy = null;

    private void Start()
    {
        myEnemy.DeathEvent += Die;
    }

    private void OnDisable()
    {
        myEnemy.DeathEvent -= Die;
    }

    public void Die(GameObject dyer)
    {
        //Debug.Log("Dying");
        base.MakeSound();
    }
}
