using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum TaskState { Running, Success, Failure }
public abstract class TaskBT 
{
    public abstract TaskState Execute();
}

public class TaskNode : Node
{
    protected List<TaskBT> Tasks { get; private set; } = new();
    private int CurrentTaskIndex { get; set; }

    public TaskNode(string tag, IEnumerable<TaskBT> tasks)
        :base(tag)
    {
        foreach (TaskBT task in tasks)
        {
            AddTask(task);
        }
    }

    public void AddTask(TaskBT task) => Tasks.Add(task);

    protected override NodeState InnerEvaluate()
    {
        bool executeNextTask = true;
        int taskCount = Tasks.Count;

        while (executeNextTask)
        {
            TaskBT currentTask = Tasks[CurrentTaskIndex];
            TaskState currentTaskState = currentTask.Execute();

            switch (currentTaskState)
            {
                case TaskState.Failure:
                    State = NodeState.Failure;
                    return State;
                case TaskState.Running:
                    executeNextTask = false;
                    break;
                case TaskState.Success:
                    if (CurrentTaskIndex == taskCount - 1)
                    {
                        CurrentTaskIndex = 0;
                        State = NodeState.Success;
                        return State;
                    }
                  
                    ++CurrentTaskIndex;
                    break;
            }
        }

        State = NodeState.Running;
        return State;
    }
}

public class DummyTask : TaskBT
{
    private TaskState ReturnState { get; set; }
    private string Message { get; set; }
    public DummyTask(string message, TaskState returnState)
    {
        Message = message;
        ReturnState = returnState;
    }
    public override TaskState Execute()
    {
        Debug.Log(Message);
        return ReturnState;
    }
}

// public class Wait : TaskBT
// {
//     private float ElapsedTime { get; set; } = 0;
//     private float SecondsToWait { get; set; } = 0;
//
//     public Wait(float secondsToWait) => SecondsToWait = secondsToWait;
//
//     //On tient pour acquis qu'Execute va �tre appeler � chaque frame
//     public override TaskState Execute()
//     {
//         ElapsedTime += Time.deltaTime;
//
//         if (ElapsedTime > SecondsToWait)
//         {
//             ElapsedTime = 0;
//             return TaskState.Success;
//         }
//
//         return TaskState.Running;
//     }
// }

// public class Patrol : TaskBT
// {
//     private Vector3[] Destinations { get; set; }
//     private NavMeshAgent Agent { get; set; }
//     private int CurrentDestinationID { get; set; }
//
//     public Patrol(Vector3[] destinations, NavMeshAgent agent)
//     {
//         Destinations = destinations;
//         Agent = agent;
//     }
//
//     public override TaskState Execute()
//     {
//         Vector3 currentDestination = Destinations[CurrentDestinationID];
//         Agent.destination = currentDestination;
//
//         if (Vector3.Distance(currentDestination, Agent.transform.position) < Agent.stoppingDistance)
//         {
//             CurrentDestinationID = (CurrentDestinationID +1) % Destinations.Length;
//             return TaskState.Success;
//         }
//         return TaskState.Running;
//     }
// }
// public class Patrol : TaskBT
// {
//     private Vector3[] Destinations { get; set; }
//     private NavMeshAgent Agent { get; set; }
//     private int CurrentDestinationID { get; set; }
//
//     public Patrol(Vector3[] destinations, NavMeshAgent agent)
//     {
//         Destinations = destinations;
//         Agent = agent;
//     }
//
//     public override TaskState Execute()
//     {
//         if (Destinations == null || Destinations.Length == 0)
//         {
//             Debug.LogError("Patrol: No destinations specified!");
//             return TaskState.Failure;
//         }
//
//         Vector3 currentDestination = Destinations[CurrentDestinationID];
//         Agent.destination = currentDestination;
//
//         // Debug.Log($"Patrol: Current position: {Agent.transform.position}, Destination: {currentDestination}");
//         Vector3 t_agent = Agent.transform.position;
//         float distance = Vector3.Distance(new Vector3(currentDestination.x,0,currentDestination.z), new Vector3(t_agent.x,0,t_agent.z));
//         if (distance < (Agent.stoppingDistance))
//         {
//             CurrentDestinationID = (CurrentDestinationID + 1) % Destinations.Length;
//             Debug.Log("Patrol: Reached destination, moving to the next one.");
//             return TaskState.Success;
//         }
//
//         return TaskState.Running;
//     }
// }

