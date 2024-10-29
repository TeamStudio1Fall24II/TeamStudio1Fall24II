using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy Data")]
[Serializable]
public class EnemyData : ScriptableObject
{
     [SerializeField]
     public ScanData m_ScanData;

     [Serializable]
     public class ScanData
     {
          public float ScanAngle = 45f;
          public float scanSpeed = 1f;
          public float scanPauseLength = 2f;
     }
}