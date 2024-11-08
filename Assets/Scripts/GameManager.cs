using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     public static GameManager Instance;

     public List<LevelManager> Levels = new List<LevelManager>();

     [SerializeField] private LevelManager currentLevel;

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
          foreach(LevelManager level in Levels)
          {
               level.LevelCompleteEvent += OnLevelComplete;
          }
     }

     private void OnLevelComplete(LevelManager level)
     {
          Debug.Log("Level Complete!");
     }
}
