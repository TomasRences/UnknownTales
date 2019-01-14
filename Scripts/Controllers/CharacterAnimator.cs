
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void StopRunning()
    {
        if (!animator.GetBool("IsAttacking"))
        {
            animator.SetInteger("Action", 0);
        }
    }

    public void StopAttacking()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetInteger("Action", 0);
    }

    public void Died()
    {
        animator.SetBool("Died", true);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsWalking", false);
        animator.SetInteger("Action", 0);
    }

    public void StartAttacking()
    {
        animator.SetBool("IsAttacking", true);
        animator.SetInteger("Action", 2);
    }

    public void StartRunning()
    {
        animator.SetInteger("Action", 1);
    }
}