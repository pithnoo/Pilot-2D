using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public AudioManager theAudioManager;

    // Update is called once per frame
    void Update()
    {
        theAudioManager = FindObjectOfType<AudioManager>();
    }

    public void LoadNextLevel(){
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadFast(){
        StartCoroutine(LoadLevelFast(SceneManager.GetActiveScene().buildIndex + 1));        
    }

    IEnumerator LoadLevel(int levelIndex){
        yield return new WaitForSeconds(3);

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadLevelFast(int levelIndex){
        theAudioManager.stopPlaying("LevelTheme");
        AudioListener.pause = false;
        theAudioManager.Play("ButtonSelect");
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(2);

        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(levelIndex);

        theAudioManager.Play("LevelTheme");
        theAudioManager.currentSong = "LevelTheme";
    }
}
