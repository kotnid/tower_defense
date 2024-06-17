using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private TurretBluePrint turretToBuild;
    public GameObject buildEffect;
    public GameObject sellEffect;
    public Node selectedNode;
    public NodeUI nodeUI;
    public bool CanBuild { get { return turretToBuild != null;}}
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost;}}

    public void SelectNode (Node node)
    {
        if(selectedNode == node)
        {
            DeSelectNode();
            return ;
        }
        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }

    public void DeSelectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild (TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeSelectNode();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        turretToBuild = null;
    }
}
