using UnityEngine;
using UnityEngine.AI;
using static EnemyDataSO;

public class Scan : EnemyAIBehavior
{
     private ScanData data;

     private float scanPauseTimer;
     private float rotateAmount;
     private float totalRotation;

     private enum ScanState
     {
          ScanningLeft,
          ScanningRight,
          Paused
     }
     private ScanState currentScanState { get; set; } = ScanState.ScanningLeft;
     private ScanState prevScanState;

     public Scan(EnemyDataSO enemyDataSO, NavMeshAgent agent, GameObject go) : base(enemyDataSO, agent, go)
     {
          data = enemyDataSO.m_ScanData;
          scanPauseTimer = data.ScanPauseLength;
     }

     public override void Tick()
     {
          switch (currentScanState)
          {
               case ScanState.ScanningLeft:
                    rotateAmount = data.ScanSpeed * Time.deltaTime;
                    totalRotation += rotateAmount;
                    transform.Rotate(Vector3.up, -rotateAmount);
                    if (totalRotation >= data.ScanAngle)
                    {
                         prevScanState = currentScanState;
                         currentScanState = ScanState.Paused;
                         scanPauseTimer = 0f;
                    }
                    break;
               case ScanState.ScanningRight:
                    rotateAmount = data.ScanSpeed * Time.deltaTime;
                    totalRotation += rotateAmount;
                    transform.Rotate(Vector3.up, rotateAmount);
                    if (totalRotation >= data.ScanAngle)
                    {
                         prevScanState = currentScanState;
                         currentScanState = ScanState.Paused;
                         scanPauseTimer = 0f;
                    }
                    break;
               case ScanState.Paused:
                    scanPauseTimer += Time.deltaTime;
                    if (scanPauseTimer >= data.ScanPauseLength)
                    {
                         currentScanState = prevScanState == ScanState.ScanningLeft ? ScanState.ScanningRight : ScanState.ScanningLeft;
                         scanPauseTimer = 0f;
                         totalRotation = 0f;
                    }
                    break;
          }
     }

}
