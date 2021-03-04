using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAfter : MonoBehaviour
{

    public MovingPlatform pmove;
    // Start is called before the first frame update
    void Start()
    {
        pmove = GetComponent<MovingPlatform>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Active");
        pmove.canMove = true;
    }
}
