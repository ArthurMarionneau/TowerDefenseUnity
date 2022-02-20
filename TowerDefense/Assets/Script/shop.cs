using UnityEngine;

public class shop : MonoBehaviour
{
    private buildManager build;
    public turretBlueprint standardTurret;
    public turretBlueprint missileTurret;
    public turretBlueprint laserTurret;

    private void Start()
    {
        build = buildManager.instance;
    }

    public void PurchaseStandardTurrent()
    {
        build.SelectTurretToBuild(standardTurret);
    }
    
    public void PurchaseMissileTurrent()
    {
        build.SelectTurretToBuild(missileTurret);
    }
    
    public void PurchaseLaserTurrent()
    {
        build.SelectTurretToBuild(laserTurret);
    }
}
