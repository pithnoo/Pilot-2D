using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float activeMoveSpeed;
    public float jumpSpeed;
    private bool falling;

    private Rigidbody2D myRigidBody;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public bool isGrounded;
    public GameObject stompBox;

    private Animator Anim;

    public Vector3 respawnPosition;

    public LevelManager theLevelManager;

    public float knockbackForce, knockbackLength, knockbackCount;
    public bool knockRight;

    private bool onPlatform;
    public float onPlatformSpeedMod;

    public bool canMove;

    public float iLength;
    private float iCounter;

    private AudioManager theAudioManager;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        theLevelManager = FindObjectOfType<LevelManager>();

        respawnPosition = transform.position;
        activeMoveSpeed = moveSpeed;

        theAudioManager = FindObjectOfType<AudioManager>();

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (knockbackCount <= 0 && canMove)
        {
            if (onPlatform)
            {
                activeMoveSpeed = moveSpeed * onPlatformSpeedMod;
            }
            else
            {
                activeMoveSpeed = moveSpeed;
            }

            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRigidBody.velocity = new Vector3(activeMoveSpeed, myRigidBody.velocity.y, 0f);
                transform.localScale = new Vector3(2f, 2f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRigidBody.velocity = new Vector3(-activeMoveSpeed, myRigidBody.velocity.y, 0f);
                transform.localScale = new Vector3(-2f, 2f, 1f);
            }
            else
            {
                myRigidBody.velocity = new Vector3(0f, myRigidBody.velocity.y, 0f);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                theAudioManager.Play("Jump");
                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, jumpSpeed, 0f);                         
            }
            
        }
        else
        {
            if (knockRight)
            {
                myRigidBody.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
            }
            else
            {
                myRigidBody.velocity = new Vector3(knockbackForce, knockbackForce, 0f);
            }
            knockbackCount -= Time.deltaTime;           
        }

        if(iCounter <= 0)
        {
            theLevelManager.invincible = false;
        }
        if(iCounter > 0)
        {
            iCounter -= Time.deltaTime; 
        }

        if(myRigidBody.velocity.y < -0.01)
        {
            falling = true;
        }
        else
        {
            falling = false;
        }

        Anim.SetFloat("Speed", Mathf.Abs(myRigidBody.velocity.x));
        Anim.SetBool("Falling", falling);
        Anim.SetBool("Grounded", isGrounded);

        if(myRigidBody.velocity.y < 0)
        {
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }
    }

    public void knockback()
    {
        knockbackCount = knockbackLength;
        iCounter = iLength;
        theLevelManager.invincible = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "KillPlane")
        {
            //gameObject.SetActive(false);

            //transform.position = respawnPosition;
            theLevelManager.healthCount = 0;
            
            //theLevelManager.Respawn();
        }

        if(other.tag == "Checkpoint")
        {
            respawnPosition = new Vector3(other.transform.position.x, other.transform.position.y, 0f);
        }

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
            onPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
            onPlatform = false;
        }
    }
}
