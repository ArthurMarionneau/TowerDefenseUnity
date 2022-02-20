using UnityEngine.EventSystems;
using UnityEngine;

//Classe relative à la construction sur les nodes, vérif argent, changement couleur, ...
public class node : MonoBehaviour
{
    public Color hoverColor;
    public Color startColor;
    public Color notMoneyColor;
    
    [HideInInspector]
    public GameObject turret;
    [HideInInspector] 
    public turretBlueprint turretBlueprint;    
    [HideInInspector] 
    public bool isUpgraded = false;
    
    public Vector3 positionOffSet;

    private buildManager buildManager;
    
    private Renderer rend;

    void Start()
    {
        buildManager = buildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
    
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffSet;
    }

    private void BuildTurret(turretBlueprint blueprint)
    {
        //Construire la tourrelle 
        if (playerStats.money < blueprint.cost)
        {
            Debug.Log("pas de tune");
            return;
        }

        playerStats.money -= blueprint.cost;
        turretBlueprint = blueprint;
        
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1f);

        
        Debug.Log("Il vous reste : " + playerStats.money);
    }
    
    private void OnMouseDown()
    {
        //Construction de la tourelle
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        
        if (!buildManager.canBuild)
        {
            return;
        }
        BuildTurret(buildManager.GetTurretToBuild());
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (!buildManager.canBuild)
        {
            return;
        }
        rend.material.color = hoverColor;

        if (buildManager.hasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notMoneyColor;
        }
    }
    
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    public void UpgradeTurret()
    {
        Debug.Log("upgrade node");
        //Améliorer la tourrelle 
        if (playerStats.money < turretBlueprint.upgradeCost)
        {
            Debug.Log("pas de tune");
            return;
        }
        playerStats.money -= turretBlueprint.upgradeCost;
        
        //Suppresion Ancienne Tourrelle
        Destroy(turret);
        //Création tourrelle améliorée
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradePrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1f);

        isUpgraded = true;
        
        Debug.Log("Tourrelle Up, Il vous reste : " + playerStats.money);
    }
    
    public void SellTurret()
    {
        Debug.Log("sell node");
        //Vendre la tourrelle

        playerStats.money += turretBlueprint.GetSellAmout();
        
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1f);
        
        //Suppresion  Tourrelle
        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;
    }
}
