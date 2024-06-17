using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;

    void Update()
    {
        moneyText.text = "$" + PlayerStats.Money.ToString();
    }
}
