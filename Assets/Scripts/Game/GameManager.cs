using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
     public static GameManager Instance;

     public List<LevelManager> Levels = new List<LevelManager>();
     [SerializeField] private List<LevelManager> completedLevels = new List<LevelManager>();

     [SerializeField] private LevelManager currentLevel;
     private int currentLevelIndex = 0;

     private void Awake()
     {
          if (Instance != null && Instance != this)
          {
               Destroy(gameObject);
               return;
          }

          Instance = this;
          //DontDestroyOnLoad(gameObject);

     }

     private void Start()
     {
          foreach(LevelManager level in Levels)
          {
               level.LevelCompleteEvent += OnLevelComplete;
          }
     }

     private void OnLevelComplete(LevelManager level)
     {
          if(Levels.Contains(level))
          {
               completedLevels.Add(level);
               Levels.Remove(level);
          }
          if(Levels.Count == 0)
          {
               Debug.Log("Game Finished!!!");
               // End game Sequence
               return;
          }
          // Level start sequence/ transition
          currentLevel = Levels[0];
          Debug.Log("Starting level: " + currentLevel.LevelTitle + "\nGood Luck...");

          currentLevel.StartLevelSequence();
     }
}
