using UnityEngine;

public class EnemyAI : MonoBehaviour
{
     [SerializeField]
     public EnemyData Data;
     public Scan ScanBehavior;
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

     private void Awake()
     {
          ScanBehavior = new Scan(Data);
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
                    ScanBehavior.ScanTick(transform);
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
}