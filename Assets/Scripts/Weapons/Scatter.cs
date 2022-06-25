using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scatter", menuName = "Scriptable Objects/Weapons/Scatter")]
public class Scatter : Equipment
{
    [HideInInspector] public bool isRightFire;
    [SerializeField] Stat _recoil;
    public float recoil
    {
        get
        {
            if(isRightFire)
                return _recoil.GetValue() * 1;
            else
                return _recoil.GetValue() * -1;
        }
        set
        {
            _recoil.AddModifier(value);
        }
    }

    public override void ResetStats()
    {
        base.ResetStats();

        _recoil.RemoveAllModifiers();
        isRightFire = true;
    }
}
