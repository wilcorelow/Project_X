using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    SentinelManager secondaryPower;

    public Stat swapWeaponSpeed;

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        secondaryPower = GetComponent<SentinelManager>();
    }

    public void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            damage.AddModifier(newItem.damageModifier.GetValue());
            attackSpeed.AddModifier(newItem.attackSpeedModifier.GetValue());
            projectileSpeed.AddModifier(newItem.projectileSpeedModifier.GetValue());
        }
        
        if (oldItem != null)
        {
            damage.RemoveModifier(oldItem.damageModifier.GetValue());
            attackSpeed.RemoveModifier(oldItem.attackSpeedModifier.GetValue());
            projectileSpeed.RemoveModifier(oldItem.projectileSpeedModifier.GetValue());
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        StartCoroutine(secondaryPower.IHealthRegenation());
        
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
}
