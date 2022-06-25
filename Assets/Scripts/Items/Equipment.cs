using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public Stat damageModifier;
    public Stat attackSpeedModifier;
    public Stat projectileSpeedModifier;
    public Stat projectileRangeModifier;
    [Space]
    public Stat maxOverheat;
    public float coolingDownTime;
    public float currentOverheat;
    public bool isOverheated;
    public Stat heat;

    public override void Use()
    {
        //base.Use();
        RemoveFromInventory();
        EquipmentManager.instance.Equip(this);
    }

    public virtual void ResetStats()
    {
        damageModifier.RemoveAllModifiers();
        attackSpeedModifier.RemoveAllModifiers();
        projectileSpeedModifier.RemoveAllModifiers();
        projectileRangeModifier.RemoveAllModifiers();
        maxOverheat.RemoveAllModifiers();
        heat.RemoveAllModifiers();

        currentOverheat = 0;
        isOverheated = false;

    }
}

public enum EquipmentSlot { PrimaryWeapon, SecondayWeapon }
