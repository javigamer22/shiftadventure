using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMove2 : MonoBehaviour
{
    public CheckBorder groundSensor;
    public CheckBorder leftSensor;
    public CheckBorder rightSensor;
    public CheckBorder topSensor;

    public float runSpeed = 2;
    public float jumpSpeed = 3;

    private bool shouldJump = false; // Variable para controlar el salto
    private bool muerte = false;    // Define si el jugador está muerto
    private float tGameOver; // Tiempo desde que el player muere hasta que saltamos a GameOver
    public float fallMultiplier = 0.5f;

    public float lowJumpMultiplier = 1f;

    public Animator animator;

    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        muerte = false;
        tGameOver = 1.5f;
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("Player Creado!");
    }
   
    void Update()
    {
        // Detecta si se presiona la tecla espacio
        if (
                Input.GetKeyDown("space") &&  
                (groundSensor.isBordered || leftSensor.isBordered || rightSensor.isBordered)
                && !muerte
            )
        {
            shouldJump = true;
        }
    }
    
    void FixedUpdate()
    {
        if (!muerte)
        {
            if (Input.GetKey("d") || Input.GetKey("right"))
            {
                rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);
                spriteRenderer.flipX = false;
                animator.SetBool("Running", true);
            }
            else if (Input.GetKey("a") || Input.GetKey("left"))
            {
                rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);
                spriteRenderer.flipX = true;
                animator.SetBool("Running", true);
            }
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                animator.SetBool("Running", false);
            }
        }

        if(groundSensor.isBordered || leftSensor.isBordered || rightSensor.isBordered)
        {
            animator.SetBool("Jump", false);
        }
        else
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Running", false);
        }

        if (shouldJump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            shouldJump = false;
        }

        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        if (rb2d.velocity.y > 0 && !Input.GetKey("space"))
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }

        // GoTo GameOver
        if (muerte && tGameOver > 0.0f)
        {
            tGameOver -= Time.deltaTime;
        }
        else if (muerte && tGameOver <= 0.0f)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Kill()
    {
        Debug.Log("Destruyo Player!!");
        muerte = true;
    }
}
