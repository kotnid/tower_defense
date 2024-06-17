using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class RightUI : MonoBehaviour
{
    public GameObject ui;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Range;
    public Image Img;
    public TextMeshProUGUI Target;
    private Node target;
    public void SetUI(Node _target)
    {
        target = _target;
        Name.text = target.turretBluePrint.name;
        Range.text = "Range: " + target.turret.GetComponent<Turret>().range.ToString();
        Img.sprite = target.turretBluePrint.sprite;

        int targetIndex = target.turret.GetComponent<Turret>().targetIndex;
        
        string[] targetName = {"Nearest","First","Last","Strongest","Weakest"};
        Target.text = "Target: " + targetName[targetIndex];

        ui.SetActive(true);
    }

    public void HideUI()
    {
        ui.SetActive(false);
    }

    public void ChangeTarget()
    {
        int targetIndex = target.turret.GetComponent<Turret>().targetIndex;
        targetIndex++;
        if(targetIndex == 5)
        {
            targetIndex = 0;
        }
        target.turret.GetComponent<Turret>().targetIndex = targetIndex;
        SetUI(target);
    }
}
