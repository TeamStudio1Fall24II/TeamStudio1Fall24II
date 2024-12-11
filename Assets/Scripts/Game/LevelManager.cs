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

     public PlayerTriggerZone LevelCompletionZone;
     public PlayerTriggerZone LevelStartZone;
     public Transform StartPoint;
     public GameObject InvisibleBlock;

     public float UnlockStartDoorDelay = 3.0f;

     private void Awake()
     {
          LevelCompletionZone.PlayerEnteredTriggerZone += OnCompletionZone;
          if(LevelStartZone != null)
               LevelStartZone.PlayerEnteredTriggerZone += OnStartZone;

          foreach (GameObject enemyGO in Enemies)
          {
               enemyGO.GetComponent<EnemyAI>().DeathEvent += RemoveEnemy;
          }
     }

     private void OnDestroy()
     {
          LevelCompletionZone.PlayerEnteredTriggerZone -= OnCompletionZone;
          if (LevelStartZone != null)
               LevelStartZone.PlayerEnteredTriggerZone -= OnStartZone;
          foreach (GameObject enemyGO in Enemies)
          {
               enemyGO.GetComponent<EnemyAI>().DeathEvent -= RemoveEnemy;
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
                    LevelCompletionZone.gameObject.SetActive(true);
                    if(InvisibleBlock!= null)
                         InvisibleBlock?.gameObject.SetActive(false);
               }
          }
     }

     private void OnStartZone()
     {
          LevelStartDoor.ForceInteract();
          LevelStartZone.gameObject.SetActive(false);
          if (InvisibleBlock != null)
               InvisibleBlock?.SetActive(true);
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
          StartCoroutine(SlowOpenDoor(UnlockStartDoorDelay));
     }

     private IEnumerator SlowOpenDoor(float seconds)
     {
          yield return new WaitForSeconds(seconds);
          LevelStartDoor.ForceInteract();
     }
}