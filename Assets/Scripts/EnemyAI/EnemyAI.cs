using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
     [SerializeField]
     public EnemyDataSO Data;
     public EnemyAIBehavior CurrentBehavior;
     public Scan ScanBehavior;
     public Combat CombatBehavior;
     private NavMeshAgent navMeshAgent;

     // TODO: targets should be acquired through detection
     // For now, just set the target in the editor.
     public GameObject target;

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
          navMeshAgent = GetComponent<NavMeshAgent>();

          ScanBehavior = new Scan(Data, navMeshAgent, gameObject);
          CombatBehavior = new Combat(Data, navMeshAgent, gameObject, target);
     }

     private void OnEnable()
     {
          CurrentState = DefaultState;
          switch (CurrentState)
          {
               case State.Idle:
                    // Do nothing
                    break;
               case State.Scan:
                    CurrentBehavior = ScanBehavior;
                    break;
               case State.Patrol:
                    break;
               case State.Investigate:
                    break;
               case State.Chase:
                    break;
               case State.Combat:
                    CurrentBehavior = CombatBehavior;
                    break;
               case State.Die:
                    break;
          }
     }

     // TODO: Eventually need to integrate animations
     // Update is called once per frame
     void Update()
    {
          CurrentBehavior.Tick();
    }
}