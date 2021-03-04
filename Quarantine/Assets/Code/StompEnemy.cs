using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;

    public GameObject blue, green, red, slime, titan;

    public float bounceForce;
    public float bossBoost;

    public float enemyRepel;
    public bool bossDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bounce")
        {           
            playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, bounceForce, 0f);
            FindObjectOfType<AudioManager>().Play("Bounce");
            Instantiate(slime,other.transform.position,other.transform.rotation);
            //particle effect   
        }

        if(other.tag == "Enemy")
        {
            //Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            other.gameObject.SetActive(false);
            Instantiate(blue, other.transform.position, other.transform.rotation);
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, enemyRepel);
        }

        if(other.tag == "EnemyGreen")
        {
            //Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            other.gameObject.SetActive(false);
            Instantiate(green, other.transform.position, other.transform.rotation);
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, enemyRepel);
        }

        if(other.tag == "EnemyRed")
        {
            //Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            other.gameObject.SetActive(false);
            Instantiate(red, other.transform.position, other.transform.rotation);
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, enemyRepel);
        }

        if(other.tag == "EnemyPilot"){
            FindObjectOfType<AudioManager>().Play("Slam");
            Instantiate(titan, other.transform.position, other.transform.rotation);
            playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, bossBoost, 0f);
            bossDamage = true;
        }
    }
}
