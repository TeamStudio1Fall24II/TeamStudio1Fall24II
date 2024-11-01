using UnityEngine;
using UnityEngine.AI;

public class MovementAI : MonoBehaviour
{
     public GameObject target;
     public GameObject target2;
     public GameObject currentTarget;
     private NavMeshAgent m_navMeshAgent;

     public bool isChasingFirst = true;

     // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
    {
          m_navMeshAgent = GetComponent<NavMeshAgent>();
          m_navMeshAgent.SetDestination(target.transform.position);
          currentTarget = target;
     }

    // Update is called once per frame
    void Update()
    {
          if(target2 != null && Vector3.Distance(transform.position, currentTarget.transform.position) < m_navMeshAgent.stoppingDistance)
          {
               currentTarget = currentTarget == target ? target2 : target;
               isChasingFirst = !isChasingFirst;
          }

          m_navMeshAgent.SetDestination(isChasingFirst ? target.transform.position : target2.transform.position);

          if (m_navMeshAgent.isOnOffMeshLink)
          {
               Vector3 teleportPos = Vector3.Distance(transform.position, m_navMeshAgent.currentOffMeshLinkData.startPos) < 4.0 ?
                    m_navMeshAgent.currentOffMeshLinkData.endPos : m_navMeshAgent.currentOffMeshLinkData.startPos;

               // Teleport the agent to the target position
               m_navMeshAgent.Warp(teleportPos);
               m_navMeshAgent.CompleteOffMeshLink(); // Important to complete the link
          }
     }
}
