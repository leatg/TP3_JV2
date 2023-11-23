using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BehaviorTree : MonoBehaviour
{
    [SerializeField]
    Transform[] patrolDestinations;
    [SerializeField]
    NavMeshAgent agent;

    [SerializeField] private Transform player;

    private BehaviorTreeTask behaviorTree;

    void Start()
    {
        Vector3[] destinations = patrolDestinations.Select(t => t.position).ToArray();

        List<TaskBT> tasks = new List<TaskBT>
        {
            new Patrol(destinations, agent, player),
            new Wait(2f),
            // Add more tasks as needed
        };

        behaviorTree = new BehaviorTreeTask(tasks);
    }

    void Update()
    {
        behaviorTree.Execute();
    }
}
public class BehaviorTreeTask
{
    private List<TaskBT> tasks;

    public BehaviorTreeTask(List<TaskBT> tasks)
    {
        this.tasks = tasks;
    }

    public void Execute()
    {
        foreach (var task in tasks)
        {
            TaskState state = task.Execute();
            if (state == TaskState.Failure)
            {
                Debug.LogError("BehaviorTree: Task failed!");
                return;
            }
            
            if (state == TaskState.Success)
            {
                Debug.Log("BehaviorTree: Task succeeded!");
            }
        }
    }
}
public class Patrol : TaskBT
{
    private Vector3[] Destinations { get; set; }
    private NavMeshAgent Agent { get; set; }
    private int CurrentDestinationID { get; set; }
    private Transform Player { get; set; } // Assuming the player is assigned to this variable
    private float DetectionDistance { get; set; } = 5f;

    public Patrol(Vector3[] destinations, NavMeshAgent agent, Transform player)
    {
        Destinations = destinations;
        Agent = agent;
        Player = player;
    }

    public override TaskState Execute()
    {
        if (Destinations == null || Destinations.Length == 0)
        {
            Debug.LogError("Patrol: No destinations specified!");
            return TaskState.Failure;
        }

        Vector3 currentDestination = Destinations[CurrentDestinationID];

        // Check if the player is in front of the agent at a distance of DetectionDistance
        Vector3 directionToPlayer = Player.position - Agent.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(Agent.transform.position, directionToPlayer.normalized, out hit, DetectionDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Patrol: Player detected in front. Stopping patrol.");
                return TaskState.Success;
            }
        }

        Agent.destination = currentDestination;

        // Debug.Log($"Patrol: Current position: {Agent.transform.position}, Destination: {currentDestination}");
        Vector3 t_agent = Agent.transform.position;
        float distance = Vector3.Distance(new Vector3(currentDestination.x, 0, currentDestination.z), new Vector3(t_agent.x, 0, t_agent.z));
        if (distance < Agent.stoppingDistance)
        {
            CurrentDestinationID = (CurrentDestinationID + 1) % Destinations.Length;
            Debug.Log("Patrol: Reached destination, moving to the next one.");
            return TaskState.Success;
        }

        return TaskState.Running;
    }
}



public class Wait : TaskBT
{
    private float ElapsedTime { get; set; } = 0;
    private float SecondsToWait { get; set; } = 0;

    public Wait(float secondsToWait) => SecondsToWait = secondsToWait;

    public override TaskState Execute()
    {
        ElapsedTime += Time.deltaTime;

        if (ElapsedTime > SecondsToWait)
        {
            ElapsedTime = 0;
            return TaskState.Success;
        }

        return TaskState.Running;
    }
}