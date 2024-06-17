using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;
    public GameObject ui;
    public TextMeshProUGUI upgradeCost;
    public Button upgradeButton;
    public TextMeshProUGUI sellText;
    public RightUI rightUI;

    public void SetTarget(Node _target)
    {   
        target = _target;
        transform.position = target.transform.position;
        if(!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBluePrint.upgradeCost;
            sellText.text = "$" + (int)(target.turretBluePrint.cost* 0.5f);
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MAXED";
            sellText.text = "$" + (int)((target.turretBluePrint.upgradeCost + target.turretBluePrint.cost )* 0.5f);
            upgradeButton.interactable = false;
        }
        
        ui.SetActive(true);
        rightUI.SetUI(target);
    }

    public void Hide()
    {
        ui.SetActive(false);
        rightUI.HideUI();
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeSelectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeSelectNode();
    }
}
