using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    private Animator playerAnimator;
    private PlayerMove2 player;
    private void Start()
    {
        Debug.Log("Enemigo Creado!");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerMove2>();
            playerAnimator = collision.gameObject.GetComponent<Animator>();
            playerAnimator.SetBool("Destroy", true);
            Debug.Log("Player Damage");
            //Destroy(collision.gameObject, 1.0f);
            player.Kill();
        }
    }
}
