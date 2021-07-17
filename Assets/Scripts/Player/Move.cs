using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Move : MonoBehaviour,  IHasCooldown
{
    //Cooldown stuff
    [Header("Cooldown Settings")]
    [SerializeField] private CooldownSystem cooldowns = null;
    //Dodge cooldown ID
    private int dodgeID => 1;
    [SerializeField]
    private float dodgeCooldown = 2f;
    
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
    public int Id => dodgeID;
    public float CooldownDuration => dodgeCooldown;
    
    //Explosion prefab
    public GameObject explosionPrefab;
    public bool dodging = false;
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
            if (cooldowns.IsOnCooldown(Id)) { Debug.Log("DASH COOLDOWN"); return; }
            dodging = true;
        }
    }

    private void FixedUpdate()
    {
        MoveRigidbody(direction.normalized, speed, movementSmoothing);
        if (dodging)
        {
            Dodge();
        }
    }

    private void MoveRigidbody(Vector2 direction, float speed, float movementSmoothing)
    {
        //Target velocity is calculated by taking the speed, time (to adjust for differing framerates), and direction into account
        Vector2 target = new Vector2(speed * 10 * direction.x * Time.fixedDeltaTime, speed * 10 * direction.y * Time.fixedDeltaTime);
        //Set the velocity using the SmoothDamp function to ensure a smooth movement experience
        rb.velocity = Vector2.SmoothDamp(rb.velocity, target, ref m_velocity, movementSmoothing);
    }

    private void Dodge()
    {
        //Toggles Collider
        GetComponent<ToggleColliderOff>().Toggle();
        //Spawns explosions
        Instantiate(explosionPrefab, rb.position, Quaternion.identity);
        MoveRigidbody(direction.normalized, dodgeSpeed * 20, movementSmoothing);
        dodging = false;
        cooldowns.PutOnCooldown(this);
    }
}
