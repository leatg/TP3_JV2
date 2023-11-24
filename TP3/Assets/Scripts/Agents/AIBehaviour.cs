using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] private bool isUpdatingTargetPosition = true;
    [SerializeField] float speed;
    private NavMeshAgent _navMeshAgent;
    bool isOnOffMeshLink = false;
    Animator animator;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _navMeshAgent.speed = speed;
        StartCoroutine(UpdateTargetPosition());
    }
    private IEnumerator UpdateTargetPosition()
    {
        while (isUpdatingTargetPosition)
        {
            if (_navMeshAgent.isOnOffMeshLink && !isOnOffMeshLink)
            {
                isOnOffMeshLink = true;
                animator.SetBool("IsJumping", true);
                Debug.Log("is jumping");
            }
            if(!_navMeshAgent.isOnOffMeshLink && isOnOffMeshLink)
            {
                isOnOffMeshLink = false;
                animator.GetComponentInChildren<Animator>().SetBool("IsJumping", false);
                Debug.Log("isnt jumping");
            }
            _navMeshAgent.SetDestination(targetTransform.position);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
