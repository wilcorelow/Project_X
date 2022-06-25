using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhaserProjectileSpeed", menuName = "Scriptable Objects/Upgrade Card/Phaser Projectile Speed")]
public class UpgradePhaserProjectileSpeed : UpgradeCard
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
            equipment.projectileSpeedModifier.AddModifier(Upgrade());
            if (equipmentManager.onEquipmentChanged != null)
                equipmentManager.onEquipmentChanged.Invoke(equipment, null);
        }
        else equipment.projectileSpeedModifier.AddModifier(Upgrade());

        if (isMaxLevelReached) upgradeCardHolder.weapons.Remove(upgradeCardHolder.phaserProjectileSpeed);

        playerLevelManager.OpenRewardPanel(false);
    }
}
