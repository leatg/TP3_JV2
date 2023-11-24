using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IsJumping(bool isJumping)
    {
        animator.SetBool("IsJumping", isJumping);
    }

    public void IsAttacking(bool isAttacking)
    {
        animator.SetBool("IsAttacking",isAttacking);
    }
}
