using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{ 
     public List<GameObject> Enemies = new List<GameObject>();
     public int EnemyCount => Enemies.Count;
     public bool isEnemiesCleared => EnemyCount == 0;
     public UnityAction<LevelManager> LevelCompleteEvent;

     public InteractableDoor LevelDoor;

     public CompletionZone LevelCompletionZone;

     private void Awake()
     {
          LevelCompletionZone.PlayerEnteredCompletionZone += OnCompletionZone;

          foreach(GameObject enemyGO in Enemies)
          {
               enemyGO.GetComponent<EnemyAI>().DeathEvent += RemoveEnemy;
          }
     }
     public void AddEnemy(GameObject enemy)
     {
          if(!Enemies.Contains(enemy) && enemy.CompareTag("EnemyAI"))
          {
               Enemies.Add(enemy);
          }
     }

     public void RemoveEnemy(GameObject enemy)
     {
          if(Enemies.Contains(enemy))
          {
               Enemies.Remove(enemy);
               if (isEnemiesCleared)
               {
                    LevelDoor.isLocked = false;
               }
          }
     }

     private void OnCompletionZone()
     {
          Debug.Log("Level Complete!");
          LevelCompleteEvent?.Invoke(this);
     }
}