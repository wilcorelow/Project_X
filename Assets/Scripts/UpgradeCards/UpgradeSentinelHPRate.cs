using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SentinelHPRate", menuName = "Scriptable Objects/Upgrade Card/Sentinel HP Rate")]
public class UpgradeSentinelHPRate : UpgradeCard
{
    public override void Activate()
    {
        base.Activate();

        player.GetComponent<SentinelManager>().sentinel.healthRegenRate = Upgrade();

        if (isMaxLevelReached) upgradeCardHolder.weapons.Remove(upgradeCardHolder.sentinelHPRate);

        playerLevelManager.OpenRewardPanel(false);
    }
}
