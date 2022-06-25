using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelManager : MonoBehaviour
{
    #region Singleton
    public static PlayerLevelManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    int playerLevel = 1;
    int maxExperience = 3;
    [SerializeField] AnimationCurve levelRate;
    float currentExp;
    float expModifier;

    public int choiseCount = 2;

    List<int> ShuffledIndex = new List<int>();

    [SerializeField] GameObject rewardPanel;
    [SerializeField] GameObject choiseParent;
    [SerializeField] GameObject[] rewardChoises = new GameObject[4];

    GameManager gameManager;

    GridLayoutGroup rewardGridLG;

    UpgradeCardHolder upgradeCardHolder;

    void Start()
    {
        gameManager = GameManager.instance;

        upgradeCardHolder = GetComponent<UpgradeCardHolder>();

        rewardGridLG = choiseParent.GetComponent<GridLayoutGroup>();
        rewardGridLG.constraintCount = 2;

        //StartCoroutine(TempLevelUp(3));

        //Debug.Log("2: " + Mathf.Round(levelRate.Evaluate(2)));
        //Debug.Log("3: " + Mathf.Round(levelRate.Evaluate(3)));
        //Debug.Log("4: " + Mathf.Round(levelRate.Evaluate(4)));
        //Debug.Log("5: " + Mathf.Round(levelRate.Evaluate(5)));
        //Debug.Log("6: " + Mathf.Round(levelRate.Evaluate(6)));
        //Debug.Log("7: " + Mathf.Round(levelRate.Evaluate(7)));
        //Debug.Log("8: " + Mathf.Round(levelRate.Evaluate(8)));
        //Debug.Log("9: " + Mathf.Round(levelRate.Evaluate(9)));
        //Debug.Log("10: " + Mathf.Round(levelRate.Evaluate(10)));
        //Debug.Log("35: " + Mathf.Round(levelRate.Evaluate(35)));
    }

    IEnumerator TempLevelUp(int loopCount)
    {
        for (int i = 0; i < loopCount; i++)
        {
            yield return new WaitForSeconds(5f);
            OpenRewardPanel(true);
        }
    }

    public void GainExp(float exp)
    {
        float bonusExp = exp * expModifier;
        float resultExp = exp + bonusExp;
        currentExp += resultExp;
        if (currentExp >= Mathf.Round(levelRate.Evaluate(playerLevel + 1)))
        {
            //Debug.Log(Mathf.Round(levelRate.Evaluate(playerLevel + 1)));
            
            playerLevel++;
            maxExperience += maxExperience / 5;
            currentExp = 0;

            OpenRewardPanel(true);
        }
    }

    #region Common Upgrades

    //UpgradeCard MaxHealth()//5 10 25
    //{
    //    playerStats.maxHealth.AddModifier(maxHealth.GetModifier());

    //    if (maxHealth.isMaxLevelReached)
    //    {
    //        CardsCommon -= MaxHealth;
    //    }

    //    //return maxHealth;
    //    return maxHealth;
    //}
    //void BodyRotate()//amount:4 15 25 50
    //{
    //    Debug.Log("+10 Rotate Speed");
    //}
    //void SwitchWeapon()//amount:4 +1
    //{
    //    Debug.Log("+10 Switch Weapon Speed");
    //}

    #endregion

    #region Weapon Upgrades
    //int starterDamageUpgrade = 1;
    //void StarterDamage()//1 2 4
    //{
    //    Debug.Log("+" + starterDamageUpgrade + " Starter Damage");
    //    CardsWeapon -= StarterDamage;
    //}
    //int starterProjectileUpgrade = 8;
    //void StarterProjectileSpeed()//8 12 20
    //{
    //    Debug.Log("+" + starterProjectileUpgrade + " Starter Projectile Speed");
    //    CardsWeapon -= StarterProjectileSpeed;
    //}
    //int shotgunDamageUpgrade = 1;
    //void ShotgunDamage()//2 3 6
    //{
    //    Debug.Log("+" + shotgunDamageUpgrade + " Shotgun Damage");
    //    CardsWeapon -= ShotgunDamage;
    //}
    //int shotgunProjectileUpgrade = 10;
    //void ShotgunProjectileSpeed()//10 16 24
    //{
    //    Debug.Log("+" + shotgunProjectileUpgrade + " Shotgun Projectile Speed");
    //    CardsWeapon -= ShotgunProjectileSpeed;
    //}
    #endregion
    
    #region commonR&D
    //void UpgradeMaxHealth()
    //{
    //    Debug.Log("Upgrade the Max Health");
    //}
    //void UpgradeBodyRotate()
    //{
    //    Debug.Log("Upgrade the Body Rotate");
    //}
    //#endregion
    
    //#region rareR&D 5% to 50% boss upgrades
    //void Experience()
    //{
    //    Debug.Log("%10 Experience");
    //}
    //void Choices()
    //{
    //    Debug.Log("+1 Choise");
    //}
    //void Supporter()
    //{
    //    Debug.Log("Gain Suporter with Health Regenation");
    //}
    #endregion
    
    public void OpenRewardPanel(bool isOpen)
    {
        if (isOpen)
        {
            int totalCard = upgradeCardHolder.weapons.Count + upgradeCardHolder.commons.Count;
            if (totalCard < 1) return;

            if (choiseCount > totalCard)
                choiseCount = totalCard;

            foreach (GameObject choice in rewardChoises)
            {
                choice.SetActive(false);
                choice.GetComponent<ChoiceCard>().RemoveCard();
            }

            upgradeCardHolder.AddChosenCardsToList(choiseCount);

            if (choiseCount == 4) rewardGridLG.constraintCount = 2;
            else rewardGridLG.constraintCount = 1;
            
            for (int i = 0; i < choiseCount; i++)
            {
                rewardChoises[i].SetActive(true);
                
                rewardChoises[i].GetComponent<ChoiceCard>().AddCard(upgradeCardHolder.choosenCards[i]);
            }

            rewardPanel.SetActive(true);
            gameManager.Pause();
        }
        else
        {
            rewardPanel.SetActive(false);
            gameManager.Resume();
        }
    }
}
