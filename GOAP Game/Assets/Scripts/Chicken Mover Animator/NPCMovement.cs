using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public void MoveTo(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public bool ReachedDestination()
    {
        return !agent.pathPending &&
               agent.remainingDistance <= agent.stoppingDistance;
    }

    public void UpdateAnim()
    {
        Vector3 vel = agent.velocity;
        float speed = vel.magnitude;

        animator.SetFloat("Vert", speed);
    }
}