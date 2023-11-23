using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class TestBehaviourTree : MonoBehaviour
{
    [SerializeField]
    Transform[] patrolDestinations;
    [SerializeField]
    NavMeshAgent agent;

    private Node rootBT;

    private void Awake()
    {
        Vector3[] destinations = patrolDestinations.Select(t => t.position).ToArray();
        
        TaskBT[] tasks0 =
        {
            // new Patrol(destinations, agent),
        };
        TaskBT[] tasks1 =
        {
            new Wait(2)
        };
        TaskBT[] tasks2 =
        {
            new DummyTask("J'ai attendu 2 secs", TaskState.Success)
        };

        TaskNode patrolNode = new TaskNode("patrolNode1", tasks0);
        TaskNode waitNode = new TaskNode("taskNode1", tasks1);
        TaskNode dummyNode = new TaskNode("dummyNode1", tasks2);
        Node seq1 = new Sequence("seq1", new[] {patrolNode, waitNode, dummyNode });

        rootBT = seq1;
    }

    // void Update()
    // {
    //     // rootBT.Evaluate();
    //     
    // }
    void Update()
    {
        // Evaluate the rootBT and store the result
        NodeState rootState = rootBT.Evaluate();

        // Check if the root state is still running
        if (rootState != NodeState.Running)
        {
            // Log the result or perform any additional actions based on the final state
            if (rootState == NodeState.Success)
            {
                Debug.Log("Behavior tree succeeded!");
                rootState = rootBT.Evaluate();
            }
            else if (rootState == NodeState.Failure)
            {
                Debug.Log("Behavior tree failed!");
            }

            // Add any necessary cleanup or reset logic here

            // Optional: Stop the continuous evaluation (comment out if not needed)
            enabled = false;
        }
    }


}
