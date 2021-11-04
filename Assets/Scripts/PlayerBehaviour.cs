using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerDirection
{
    FORWARD = 0,
    RIGHT = 1,
    BACKWARD = 2,
    LEFT = 3
}

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 5;
    public Animator animatior;
    public PlayerDirection direction = PlayerDirection.FORWARD;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0));

        if (Input.GetAxis("Horizontal") > 0)
        {
            direction = PlayerDirection.RIGHT;
            animatior.SetInteger("WalkDirection", (int)direction);
            animatior.SetBool("IsWalking", true);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            direction = PlayerDirection.LEFT;
            animatior.SetInteger("WalkDirection", (int)direction);
            animatior.SetBool("IsWalking", true);
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            direction = PlayerDirection.BACKWARD;
            animatior.SetInteger("WalkDirection", (int)direction);
            animatior.SetBool("IsWalking", true);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            direction = PlayerDirection.FORWARD;
            animatior.SetInteger("WalkDirection", (int)direction);
            animatior.SetBool("IsWalking", true);
        }
        else
        {
            animatior.SetInteger("WalkDirection", (int)direction);
            animatior.SetBool("IsWalking", false);
        }
    }

    
}
