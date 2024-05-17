using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMove2 : MonoBehaviour
{
    public static PlayerMove2 instance;
    public CheckBorder groundSensor;
    public CheckBorder leftSensor;
    public CheckBorder rightSensor;
    public CheckBorder topSensor;

    public float runSpeed = 2;
    public float jumpSpeed = 3;
    public float djumpSpeed = 2.5F;

    private bool shouldJump = false; // Variable para controlar el salto
    private bool dobleSalto = false; // Activación doble salto
    private bool muerte = false;    // Define si el jugador está muerto
    private int vidas = 3;
    private float tGameOver; // Tiempo desde que el player muere hasta que saltamos a GameOver
    private Vector3 startPosition;
    public float fallMultiplier = 0.5f;

    public float lowJumpMultiplier = 1f;

    public Animator animator;

    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        Debug.Log("Awake called.");
    }

    void Start()
    {
        muerte = false;
        tGameOver = 1.5f;
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Posiciono el jugador en el origen (0,0,0)
        transform.position = Vector3.zero;

        // Asegura que el SpriteRenderer esté activado
        GetComponent<SpriteRenderer>().enabled = true;
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
        // Permito doble salto
        if(
                shouldJump == false
                && Input.GetKeyDown("space")
                && !dobleSalto
                && rb2d.velocity.y != 0
            )
        {
            shouldJump = true;
            dobleSalto = true;
            animator.SetBool("DoubleJump", true);
            animator.SetBool("Fall", false);
            animator.SetBool("Running", false);
        }

        if ((dobleSalto && groundSensor.isBordered) || rb2d.velocity.y == 0)
        {
            dobleSalto = false;
        }

        if (
                (leftSensor.isBordered || rightSensor.isBordered)
                //&& Input.GetKeyDown("space")
                //&& shouldJump
                && rb2d.velocity.y < 0
                && !groundSensor.isBordered
            )
        {
            animator.SetBool("WallJump", true);
            animator.SetBool("Running", false);
        }
        else
        {
            animator.SetBool("WallJump", false);
        }

        if (groundSensor.isBordered)
        {
            animator.SetBool("WallJump", false);
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

        if(groundSensor.isBordered)// || leftSensor.isBordered || rightSensor.isBordered
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", false);
        }
        else
        {
            animator.SetBool("Jump", true);
            if (rb2d.velocity.y < 0)
            {
                animator.SetBool("Fall", true);
            }
            animator.SetBool("Running", false);
        }

        if (shouldJump)
        {
            if (dobleSalto)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, djumpSpeed);
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            }            
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
        else if (muerte && tGameOver <= 0.0f && vidas <= 0)
        {
            Debug.Log("Vidas = " + vidas);
            Invoke("GameOver", 3.0f);
        }
        else if (muerte && tGameOver <= 0.0f && vidas > 0)
        {
            muerte = false;
            Debug.Log("Vidas = " + vidas);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public int getVidas()
    {
        return vidas;
    }

    public void Desaparece()
    {
        spriteRenderer.enabled = false;
    }

    public void Kill()
    {
        if (!muerte)
        {
            vidas -= 1;
            muerte = true;
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }

    private void GameOver()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }
}
