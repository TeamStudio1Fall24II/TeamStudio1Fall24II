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

     // Need a zone/collider that player enters.
     // On trigger enter, trigger event that player entered zone
     // Level Manager listens to that event to trigger Level completed event
     // game manager listens to level completed event to start level transition

     private void Awake()
     { 
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
}