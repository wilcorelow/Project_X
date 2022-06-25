using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhaserHeat", menuName = "Scriptable Objects/Upgrade Card/Phaser Heat")]
public class UpgradePhaserHeat : UpgradeCard
{
    public override void Activate()
    {
        base.Activate();

        PhaserManager phaserManager = player.GetComponent<PhaserManager>();
        Equipment equipment = phaserManager.equipment;

        if (player.GetComponent<PlayerController>().selectedWeaponManager.GetType() == phaserManager.GetType())
        {
            if (equipmentManager.onEquipmentChanged != null)
                equipmentManager.onEquipmentChanged.Invoke(null, equipment);
            equipment.heat.AddModifier(Upgrade());
            if (equipmentManager.onEquipmentChanged != null)
                equipmentManager.onEquipmentChanged.Invoke(equipment, null);
        }
        else equipment.heat.AddModifier(Upgrade());

        if (isMaxLevelReached) upgradeCardHolder.weapons.Remove(upgradeCardHolder.phaserHeat);

        playerLevelManager.OpenRewardPanel(false);
    }
}
