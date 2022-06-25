using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScatterAttackSpeed", menuName = "Scriptable Objects/Upgrade Card/Scatter Attack Speed")]
public class UpgradeScatterAttackSpeed : UpgradeCard
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
            equipment.attackSpeedModifier.AddModifier(Upgrade());
            if (equipmentManager.onEquipmentChanged != null)
                equipmentManager.onEquipmentChanged.Invoke(equipment, null);
        }
        else equipment.attackSpeedModifier.AddModifier(Upgrade());

        if (isMaxLevelReached) upgradeCardHolder.weapons.Remove(upgradeCardHolder.scatterAttackSpeed);

        playerLevelManager.OpenRewardPanel(false);
    }
}
