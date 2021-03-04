using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_Slam : MonoBehaviour
{
    public float radius;
    public float moveSpeed;
    public Vector2 startPoint;
    public int numberOfProjectiles;
    public GameObject projectile;
	public bool idle;
	public Animator animator;
	public GameObject vunerable;

    // Start is called before the first frame update
	void Start(){
		animator = GetComponent<Animator>();
	}
	public void SlamCharge(){
		FindObjectOfType<AudioManager>().Play("LazerCharge");
	}
    public void Slam(){
        startPoint = transform.position;  
        SpawnProjectiles(numberOfProjectiles);
		FindObjectOfType<AudioManager>().Play("Slam");
		animator.ResetTrigger("Slam");
    }

	public void SlamReset(){
		animator.ResetTrigger("Slam");
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
