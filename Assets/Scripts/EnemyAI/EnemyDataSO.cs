using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy Data")]
[Serializable]
public class EnemyDataSO : ScriptableObject
{
     [SerializeField]
     public ScanData m_ScanData;
     [SerializeField]
     public CombatData m_CombatData;

     [Serializable]
     public class ScanData
     {
          public float ScanAngle = 45f;
          public float scanSpeed = 1f;
          public float scanPauseLength = 2f;
     }

     [Serializable]
     public class CombatData
     {
          public float CombatRunSpeed = 5.0f;
          public float FireCooldown = 1.0f;
          public float MaintainDistance = 8.0f;
          public float LineOfSightDistance = 20.0f;

          // Avoidance
          public float avoidanceForce = 2f;
          public float spreadForce = 2f;
          public float avoidanceRadius = 2.0f;
          public float spreadRadius = 2.0f;
          public float PlayerAvoidanceForce = 0.5f;
          public float PlayerAvoidanceRadius = 1.5f;
     }
}