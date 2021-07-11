using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Movement Settings")]
    //Speed is how quickly the player moves, movementSmoothing is a value passed into the smoothDamp method to calculate how to spread it out over time. I'd keep it at 0.05f, it's a good value.
    public float speed;
    public float movementSmoothing = 0.05f;
    //The player's current direction is stored as a Vector2 and is constantly updated with the Horizontal and Vertical axes
    private Vector2 direction { get; set; }
    //Reference to gameObj's rigidbody to set its velocity for movement
    private Rigidbody2D rb;
    //Collider for roll IFrames
    private BoxCollider2D col;
    //m_velocity is the "current velocity" param passed into Vector2.SmoothDamp. This is set at 0 because the player's velocity should start at 0--then, each time we set the velocity with SmoothDamp, it updates m_velocity.
    private Vector2 m_velocity = Vector2.zero;

    [Header("Dodge Settings")] 
    public float dodgeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        //Checks for roll Input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Turn collider off for roll
            col.enabled = false;
            Vector2 tempVect = direction;
            tempVect = tempVect.normalized * (Time.fixedDeltaTime * dodgeSpeed);
            rb.MovePosition((Vector2)transform.position + tempVect.normalized * 2.25f);
            //Turn collider back on
            col.enabled = true;
            //"Boom" mechanic needs to be added to dodge
        }
    }

    private void FixedUpdate()
    {
        //Target velocity is calculated by taking the speed, time (to adjust for differing framerates), and direction into account
        Vector2 targetVelocity = new Vector2(speed * 10 * direction.x * Time.fixedDeltaTime, speed * 10 * direction.y * Time.fixedDeltaTime);
        //Set the velocity using the SmoothDamp function to ensure a smooth movement experience
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_velocity, movementSmoothing);
    }
}
