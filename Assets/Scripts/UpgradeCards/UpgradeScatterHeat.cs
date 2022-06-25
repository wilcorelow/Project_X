using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScatterHeat", menuName = "Scriptable Objects/Upgrade Card/Scatter Heat")]
public class UpgradeScatterHeat : UpgradeCard
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
            equipment.heat.AddModifier(Upgrade());
            if (equipmentManager.onEquipmentChanged != null)
                equipmentManager.onEquipmentChanged.Invoke(equipment, null);
        }
        else equipment.heat.AddModifier(Upgrade());

        if (isMaxLevelReached) upgradeCardHolder.weapons.Remove(upgradeCardHolder.scatterHeat);

        playerLevelManager.OpenRewardPanel(false);
    }
}
