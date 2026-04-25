using GOAP;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class NPCPathing : MonoBehaviour
{
    [Header("Location Management")]
    [SerializeField]
    LocationInstance currentLocation; // Use this for tracking current location
    [SerializeField] G_AtLocation locationTrackingStateRef;
    G_AtLocation locationTrackingState;

    [Header("Pathfinding")]
    [SerializeField] bool useNavmesh = true;

    [Header("Navmesh")]
    NavMeshAgent agent;
    public NavMeshAgent Agent
    {
        get {  return agent; }
    }

    NPCGOAPHandler worldState;


    [Header("Alternate Pathfinding")]
    AIPath altPathfinder;

    #region Setup
    public void Init(NPCGOAPHandler worldState)
    {
        this.worldState = worldState;
        if (locationTrackingStateRef != null)
        {
            AssignLocationTrackingState();
        }
        if (useNavmesh)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        else
        {
            altPathfinder = GetComponent<AIPath>();
        }
        
    }
    void AssignLocationTrackingState()
    {
        G_AtLocation tempState = this.worldState.GetLocalWorldState().FindState(locationTrackingStateRef) as G_AtLocation;
        if (tempState != null)
        {
            locationTrackingState = tempState;
        }
    }
    #endregion

    #region Pathing

    public void StartPath(Vector3 destination)
    {
        if (useNavmesh)
        {
            agent.SetDestination(destination);
        }
        else
        {
            altPathfinder.destination = destination;
        }
       
    }

    public void StartPath(LocationInstance locationInstance)
    {
        if (useNavmesh)
        {
            agent.SetDestination(locationInstance.GetAccesPoint());
        }
        else
        {
            altPathfinder.destination = locationInstance.GetAccesPoint();
        }
    }

    public bool IsAtLocation(LocationInstance location)
    {
        return currentLocation == location;
    }

    public bool IsAtLocationType(LocationType type)
    {
        return currentLocation != null ?
            currentLocation.GetLocationType() == type
            : type == null;
    }

    #endregion

    #region Location Entry and Exit
    void EnterLocation(LocationInstance location)
    {
        if(currentLocation != location)
        {
            SetLocation(location);
        }
    }

    void ExitCurrentLocation(LocationInstance location)
    {
        if (currentLocation == location)
        {
            SetLocation(null);
        }
    }

    void SetLocation(LocationInstance location)
    {
        currentLocation = location;
        if(currentLocation != null)
        {
            locationTrackingState.SetValue(currentLocation.GetLocationType());
        }
        else
        {
            locationTrackingState.SetValue(null);
        }
        
    }

    #endregion

    #region Trigger Handling

    private void OnTriggerEnter(Collider other)
    {
        HandleAreaEntryAndExit(other.gameObject, true);
    }

    private void OnTriggerExit(Collider other)
    {
        HandleAreaEntryAndExit(other.gameObject, false);
    }

    void HandleAreaEntryAndExit(GameObject obj, bool entry)
    {
        if (obj.tag == "location")
        {
            LocationArea area = obj.GetComponent<LocationArea>();
            if (area != null)
            {
                if (entry)
                {
                    EnterLocation(area.GetPaarentInstance());
                }
                else
                {
                    ExitCurrentLocation(area.GetPaarentInstance());
                }
            }
        }
            
    }

    #endregion
}
