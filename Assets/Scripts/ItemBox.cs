using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    private Animator animator;
    public int golpesMax = 3;
    private int golpesActual;

    private void Start()
    {
        golpesActual = 0;
        animator = gameObject.transform.GetComponentInParent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Colisión en Caja!!!");
            golpesActual += 1;
            if (golpesActual >= golpesMax)
            {
                animator.SetBool("Romper", true);
                animator.SetBool("Golpe", false);
            }
            else
            {
                animator.SetBool("Golpe", true);
            }            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("Golpe", false);
    }
  
    private void FixedUpdate()
    {
        if (golpesActual >= golpesMax)
        {
            Destroy(transform.parent.gameObject, 0.5f);
        }
    }
}
