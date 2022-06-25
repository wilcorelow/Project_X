using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BodyRotate", menuName = "Scriptable Objects/Upgrade Card/Body Rotate")]
public class UpgradeBodyRotate : UpgradeCard
{
    public override void Activate()
    {
        base.Activate();

        player.GetComponent<PlayerStats>().rotateSpeed.AddModifier(Upgrade());

        if (isMaxLevelReached) upgradeCardHolder.commons.Remove(upgradeCardHolder.bodyRotate);

        playerLevelManager.OpenRewardPanel(false);
    }
}
