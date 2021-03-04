using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_Link : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lazer;
    public GameObject lazerShot;
    public Transform shotPoint;
    public AudioManager theAudioManager;
    public Transform lazerShotPoint;
    public int pause;
    void Awake() {
        theAudioManager = FindObjectOfType<AudioManager>();
    }

    IEnumerator shootLazer(){
        Vector2 lazerPosition = new Vector2(lazerShotPoint.position.x, lazerShotPoint.position.y);
        Vector2 LazerShotPosition = new Vector2(lazerShotPoint.position.x - 25, lazerShotPoint.position.y);

        yield return new WaitForSeconds(pause);
        
        Instantiate(lazer, lazerPosition, lazerShotPoint.rotation);
        Instantiate(lazerShot, LazerShotPosition, lazerShotPoint.rotation);
    }
}
