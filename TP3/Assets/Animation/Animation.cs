using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Animation : MonoBehaviour
{
    bool jumping { get; set; } = false;
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)){
            //animator.SetBool(0, true); //IsWalking
            animator.SetBool("IsWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.W)){
            animator.SetBool("IsWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //animator.SetBool(0, true); //IsWalking
            animator.SetBool("IsRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
        }

        if (jumping)
        {
            animator.SetBool("IsJumping", true);
        }
    }
    public void Stop()
    {
        animator.SetBool("IsJumping", false);
        jumping = false;
    }
    public void StopCelebrating()
    {
        animator.SetBool("IsCelebrating", false);
    }
}
