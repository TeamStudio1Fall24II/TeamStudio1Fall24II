using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using static EnemyDataSO;

public class Combat : EnemyAIBehavior
{
     public GameObject Target;
     private CombatData data;
     private float fireTimer = 0.0f;
     private ProjectileLauncher launcher;

     private bool isWithinRange => Vector3.Distance(Target.transform.position, enemyGO.transform.position) < data.MaintainDistance;
     private bool isBeyondMinRange => Vector3.Distance(Target.transform.position, enemyGO.transform.position) > data.PlayerAvoidanceRadius;
     private bool hasLineOfSight = false;
     private float LOSRaycastDelay = 0.2f;
     private float LOSTimer = 0.0f;

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
          DelayCheckLineOfSight();
          switch (currentCombatState)
          {
               case CombatState.ClosingIn:
                    CloseDistance();
                    break;

               case CombatState.MaintainDistance:
                    MaintainDistance();

                    Avoidance();
                    Spreading();

                    Aim();
                    Fire();

                    Chase();
                    break;
          }
     }

     // Avoidance code partially generated with Gemini AI
     private void Avoidance()
     {
          // Avoidance
          Collider[] colliders = Physics.OverlapSphere(transform.position, data.avoidanceRadius);
          foreach (Collider collider in colliders)
          {
               if (collider.gameObject != enemyGO)
               {
                    NavMeshAgent otherAgent = collider.GetComponent<NavMeshAgent>();
                    if (otherAgent != null)
                    {
                         Vector3 directionAway = (transform.position - otherAgent.transform.position).normalized;
                         navMeshAgent.velocity += directionAway * data.avoidanceForce;
                    }
               }
          }
     }
     // Spreading code partially generated with Gemini AI
     private void Spreading()
     {
          // Spreading
          Collider[] obstructingColliders = Physics.OverlapSphere(transform.position, data.spreadRadius);
          foreach (Collider collider in obstructingColliders)
          {
               if (collider.gameObject != enemyGO && collider.CompareTag("EnemyAI"))
               {
                    Vector3 spreadDirection = (transform.position - collider.transform.position).normalized;
                    navMeshAgent.velocity += spreadDirection * data.spreadForce;
               }
          }
     }

     private void CloseDistance()
     {
          navMeshAgent.SetDestination(Target.transform.position);

          if (isWithinRange && hasLineOfSight)
          {
               currentCombatState = CombatState.MaintainDistance;
          }
     }

     private void MaintainDistance()
     {
          navMeshAgent.isStopped = true;
          if(!isBeyondMinRange)
          {
               Vector3 directionAway = (transform.position - Target.transform.position).normalized;
               navMeshAgent.velocity += directionAway * data.PlayerAvoidanceForce;
          }
     }

     // TODO: add accuracy variable and account profectile drop over distance
     private void Aim()
     {
          transform.LookAt(Target.transform.position);
          launcher.gameObject.transform.LookAt(Target.transform.position);
     }

     private void Fire()
     {
          fireTimer += Time.deltaTime;
          if (fireTimer > data.FireCooldown)
          {
               launcher.Launch();
               fireTimer = 0.0f;
          }
     }
     private void Chase()
     {
          if (!isWithinRange || !hasLineOfSight)
          {
               currentCombatState = CombatState.ClosingIn;
               navMeshAgent.isStopped = false;
          }
     }

     private void DelayCheckLineOfSight()
     {
          LOSTimer += Time.deltaTime;
          if(LOSTimer > LOSRaycastDelay)
          {
               hasLineOfSight = HasLineOfSight();
               LOSTimer = 0f;
          }
     }
     private bool HasLineOfSight()
     {
          Vector3 directionToPlayer = Target.transform.position - transform.position;

          RaycastHit hit;
          return Physics.Raycast(transform.position, directionToPlayer, out hit, data.LineOfSightDistance)
                 && hit.collider.gameObject == Target.transform.gameObject;
     }
}
