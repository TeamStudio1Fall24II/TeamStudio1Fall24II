using UnityEngine;
using static EnemyData;

public class Scan
{
     private EnemyData EnemyDataSO;
     private ScanData Data;

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

     public Scan(EnemyData initData)
     {
          EnemyDataSO = initData;
          Data = EnemyDataSO.m_ScanData;
          scanPauseTimer = Data.scanPauseLength;
     }

     public void ScanTick(Transform transform)
     {
          switch (currentScanState)
          {
               case ScanState.ScanningLeft:
                    rotateAmount = Data.scanSpeed * Time.deltaTime;
                    totalRotation += rotateAmount;
                    transform.Rotate(Vector3.up, -rotateAmount);
                    if (totalRotation >= Data.ScanAngle)
                    {
                         prevScanState = currentScanState;
                         currentScanState = ScanState.Paused;
                         scanPauseTimer = 0f;
                    }
                    break;
               case ScanState.ScanningRight:
                    rotateAmount = Data.scanSpeed * Time.deltaTime;
                    totalRotation += rotateAmount;
                    transform.Rotate(Vector3.up, rotateAmount);
                    if (totalRotation >= Data.ScanAngle)
                    {
                         prevScanState = currentScanState;
                         currentScanState = ScanState.Paused;
                         scanPauseTimer = 0f;
                    }
                    break;
               case ScanState.Paused:
                    scanPauseTimer += Time.deltaTime;
                    if (scanPauseTimer >= Data.scanPauseLength)
                    {
                         currentScanState = prevScanState == ScanState.ScanningLeft ? ScanState.ScanningRight : ScanState.ScanningLeft;
                         scanPauseTimer = 0f;
                         totalRotation = 0f;
                    }
                    break;
          }
     }

}
