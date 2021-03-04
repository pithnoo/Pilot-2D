using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestore : MonoBehaviour
{
    private LevelManager theLevelManager;

    public int healthAmount;


    // Start is called before the first frame update
    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && theLevelManager.healthCount < 4)
        {
            theLevelManager.healthRestore(healthAmount);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
