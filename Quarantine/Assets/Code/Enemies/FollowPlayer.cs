using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float speed;
    public float moveSpeed;
    public float rotateSpeed = 200f;
    public Transform target;
    private bool timeActivate;
    public GameObject projectile;
    private Vector2 startPoint;
    public float radius;
    public int numberOfProjectiles;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();        
        timeActivate = false;       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotationAmount = Vector3.Cross(direction, -transform.right).z;

        rb.angularVelocity = -rotationAmount * rotateSpeed;
        rb.velocity = -transform.right * speed;
                
        if(timeActivate == false){
            timeActivate = true;
            StartCoroutine("Timer");
        }
              
    }

    IEnumerator Timer(){
        yield return new WaitForSeconds(4);
        startPoint = transform.position;
        SpawnProjectiles(numberOfProjectiles);
        FindObjectOfType<AudioManager>().Play("MissileFire");
        Destroy(gameObject);
    }

    void SpawnProjectiles(int numProjectiles)
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
