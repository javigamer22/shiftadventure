using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private int num = 0;
    private PlayerMove2 player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerMove2>();
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

    public void ResetLife()
    {
        animator.SetBool("Destroy", false);

        if (player.getVidas() > 0)
        {
            player.ResetPosition();
        }
        else
        {
            player.Desaparece();
        }
    }
}

