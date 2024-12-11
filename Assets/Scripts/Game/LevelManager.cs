using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
     public string LevelTitle = "New Level";
     public List<GameObject> Enemies = new List<GameObject>();
     public int EnemyCount => Enemies.Count;
     public bool isEnemiesCleared => EnemyCount == 0;
     public UnityAction<LevelManager> LevelCompleteEvent;

     public InteractableDoor LevelDoor;
     public InteractableDoor LevelStartDoor;

     public CompletionZone LevelCompletionZone;
     public Transform StartPoint;

     public float UnlockStartDoorDelay = 3.0f;

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
          Debug.Log("Level " + LevelTitle + " Complete!!!");
          LevelCompleteEvent?.Invoke(this);
          // Wrap up level as game manager transitions
     }

     // TODO: Probably better way than teleporting to start next level
     // Maybe an animation and a timer for door unlocks.
     public void StartLevelSequence()
     {
          LevelStartDoor.isLocked = true;
          GameObject player = GameObject.Find("Player");
          player.GetComponent<CharacterController>().enabled = false;
          player.transform.position = StartPoint.position;
          player.transform.Rotate(0, 90, 0);
          player.GetComponent<CharacterController>().enabled = true;
          StartCoroutine(SlowUnlockDoor(UnlockStartDoorDelay));
     }

     private IEnumerator SlowUnlockDoor(float seconds)
     {
          yield return new WaitForSeconds(seconds);
          LevelStartDoor.isLocked = false;
     }
}