using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAIBehavior
{
     protected EnemyDataSO enemyDataSO;
     protected NavMeshAgent navMeshAgent;
     protected GameObject enemyGO;
     protected Transform transform;
     
     public EnemyAIBehavior(EnemyDataSO data, NavMeshAgent agent, GameObject go)
     {
          enemyDataSO = data;
          navMeshAgent = agent;
          enemyGO = go;
          transform = enemyGO.transform;
     }

     public abstract void Tick();
}
