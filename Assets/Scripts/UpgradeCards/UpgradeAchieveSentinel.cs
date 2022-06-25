using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Achieve Sentinel", menuName = "Scriptable Objects/Upgrade Card/Achieve Sentinel")]
public class UpgradeAchieveSentinel : UpgradeCard
{
    public override void Activate()
    {
        base.Activate();

        SentinelManager secondaryPower = player.GetComponent<SentinelManager>();
        //Equipment equipment = secondaryPower.equipment;
        PlayerController playerController = player.GetComponent<PlayerController>();

        secondaryPower.isHealthRegenerationAchieved = true;

        playerController.SentinelControl();

        upgradeCardHolder.weapons.Remove(upgradeCardHolder.achieveSentinel);
        upgradeCardHolder.sentinelHPRate.ResetValues();
        upgradeCardHolder.weapons.Add(upgradeCardHolder.sentinelHPRate);

        //if player has damaged activate the power without waiting to taking damage
        secondaryPower.StartCoroutine(secondaryPower.IHealthRegenation());

        // add upgrades of the sentinel

        playerLevelManager.OpenRewardPanel(false);
    }
}
