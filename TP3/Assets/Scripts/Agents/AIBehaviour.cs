using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] private bool isUpdatingTargetPosition = true;
    private NavMeshAgent _navMeshAgent;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        StartCoroutine(UpdateTargetPosition());
    }
    private IEnumerator UpdateTargetPosition()
    {
        while (isUpdatingTargetPosition)
        {
            _navMeshAgent.SetDestination(targetTransform.position);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
