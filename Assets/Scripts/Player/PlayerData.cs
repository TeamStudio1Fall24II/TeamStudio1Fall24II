using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data")]
[Serializable]
public class PlayerData : ScriptableObject
{
     public int MaxHealth = 100;
     public float maxStam = 100;
     public float RunCost = 1;
     public float StamChargeRate = 1;
     public int HealthChargeRate = 1;
}
