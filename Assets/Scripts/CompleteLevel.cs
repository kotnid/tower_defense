using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour
{
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";
    public Button continueButton;

    public void Awake()
    {
        LevelSelector.currentLevel++;
        Debug.Log(LevelSelector.currentLevel);
        Debug.Log(LevelSelector.highestLevel);
        if(LevelSelector.currentLevel >= LevelSelector.highestLevel)
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        } 
    }

    public void Continue()
    {
        if(LevelSelector.currentLevel < LevelSelector.highestLevel)
        {
            string levelName = "Level" + ((LevelSelector.currentLevel-1)/3+1).ToString();
            sceneFader.FadeTo(levelName);
        }   
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
