using UnityEngine;

public class DeathAudio : SFXMaker
{
    [SerializeField] EnemyAI myEnemy = null;
    [SerializeField] bool hasDied = false;

    private void Start()
    {
        myEnemy.DeathEvent += Die;
    }

    private void OnDisable()
    {
        myEnemy.DeathEvent -= Die;
    }

    public void Die(GameObject newlyDeceased)
    {
        if (!hasDied)
        {
            hasDied = true;
            base.MakeSound();
            //Debug.Log("Dying");
            myEnemy.DeathEvent -= Die;
        }
    }
}
