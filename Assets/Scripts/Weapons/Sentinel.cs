using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sentinel", menuName = "Scriptable Objects/Weapons/Sentinel")]
public class Sentinel : Item
{
    [SerializeField] Stat _healthRegenRate;
    public float healthRegenRate
    {
        get
        {
            return _healthRegenRate.GetValue();
        }

        set
        {
            _healthRegenRate.AddModifier(value);
            healthRegenTick = new WaitForSeconds(_healthRegenRate.GetValue());
        }
    }
    
    public Stat healthRegenValue;
    
    public WaitForSeconds healthRegenTick;

    public void ResetStat()
    {
        healthRegenValue.RemoveAllModifiers();
        _healthRegenRate.RemoveAllModifiers();
        healthRegenTick = new WaitForSeconds(_healthRegenRate.GetValue());
    }
}
