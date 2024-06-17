using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint missileLauncher;
    public TurretBluePrint laserBeamer;
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        Debug.Log("standard selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectedMissileLauncher()
    {
        Debug.Log("missile selected");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectedLaserBeamer()
    {
        Debug.Log("laser selected");
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
