using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;

    void Update()
    {
        livesText.text = PlayerStats.Lives.ToString();
    }
}
