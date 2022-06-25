using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Kamikaze : MonoBehaviour
{
    CharacterStats myStats;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    public void Detonation(CharacterStats targetStats)
    {
        targetStats.TakeDamage(myStats.damage.GetValue());

        myStats.Die();
    }
}
