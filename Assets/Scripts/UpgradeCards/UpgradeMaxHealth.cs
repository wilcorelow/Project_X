using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaxHealth" , menuName = "Scriptable Objects/Upgrade Card/Max Health")]
public class UpgradeMaxHealth : UpgradeCard
{
    public override void Activate()
    {
        base.Activate();

        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        float modifier = Upgrade();
        playerStats.maxHealth = modifier;
        playerStats.currentHealth += modifier;

        if (isMaxLevelReached) upgradeCardHolder.commons.Remove(upgradeCardHolder.maxHealth);

        playerLevelManager.OpenRewardPanel(false);
    }
}
