using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class RangedAttack : MonoBehaviour
{
    CharacterStats myStats;

    [SerializeField] Transform barrelPoint;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }
    public void Shoot()
    {
        GameObject bullet = transform.parent.parent.GetComponent<Pooler>().GetObject();
        
        bullet.GetComponent<EnemyBulletController>().Setup(barrelPoint, myStats);
    }
}
