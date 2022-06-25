using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCard : ScriptableObject
{
    public new string name;
    [Space]
    public float modifier;
    public int maxChoice;
    int currentChoice;

    protected bool isMaxLevelReached;

    protected GameObject player;

    protected UpgradeCardHolder upgradeCardHolder;

    protected PlayerLevelManager playerLevelManager;

    protected EquipmentManager equipmentManager;

    public void ResetValues()
    {
        currentChoice = maxChoice;
        isMaxLevelReached = false;
    }

    public string GetValueText()
    {
        if (modifier == 0) return null;
        string valueText;
        string plusOrMinus;

        plusOrMinus = Mathf.Sign(modifier) == 1 ? "+" : "";

        valueText = plusOrMinus + modifier.ToString();
        
        return valueText;
    }

    public float Upgrade()
    {
        currentChoice--;
        if (currentChoice < 1)
        {
            isMaxLevelReached = true;
        }
        //Debug.Log("current choise: " + currentChoice);
        
        return modifier;
    }

    public virtual void Activate()
    {
        player = PlayerManager.instance.player;
        upgradeCardHolder = UpgradeCardHolder.instance;
        playerLevelManager = PlayerLevelManager.instance;
        equipmentManager = EquipmentManager.instance;

        //Debug.Log(player + " has gained " + GetValueText() + " " + name);
    }
}
