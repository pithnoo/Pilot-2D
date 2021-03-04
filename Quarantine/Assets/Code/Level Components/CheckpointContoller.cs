using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointContoller : MonoBehaviour
{
    public Sprite pointClosed;
    public Sprite pointOpen;

    public bool isOpenStart;
    public bool isOpen;

    private SpriteRenderer theSpriteRender;

    private Animator Anim;

    public bool checkPointActive;

    public bool generateParticle;

    public GameObject checkPointParticle;

    public AudioManager theAudioManager;

    // Start is called before the first frame update
    void Start()
    {
        theSpriteRender = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        theAudioManager = FindObjectOfType<AudioManager>();

        isOpen = false;
        isOpenStart = false;
        generateParticle = false;
    }

    // Update is called once per frame
    void Update()
    {      
        Anim.SetBool("isOpen", isOpen);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Anim.SetTrigger("isOpenStart");           
            isOpen = true;

            checkPointActive = true;
            
            if(generateParticle == false){
                Instantiate(checkPointParticle,transform.position, transform.rotation);
                theAudioManager.Play("CheckPoint");
                generateParticle = true;
            }
        }
    }
}
