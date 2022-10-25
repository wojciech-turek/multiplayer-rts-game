using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : NetworkBehaviour
{
    private NavMeshAgent agent = null;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


#region Server

    [ServerCallback]
    private void Update()
    {
        if (!agent.hasPath) return;
        if (agent.remainingDistance > agent.stoppingDistance) return;
        agent.ResetPath();
    }

    [Command]
    public void CmdMove(Vector3 position)
    {
        if (
            !NavMesh
                .SamplePosition(position,
                out NavMeshHit hit,
                1f,
                NavMesh.AllAreas)
        )
        {
            return;
        }

        agent.SetDestination(hit.position);
    }


#endregion

}
