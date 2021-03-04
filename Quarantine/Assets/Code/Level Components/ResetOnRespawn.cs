using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespawn : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startLocalScale;

    private Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Rigidbody2D>() != null) //tests whether the selected object has a rigidbody, this prevents errors from coins and extra lives
        {
            myRigidBody = GetComponent<Rigidbody2D>();
        }

        startPosition = transform.position; //in reference to the selected objects position, rotation, and local scale
        startRotation = transform.rotation;
        startLocalScale = transform.localScale; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetObject()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.localScale = startLocalScale;

        if(myRigidBody != null)
        {
            myRigidBody.velocity = Vector3.zero;
        }
    }
}
