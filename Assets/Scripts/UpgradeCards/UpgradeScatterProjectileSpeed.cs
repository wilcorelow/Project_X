using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScatterProjectileSpeed", menuName = "Scriptable Objects/Upgrade Card/Scatter Projectile Speed")]
public class UpgradeScatterProjectileSpeed : UpgradeCard
{
    public override void Activate()
    {
        base.Activate();

        ScatterManager scatterManager = player.GetComponent<ScatterManager>();
        Equipment equipment = scatterManager.equipment;

        if (player.GetComponent<PlayerController>().selectedWeaponManager.GetType() == scatterManager.GetType())
        {
            if (equipmentManager.onEquipmentChanged != null)
                equipmentManager.onEquipmentChanged.Invoke(null, equipment);
            equipment.projectileSpeedModifier.AddModifier(Upgrade());
            if (equipmentManager.onEquipmentChanged != null)
                equipmentManager.onEquipmentChanged.Invoke(equipment, null);
        }
        else equipment.projectileSpeedModifier.AddModifier(Upgrade());

        if (isMaxLevelReached) upgradeCardHolder.weapons.Remove(upgradeCardHolder.scatterProjectileSpeed);

        playerLevelManager.OpenRewardPanel(false);
    }
}
