using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private int num = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // M�todo para ser llamado por el Animation Event
    public void ResetDoubleJump()
    {
        num++;
        Debug.Log("Funci�n llamada!" + num);
        animator.SetBool("DoubleJump", false);
    }

    // M�todo para ser llamado por el Animation Event
    public void ResetWallJump()
    {
        num++;
        Debug.Log("Funci�n Wall !" + num);
        animator.SetBool("WallJump", false);
    }
}

