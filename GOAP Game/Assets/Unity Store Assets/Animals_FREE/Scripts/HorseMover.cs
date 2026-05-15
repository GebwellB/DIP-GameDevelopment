using UnityEngine;
using UnityEngine.AI;
using ithappy.Animals_FREE;

public class HorseMover : MonoBehaviour
{
    [Header("Waypoints (local space)")]
    [SerializeField] private Vector3[] points;

    [Header("World Offset")]
    [SerializeField] private Vector3 worldOffset = new Vector3(-7.41427f, 0f, 4.7394f);

    [Header("Settings")]
    [SerializeField] private float stopDistance = 1.5f;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private bool run = false;

    private CreatureMover creatureMover;
    private NavMeshAgent agent;

    private int index;
    private float waitTimer;

    private void Awake()
    {
        creatureMover = GetComponent<CreatureMover>();
        agent = GetComponent<NavMeshAgent>();

        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    private void Start()
    {
        if (points == null || points.Length == 0)
            return;

        agent.SetDestination(ToWorld(points[0]));
    }

    private void Update()
    {
        if (points == null || points.Length == 0)
            return;

        SyncAgent();

        Vector3 target = agent.steeringTarget;
        Vector3 toTarget = target - transform.position;
        toTarget.y = 0f;

        float distance = toTarget.magnitude;

        // ARRIVED AT POINT
        if (!agent.pathPending && distance <= stopDistance)
        {
            waitTimer += Time.deltaTime;

            creatureMover.SetInput(Vector2.zero, target, false, false);

            if (waitTimer >= waitTime)
            {
                index = (index + 1) % points.Length;

                agent.SetDestination(ToWorld(points[index]));

                waitTimer = 0f;
            }

            return;
        }

        // MOVE TOWARDS PATH
        toTarget.Normalize();

        Vector2 axis = new Vector2(toTarget.x, toTarget.z);
        axis = Vector2.ClampMagnitude(axis, 1f);

        creatureMover.SetInput(axis, target, run, false);
    }

    private Vector3 ToWorld(Vector3 localPoint)
    {
        return localPoint + worldOffset;
    }

    private void SyncAgent()
    {
        agent.nextPosition = transform.position;
    }
}