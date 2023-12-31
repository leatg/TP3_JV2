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

    private NavMeshAgent _agent;
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
        _agent = GetComponent<NavMeshAgent>();
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
    private EnemyAnimator Enemy2Animation { get; set; }
    private Vector3[] Destinations { get; set; }
    private NavMeshAgent Agent { get; set; }
    private int CurrentDestinationID { get; set; }
    private Transform Player { get; set; } // Assuming the player is assigned to this variable
    private float DetectionDistance { get; set; } = 6f;
    private float DetectionAngle { get; set; } = 30f;
    private float DefaultSpeed { get; set; }
    private float IncreasedSpeed { get; set; } = 8f;
    public Patrol(Vector3[] destinations, NavMeshAgent agent, Transform player)
    {
        //joueur
        Player = player;
        // agent
        Agent = agent;
        Destinations = destinations;
        //attaque
        DefaultSpeed = 2f;
        //animation
        Enemy2Animation = Agent.GetComponentInChildren<EnemyAnimator>();
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
        float angleToPlayer = Vector3.Angle(Agent.transform.forward, directionToPlayer.normalized);

        if (angleToPlayer < DetectionAngle && directionToPlayer.magnitude < DetectionDistance)
        {
            Debug.Log("Patrol: Player detected in front. Stopping patrol.");
            Agent.speed = IncreasedSpeed;
            Enemy2Animation.IsAttacking(true);
            return TaskState.Success;
        }
        else
        {
            if(Agent.speed != DefaultSpeed)
                Agent.speed = DefaultSpeed;
            Enemy2Animation.IsAttacking(false);
        }
        Agent.destination = currentDestination;

        // Debug.Log($"Patrol: Current position: {Agent.transform.position}, Destination: {currentDestination}");
        Vector3 t_agent = Agent.transform.position;
        float distance = Vector3.Distance(new Vector3(currentDestination.x, 0, currentDestination.z), new Vector3(t_agent.x, 0, t_agent.z));
        if ((distance - Agent.stoppingDistance) < 0.1f)
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