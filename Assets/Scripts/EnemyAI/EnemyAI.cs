using UnityEngine;

public class EnemyAI : MonoBehaviour
{
     public enum State
     {
          Idle,
          Scan,
          Patrol,
          Investigate,
          Chase,
          Combat,
          Die
     }

     public State CurrentState;
     // Starts in this state and will return to this state if nothing else is going on.
     public State DefaultState;

     #region Scan variables
     [SerializeField]
     private float ScanAngle = 45f;

     private Vector3 startAngleVec;
     private Vector3 endAngleVec;
     [SerializeField]
     private float frequency;
     [SerializeField]
     private float scanPauseLength;
     private float scanPauseTimer;
     private bool isPaused = false;
     #endregion

     private void Awake()
     {
          startAngleVec = new Vector3(0.0F, ScanAngle, 0.0F);
          endAngleVec = new Vector3(0.0F, -ScanAngle, 0.0F);
          scanPauseTimer = scanPauseLength;
     }

     private void OnEnable()
     {
          CurrentState = DefaultState;
     }

     // TODO: Eventually need to integrate animations
     // Update is called once per frame
     void Update()
    {
        switch(CurrentState)
          {
               case State.Idle:
                    // Do nothing
                    break;
               case State.Scan:
                    ScanTick();
                    break;
               case State.Patrol:
                    break;
               case State.Investigate:
                    break;
               case State.Chase:
                    break;
               case State.Combat:
                    break;
               case State.Die:
                    break;
          }
    }

     private void ScanTick()
     {
          if (!isPaused)
          {
               Quaternion from = Quaternion.Euler(startAngleVec);
               Quaternion to = Quaternion.Euler(endAngleVec);

               // TODO: Time since startup does not work with pauses
               float lerp = 0.5F * (1.0F + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * frequency));
               transform.localRotation = Quaternion.Lerp(from, to, lerp);

               if (Mathf.Abs(transform.localRotation.eulerAngles.y) >= ScanAngle - 2)
               {
                    isPaused = true;
                    scanPauseTimer = scanPauseLength;
               }
          }
          else
          {
               scanPauseTimer -= Time.deltaTime;
               if(scanPauseTimer < 0.0f)
               {
                    isPaused = false;
               }
          }
     }
}
