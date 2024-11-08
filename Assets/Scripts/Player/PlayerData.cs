using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data")]
[Serializable]
public class PlayerData : ScriptableObject
{
     public int MaxHealth = 10;
}
