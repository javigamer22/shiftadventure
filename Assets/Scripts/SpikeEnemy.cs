using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemy : MonoBehaviour
{
    private PlayerMove2 player;
    private Animator playerAnimator;
    private void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerMove2>();
            playerAnimator = collision.gameObject.GetComponent<Animator>();
            playerAnimator.SetBool("Destroy", true);
            //Destroy(collision.gameObject, 1.0f);
            player.Kill();
        }
    }
}
