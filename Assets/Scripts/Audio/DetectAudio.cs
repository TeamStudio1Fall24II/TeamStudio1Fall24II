using UnityEngine;

public class DetectAudio : SFXMaker
{
    [SerializeField] EnemyAI myEnemy = null;

    private void Start()
    {
        myEnemy.PlayerDetectEvent += Detected;
    }

    private void OnDisable()
    {
        myEnemy.PlayerDetectEvent -= Detected;
    }

    public void Detected()
    {
        //Debug.Log("Detected");
        base.MakeSound();
    }
}
