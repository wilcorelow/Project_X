using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class MeleeCombat : MonoBehaviour
{
    CharacterStats myStats;
    public event System.Action OnAttack; // For the attack animation.

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    public void Attack(CharacterStats targetStats)
    {
        targetStats.TakeDamage(myStats.damage.GetValue());

        if (OnAttack != null)
            OnAttack();
    }
}
