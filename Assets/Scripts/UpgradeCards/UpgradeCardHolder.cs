using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCardHolder : MonoBehaviour
{
    #region Singleton
    public static UpgradeCardHolder instance;

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

    #region Upgrade Cards
    public UpgradeCard maxHealth;
    public UpgradeCard bodyRotate;
    public UpgradeCard weaponSwitchSpeed;
    
    public UpgradeCard phaserDamage;
    public UpgradeCard phaserProjectileSpeed;
    public UpgradeCard phaserAttackSpeed;
    public UpgradeCard phaserHeat;
    public UpgradeCard phaserAccuracy;

    public UpgradeCard scatterDamage;
    public UpgradeCard scatterProjectileSpeed;
    public UpgradeCard scatterAttackSpeed;
    public UpgradeCard scatterRecoil;
    public UpgradeCard scatterHeat;

    public UpgradeCard achieveSentinel;
    public UpgradeCard sentinelHPRate;
    public UpgradeCard choiceCount;
    #endregion

    [HideInInspector] public List<UpgradeCard> commons = new List<UpgradeCard>();
    [HideInInspector] public List<UpgradeCard> weapons = new List<UpgradeCard>();

    [HideInInspector] public List<UpgradeCard> choosenCards = new List<UpgradeCard>();

    List<int> ShuffledNumbers = new List<int>();

    int commonRollCount;
    int weaponRollCount;
    
    void Start()
    {
        #region Card Adding
        commons.Add(maxHealth);
        commons.Add(bodyRotate);
        commons.Add(weaponSwitchSpeed);
        commons.Add(choiceCount);

        weapons.Add(phaserDamage);
        weapons.Add(phaserProjectileSpeed);
        weapons.Add(phaserAttackSpeed);
        weapons.Add(phaserHeat);
        weapons.Add(phaserAccuracy);

        weapons.Add(scatterDamage);
        weapons.Add(scatterProjectileSpeed);
        weapons.Add(scatterAttackSpeed);
        weapons.Add(scatterRecoil);
        weapons.Add(scatterHeat);

        weapons.Add(achieveSentinel);
        #endregion

        foreach (UpgradeCard card in commons)
        {
            card.ResetValues();
        }

        foreach (UpgradeCard card in weapons)
        {
            card.ResetValues();
        }
    }

    /// <summary>
    /// Which card are chosen?
    /// </summary>
    /// <param name="totalCount">count of list</param>
    /// <param name="generateCount"></param>
    void GetShuffledRNG(int totalCount, int generateCount)
    {
        ShuffledNumbers.Clear();
        
        List<int> NumberBag = new List<int>();
        
        for (int i = 0; i < totalCount; i++)
        {
            NumberBag.Add(i);
        }
        
        for (int i = 0; i < generateCount; i++)
        {
            int generatedNumber = NumberBag[Random.Range(0, NumberBag.Count)];
            NumberBag.Remove(generatedNumber);
            ShuffledNumbers.Add(generatedNumber);
        }
    }

    /// <summary>
    /// Which card type selected?
    /// </summary>
    /// <param name="choiseCount"></param>
    void RollReward(int choiseCount)
    {
        commonRollCount = 0;
        weaponRollCount = 0;

        int weaponChance = 40;

        for (int i = 0; i < choiseCount; i++) 
        {
            int result = UnityEngine.Random.Range(0, 101);
            //Debug.Log("result " + result);

            if (result <= weaponChance)
            {
                if (weaponRollCount < weapons.Count) weaponRollCount++;
                else if (commonRollCount < commons.Count) commonRollCount++;
            }
            else
            {
                if (commonRollCount < commons.Count) commonRollCount++;
                else if (weaponRollCount < weapons.Count) weaponRollCount++;
            } 
        }
    }

    public void AddChosenCardsToList(int choiseCount)
    {
        choosenCards.Clear();

        RollReward(choiseCount);

        if (weaponRollCount > 0)
        {
            if (weapons.Count == 1)
            {
                choosenCards.Add(weapons[0]);
            }
            else
            {
                GetShuffledRNG(weapons.Count, weaponRollCount);

                for (int i = 0; i < ShuffledNumbers.Count; i++)
                {
                    choosenCards.Add(weapons[ShuffledNumbers[i]]);
                }
            }
        }

        if (commonRollCount > 0)
        {
            if (commons.Count == 1)
            {
                choosenCards.Add(commons[0]);
            }
            else
            {
                GetShuffledRNG(commons.Count, commonRollCount);

                for (int i = 0; i < ShuffledNumbers.Count; i++)
                {
                    choosenCards.Add(commons[ShuffledNumbers[i]]);
                }
            }
        }
    }
}
