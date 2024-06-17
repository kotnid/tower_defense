using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static bool gameEnded;
    [SerializeField] GameObject gameOverUI;
    public GameObject completeLevelUI;
    
    void Start()
    {
        gameEnded = false;
    }
    
    void Update()
    {
        if(gameEnded)
        {
            return ;
        }

        if(Input.GetKeyDown("e"))
        {
            EndGame();
        }

        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel(int level)
    {
        gameEnded = true;
        int highestLevelReached = PlayerPrefs.GetInt("highestlevelReached", 0);
        if(level > highestLevelReached)
        {
            PlayerPrefs.SetInt("highestLevelReached",level);
        }
        completeLevelUI.SetActive(true);
    }
}
