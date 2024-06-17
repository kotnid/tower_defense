using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader fader;
    public static int currentLevel = 1;
    public Button[]  LevelButtons;
    public static int highestLevel;

    void Start()
    {
        int highestLevelReached = PlayerPrefs.GetInt("highestLevelReached",0);

        for(int i=highestLevelReached+1; i<LevelButtons.Length; i++)
        {
            LevelButtons[i].interactable = false;
        }
        highestLevel = LevelButtons.Length;
    }

    public void Select (int level)
    {
        currentLevel = level;
        string levelName = "Level" + ((level-1)/3+1).ToString();
        fader.FadeTo(levelName);
    }
}
