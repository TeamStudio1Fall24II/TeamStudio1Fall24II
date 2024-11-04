using UnityEngine;
using UnityEngine.AI;
using static EnemyDataSO;

public class Combat : EnemyAIBehavior
{
     public GameObject Target;
     private CombatData data;
     private float fireTimer = 0.0f;
     private ProjectileLauncher launcher;

     private enum CombatState
     {
          ClosingIn,
          MaintainDistance
     }

     private CombatState currentCombatState = CombatState.ClosingIn;

     public Combat(EnemyDataSO enemyDataSO, NavMeshAgent agent, GameObject go, GameObject targetGO) : base(enemyDataSO, agent, go)
     {
          data = enemyDataSO.m_CombatData;
          launcher = enemyGO.GetComponentInChildren<ProjectileLauncher>();
          Target = targetGO;
     }

     public override void Tick()
     {
          switch(currentCombatState)
          {
               case CombatState.ClosingIn:
                    // Chase
                    navMeshAgent.SetDestination(Target.transform.position);

                    // If we are within set distance
                    if(Vector3.Distance(Target.transform.position, enemyGO.transform.position) < data.MaintainDistance)
                    {
                         currentCombatState = CombatState.MaintainDistance;
                    }
                    break;

               case CombatState.MaintainDistance:
                    // TODO: keep distance as player moves
                    navMeshAgent.isStopped = true;

                    // Aim
                    // TODO: add accuracy variable and account profectile drop over distance
                    transform.LookAt(Target.transform.position);
                    launcher.gameObject.transform.LookAt(Target.transform.position);
                    
                    // Fire
                    fireTimer += Time.deltaTime;
                    if(fireTimer > data.FireCooldown)
                    {
                         launcher.Launch();
                         fireTimer = 0.0f;
                    }

                    // Chase
                    if (Vector3.Distance(Target.transform.position, enemyGO.transform.position) > data.MaintainDistance)
                    {
                         currentCombatState = CombatState.ClosingIn;
                         navMeshAgent.isStopped = false;
                    }
                    break;
          }
     }

}
