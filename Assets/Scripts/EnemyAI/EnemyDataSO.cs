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
     [SerializeField]
     public FOVData m_FOVData;
     [SerializeField]
     public HealthData m_HealthData;

     [Serializable]
     public class ScanData
     {
          public float ScanAngle = 45f;
          public float ScanSpeed = 1f;
          public float ScanPauseLength = 2f;
     }

     [Serializable]
     public class CombatData
     {
          public float CombatRunSpeed = 5.0f;
          public float FireCooldown = 1.0f;
          public float MaintainDistance = 8.0f;
          public float LineOfSightDistance = 20.0f;

          // Avoidance
          public float AvoidanceForce = 2f;
          public float SpreadForce = 2f;
          public float AvoidanceRadius = 2.0f;
          public float SpreadRadius = 2.0f;
          public float PlayerAvoidanceForce = 0.5f;
          public float PlayerAvoidanceRadius = 1.5f;
     }

     [Serializable]
     public class FOVData
     {
          public float FieldOfViewAngle = 110f;
          public float ViewDistance = 10f;
          public float RaycastTime = 0.2f;
     }

     [Serializable]
     public class HealthData
     {
          public int MaxHealth = 5;
     }
}