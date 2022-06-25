using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhaserAttackSpeed", menuName = "Scriptable Objects/Upgrade Card/Phaser Attack Speed")]
public class UpgradePhaserAttackSpeed : UpgradeCard
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
            equipment.attackSpeedModifier.AddModifier(Upgrade());
            if (equipmentManager.onEquipmentChanged != null)
                equipmentManager.onEquipmentChanged.Invoke(equipment, null);
        }
        else equipment.attackSpeedModifier.AddModifier(Upgrade());

        if (isMaxLevelReached) upgradeCardHolder.weapons.Remove(upgradeCardHolder.phaserAttackSpeed);

        playerLevelManager.OpenRewardPanel(false);
    }
}
