using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhaserDamage", menuName = "Scriptable Objects/Upgrade Card/Phaser Damage")]
public class UpgradePhaserDamage : UpgradeCard
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
            equipment.damageModifier.AddModifier(Upgrade());
            if (equipmentManager.onEquipmentChanged != null)
                equipmentManager.onEquipmentChanged.Invoke(equipment, null);
        }
        else equipment.damageModifier.AddModifier(Upgrade());

        if (isMaxLevelReached) upgradeCardHolder.weapons.Remove(upgradeCardHolder.phaserDamage);

        playerLevelManager.OpenRewardPanel(false);
    }
}
