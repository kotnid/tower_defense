using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{   
    [SerializeField] Color hoverColor;
    [SerializeField] Color notEnoughMoneyColor;
    [SerializeField] GameObject turrets;
    [SerializeField] Vector3 positionOffest;
    private Renderer rend;
    private Color startColor;
    public GameObject turret;
    public TurretBluePrint turretBluePrint;
    public bool isUpgraded = false;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    private void OnMouseDown() {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return ;
        }

        if(turret != null)
        {
            buildManager.SelectNode(this);
        }
        else
        {
            if(!buildManager.CanBuild)
            {
                return ;
            }
            
            BuildTurret(buildManager.GetTurretToBuild());
        }
    }

    private void BuildTurret(TurretBluePrint blueprint)
    {
        if(PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("not enough money");
            return ;
        }

        PlayerStats.Money -= blueprint.cost;

        turretBluePrint = blueprint;
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position, transform.rotation);
        turret = _turret;
        turret.transform.SetParent(turrets.transform);

        Transform turretTransform = turret.transform;
        float turretScale = turretTransform.localScale.y;

        float offsetY = 0.2f;
        if(turret.name.Contains("turret"))
        {
            offsetY += 0.2f;
        }
        if(turret.name.Contains("laser"))
        {
            offsetY -= 0.15f;
        }

        turretTransform.localPosition = new Vector3(
            turretTransform.localPosition.x,
            offsetY + Mathf.Abs(1.4f - turretScale) * 0.5f,
            turretTransform.localPosition.z
        );

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);

        Debug.Log("Left"+PlayerStats.Money);
    }

    public void UpgradeTurret()
    {
        if(isUpgraded)
        {
            return ;
        }
        
        if(PlayerStats.Money < turretBluePrint.upgradeCost)
        {
            Debug.Log("not enough money");
            return ;
        }

        PlayerStats.Money -= turretBluePrint.upgradeCost;

        int targetIndex = turret.GetComponent<Turret>().targetIndex;
        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefab, transform.position, transform.rotation);
        turret = _turret;
        turret.transform.SetParent(turrets.transform);
        
        turret.GetComponent<Turret>().targetIndex = targetIndex;
        Debug.Log(targetIndex);
        Debug.Log(turret.GetComponent<Turret>().targetIndex);   

        Transform turretTransform = turret.transform;
        float turretScale = turretTransform.localScale.y;

        float offsetY = 0.2f;
        if(turret.name.Contains("turret"))
        {
            offsetY += 0.2f;
        }
        if(turret.name.Contains("laser"))
        {
            offsetY -= 0.15f;
        }

        turretTransform.localPosition = new Vector3(
            turretTransform.localPosition.x,
            offsetY + Mathf.Abs(1.4f - turretScale) * 0.5f,
            turretTransform.localPosition.z
        );

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);

        isUpgraded = true;

        Debug.Log("Left"+PlayerStats.Money);
    }

    private void OnMouseEnter() 
    {   
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return ;
        }

        if(!buildManager.CanBuild)
        {
            return ;
        }

        if(buildManager.HasMoney)
        {
            rend.material.color = hoverColor;    
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    private void OnMouseExit() {
        rend.material.color = startColor;
    }

    public void SellTurret()
    {
        PlayerStats.Money += (int)(turretBluePrint.cost * 0.5f);
        if(isUpgraded)
        {
            PlayerStats.Money += (int)(turretBluePrint.upgradeCost * 0.5f);
            isUpgraded = false;
        }
        Destroy(turret);
        turretBluePrint = null;

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }
}
