using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Phaser", menuName = "Scriptable Objects/Weapons/Phaser")]
public class Phaser : Equipment
{
    [Tooltip("More accurate when close to 0.")]
    [SerializeField] Stat _accuracy;
    
    public float accuracy
    {
        get
        {
            float min = _accuracy.GetValue() * -1;

            float result = Random.Range(min, _accuracy.GetValue());
            
            return result;
        }

        set
        {
            _accuracy.AddModifier(value);
        }

    }

    public override void ResetStats()
    {
        base.ResetStats();

        _accuracy.RemoveAllModifiers();
    }
}
