using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBorder : MonoBehaviour
{
    public bool isBordered;

    void Start()
    {
        isBordered = false;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo") || collision.CompareTag("itemBox"))
        {
            isBordered = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            isBordered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isBordered = false;
    }
}
