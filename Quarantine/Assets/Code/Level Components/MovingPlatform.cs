using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject objectToMove;

    public Transform startPoint;
    public Transform endPoint;

    public float moveSpeed;
    public bool x;

    private Vector3 currentTarget;
    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = endPoint.position;
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);

            if (objectToMove.transform.position == endPoint.position)
            {
                currentTarget = startPoint.position;
                x = false;
            }
            if (objectToMove.transform.position == startPoint.position)
            {
                currentTarget = endPoint.position;
                x = true;
            }
        }
        
    }
}
