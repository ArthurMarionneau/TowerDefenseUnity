using UnityEngine;
using UnityEngine.UI;

public class nodeUi : MonoBehaviour
{
    private node target;

    public GameObject ui;
    public Text upgradeCost;
    public Button upgradeButton;
    public Text sellAmount;
    public void SetTarget(node _target)
    {
        target = _target;

        Debug.Log("settarget2");
        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "-" + target.turretBlueprint.upgradeCost + "$";
            upgradeButton.interactable = true;
            ui.SetActive(true);
        }
        else
        {
            upgradeCost.text = "Max";
            upgradeButton.interactable = false;
            ui.SetActive(true);
        }

        sellAmount.text = "+" + target.turretBlueprint.GetSellAmout() + "$";
        Debug.Log("setTarget2");
        
        
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        Debug.Log("upgrade");
        target.UpgradeTurret();
        buildManager.instance.DeselectNode();
    }
    
    public void Sell()
    {
        Debug.Log("sell");
        target.SellTurret();
        buildManager.instance.DeselectNode();
    }
}
