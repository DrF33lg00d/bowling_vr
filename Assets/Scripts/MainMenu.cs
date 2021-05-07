using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public float soundVolume = 0.5f;
    public AudioSource soundSource;

    private void Start()
    {
        soundSource.volume = soundVolume;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("FirstGameHall");
    }
    
    public void LoadTestScene()
    {
        SceneManager.LoadScene("Test");
    }

    public void ExitFromGame()
    {
        Debug.Log("Exit from BowlingVR");
        // Stop Editor if starts in Editor
        if (UnityEditor.EditorApplication.isPlaying) UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void ChangeBackSound(Slider sliderItem)
    {
        soundVolume = sliderItem.value;
        soundSource.volume = soundVolume;
        Debug.Log("Changed sound on " + soundVolume);
    }
}
