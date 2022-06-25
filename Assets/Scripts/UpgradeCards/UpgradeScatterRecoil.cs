using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScatterRecoil", menuName = "Scriptable Objects/Upgrade Card/Scatter Recoil")]
public class UpgradeScatterRecoil : UpgradeCard
{
    public override void Activate()
    {
        base.Activate();

        ScatterManager scatterManager = player.GetComponent<ScatterManager>();
        Scatter equipment = (Scatter)scatterManager.equipment;

        if (player.GetComponent<PlayerController>().selectedWeaponManager.GetType() == scatterManager.GetType())
        {
            if (equipmentManager.onEquipmentChanged != null)
                equipmentManager.onEquipmentChanged.Invoke(null, equipment);
            equipment.recoil = Upgrade();
            if (equipmentManager.onEquipmentChanged != null)
                equipmentManager.onEquipmentChanged.Invoke(equipment, null);
        }
        else equipment.recoil = Upgrade();

        if (isMaxLevelReached) upgradeCardHolder.weapons.Remove(upgradeCardHolder.scatterRecoil);

        playerLevelManager.OpenRewardPanel(false);
    }
}
