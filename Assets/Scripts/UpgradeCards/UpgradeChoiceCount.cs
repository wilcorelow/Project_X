using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChoiceCount", menuName = "Scriptable Objects/Upgrade Card/Choice Count")]
public class UpgradeChoiceCount : UpgradeCard
{
    public override void Activate()
    {
        base.Activate();

        playerLevelManager.choiseCount += (int)Upgrade();

        if (isMaxLevelReached) upgradeCardHolder.commons.Remove(upgradeCardHolder.choiceCount);

        playerLevelManager.OpenRewardPanel(false);
    }
}
