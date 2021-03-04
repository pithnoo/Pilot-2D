using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public int levelIndex;
    public AudioManager theAudioManager;
    void Start(){
        theAudioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                resume();
            }
            else{
                pause();
            }
        }    
    }

    public void resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        theAudioManager.Play("ButtonSelect");
        AudioListener.pause = false;
    }

    void pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        theAudioManager.Play("ButtonSelect");
        AudioListener.pause = true;
    }

    public void loadMenu(){
        AudioListener.pause = true;
        SceneManager.LoadScene(levelIndex);
        Time.timeScale = 1f;
        Debug.Log("menu loaded");
        isPaused = false;
    }

    public void quitGame(){
        Debug.Log("exited game");
        Application.Quit();
    }
}

