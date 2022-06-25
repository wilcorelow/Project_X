using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSwitchSpeed", menuName = "Scriptable Objects/Upgrade Card/Weapon Switch Speed")]
public class UpgradeWeaponSwitchSpeed : UpgradeCard
{
    public override void Activate()
    {
        base.Activate();

        player.GetComponent<PlayerStats>().swapWeaponSpeed.AddModifier(Upgrade());

        PlayerController playerController = player.GetComponent<PlayerController>();
        foreach (WeaponAnimationController item in playerController.PrimaryWeaponHolder_L.GetComponentsInChildren<WeaponAnimationController>())
        {
            item.UpdateAnimSpeed();
        }
        foreach (WeaponAnimationController item in playerController.PrimaryWeaponHolder_R.GetComponentsInChildren<WeaponAnimationController>())
        {
            item.UpdateAnimSpeed();
        }

        if (isMaxLevelReached) upgradeCardHolder.commons.Remove(upgradeCardHolder.weaponSwitchSpeed);

        playerLevelManager.OpenRewardPanel(false);
    }
}
