using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Level : MonoBehaviour
{ 
     public List<GameObject> Enemies = new List<GameObject>();
     public int EnemyCount => Enemies.Count;
     public bool isEnemiesCleared => EnemyCount == 0;
     public UnityAction LevelCompleteEvent;


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
               if(isEnemiesCleared)
               {
                    LevelCompleteEvent?.Invoke();
               }
          }
     }
}