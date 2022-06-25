using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScatterDamage", menuName = "Scriptable Objects/Upgrade Card/Scatter Damage")]
public class UpgradeScatterDamage : UpgradeCard
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
            equipment.damageModifier.AddModifier(Upgrade());
            if (equipmentManager.onEquipmentChanged != null)
                equipmentManager.onEquipmentChanged.Invoke(equipment, null);
        }
        else equipment.damageModifier.AddModifier(Upgrade());

        if (isMaxLevelReached) upgradeCardHolder.weapons.Remove(upgradeCardHolder.scatterDamage);

        playerLevelManager.OpenRewardPanel(false);
    }
}
