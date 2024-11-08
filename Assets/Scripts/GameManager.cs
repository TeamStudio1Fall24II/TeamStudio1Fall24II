using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     public static GameManager Instance;

     public List<Level> Levels = new List<Level>();

     [SerializeField] private Level currentLevel;

     private void Awake()
     {
          if (Instance != null && Instance != this)
          {
               Destroy(gameObject);
               return;
          }

          Instance = this;
          DontDestroyOnLoad(gameObject);

     }

     private void Start()
     {
          foreach(Level level in Levels)
          {
               level.LevelCompleteEvent += OnLevelComplete;
          }
     }

     private void OnLevelComplete(Level level)
     {
          Debug.Log("Level Complete!");
     }
}
