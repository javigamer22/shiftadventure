using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromEffector : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float time;

    void Start()
    {
        time = 0.5f;
        effector = gameObject.GetComponent<PlatformEffector2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey("s") || Input.GetKey("down"))
            {
                time -= Time.deltaTime;
                if (time <= 0.0f)
                {
                    effector.rotationalOffset = 180;
                    time = 0.0f;
                }
            }
        }
        else
        {
            time = 0.5f;
            effector.rotationalOffset = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        time = 0.5f;
        effector.rotationalOffset = 0;
    }

    void Update()
    {
        
    }
}
