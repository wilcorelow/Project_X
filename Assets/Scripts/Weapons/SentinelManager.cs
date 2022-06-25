using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelManager : MonoBehaviour
{
    bool isPowerActive = false;

    [HideInInspector] public bool isHealthRegenerationAchieved;
    
    //public Equipment equipment;
    public Sentinel sentinel;

    [SerializeField] SentinelController secondaryController;

    CharacterStats playerStat;

    void Start()
    {
        playerStat = PlayerManager.instance.player.GetComponent<PlayerStats>();

        sentinel.ResetStat();
    }

    public IEnumerator IHealthRegenation()
    {
        if (isHealthRegenerationAchieved)
        {
            if (!isPowerActive && playerStat.currentHealth < playerStat._maxHealth.GetValue())
            {
                isPowerActive = true;
                while(playerStat.currentHealth < playerStat._maxHealth.GetValue())
                {
                    playerStat.currentHealth += sentinel.healthRegenValue.GetValue();
                    //playerStat.healthbar.SetHealth((int)playerStat.currentHealth);
                    yield return sentinel.healthRegenTick;
                }
                isPowerActive = false;
            }
        }
    }
}
