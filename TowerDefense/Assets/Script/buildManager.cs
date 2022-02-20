using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class buildManager : MonoBehaviour
{
    #region Singleton
    public static buildManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a déjà une instance de buildManager sur la scène");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject buildEffect;
    public GameObject sellEffect;
    
    private turretBlueprint turretToBuild;
    private node selectedNode;

    public nodeUi nodeUi;
    
    public bool canBuild
    {
        get { return turretToBuild != null; }
    }
    
    public bool hasMoney
    {
        get { return playerStats.money >= turretToBuild.cost; }
    }

    public void SelectTurretToBuild(turretBlueprint turret)
    {
        //Choisir le type de tourrelle
        turretToBuild = turret;
        DeselectNode();
    }

    public turretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUi.Hide();
    }
    
    public void SelectNode(node node)
    {
        //Attention à ne pas garder en mémoire la tourelle à construire
        if (node == selectedNode)
        {
            DeselectNode();
            return;
        }
        Debug.Log("selectNode");
        selectedNode = node;
        turretToBuild = null;

        nodeUi.SetTarget(node);
    }
}