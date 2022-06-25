using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhaserAccuracy", menuName = "Scriptable Objects/Upgrade Card/Phaser Accuracy")]
public class UpgradePhaserAccuracy : UpgradeCard
{
    public override void Activate()
    {
        base.Activate();

        PhaserManager phaserManager = player.GetComponent<PhaserManager>();
        Phaser equipment = (Phaser)phaserManager.equipment;

        if (player.GetComponent<PlayerController>().selectedWeaponManager.GetType() == phaserManager.GetType())
        {
            if (equipmentManager.onEquipmentChanged != null)
                equipmentManager.onEquipmentChanged.Invoke(null, equipment);
            equipment.accuracy = Upgrade();
            if (equipmentManager.onEquipmentChanged != null)
                equipmentManager.onEquipmentChanged.Invoke(equipment, null);
        }
        else equipment.accuracy = Upgrade();

        if (isMaxLevelReached) upgradeCardHolder.weapons.Remove(upgradeCardHolder.phaserDamage);

        playerLevelManager.OpenRewardPanel(false);
    }
}
