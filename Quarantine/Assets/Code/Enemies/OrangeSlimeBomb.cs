using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeSlimeBomb : MonoBehaviour
{
    public AudioManager theAudioManager;

    [Header("Ground Check Settings")]
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;
    public Transform groundCheck;

    [Header("Projectile Settings")]
    public GameObject projectile;
    public int numberOfProjectiles;
    public float moveSpeed = 5f;
    public Vector2 startPoint;
    public float radius = 5f;


    // Start is called before the first frame update
    void Start()
    {
        theAudioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if(isGrounded == true){
            explode();
        }
    }

    void explode()
    {
        Destroy(gameObject);
        startPoint = transform.position;
        SpawnProjectiles(numberOfProjectiles);
        theAudioManager.Play("MissileFire");
    }

    public void SpawnProjectiles(int numProjectiles)
	{
		float angleStep = 360f / numProjectiles;
		float angle = 0f;

		for (int i = 0; i <= numberOfProjectiles - 1; i++) {
			
			float projectileDirXposition = startPoint.x + Mathf.Sin ((angle * Mathf.PI) / 180) * radius;
			float projectileDirYposition = startPoint.y + Mathf.Cos ((angle * Mathf.PI) / 180) * radius;

			Vector2 projectileVector = new Vector2 (projectileDirXposition, projectileDirYposition);
			Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

			var proj = Instantiate (projectile, startPoint, Quaternion.identity);
            proj.transform.Rotate(0, 0, angle);
			proj.GetComponent<Rigidbody2D> ().velocity = 
				new Vector2 (projectileMoveDirection.x, projectileMoveDirection.y);

			angle += angleStep;
		}
	}
}
