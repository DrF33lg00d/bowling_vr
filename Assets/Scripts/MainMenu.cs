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
        Debug.Log("Changed sound on " + sliderItem.value);
    }
}
