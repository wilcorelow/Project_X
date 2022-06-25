using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterManager : WeaponManager
{
    [SerializeField] Transform barrelPoint_L1;
    [SerializeField] Transform barrelPoint_L2;
    [SerializeField] Transform barrelPoint_R1;
    [SerializeField] Transform barrelPoint_R2;

    [SerializeField] GameObject light_L;
    [SerializeField] GameObject light_R;

    [HideInInspector] public Scatter weapon;

    [HideInInspector] public int shootCount = 0;

    void Awake()
    {
        weapon = (Scatter)equipment;
        
        light_R.SetActive(true);
        light_L.SetActive(false);
    }

    public override void Shoot()
    {
        if (!canShoot) return;

        if (equipment.isOverheated)
        {
            Debug.Log(equipment.name + " is overheated!");
            return;
        }

        ScatterShoot();

        shootCount++;
        if (shootCount >= 2)
        {
            shootCount = 0;
            canShoot = false;
        }

        base.Shoot();
    }

    void ScatterShoot()
    {
        if (weapon.isRightFire)
        {
            GameObject bullet3 = bulletPool.GetObject();
            bullet3.GetComponent<BulletController>().Setup(barrelPoint_R1, equipment, myStats);
            StartCoroutine(IMuzzleFlash(barrelPoint_R1));

            GameObject bullet4 = bulletPool.GetObject();
            bullet4.GetComponent<BulletController>().Setup(barrelPoint_R2, equipment, myStats);
            StartCoroutine(IMuzzleFlash(barrelPoint_R2));

            BodyRecoil();
        }
        else
        {
            GameObject bullet = bulletPool.GetObject();
            bullet.GetComponent<BulletController>().Setup(barrelPoint_L1, equipment, myStats);
            StartCoroutine(IMuzzleFlash(barrelPoint_L1));

            GameObject bullet2 = bulletPool.GetObject();
            bullet2.GetComponent<BulletController>().Setup(barrelPoint_L2, equipment, myStats);
            StartCoroutine(IMuzzleFlash(barrelPoint_L1));

            BodyRecoil();
        }
        weapon.isRightFire = !weapon.isRightFire;
        Lighting();
    }
    public void BodyRecoil()
    {
        rigidbody.AddTorque(transform.up * weapon.recoil, ForceMode.VelocityChange);
    }

    void Lighting()
    {
        if (weapon.isRightFire)
        {
            light_R.SetActive(true);
            light_L.SetActive(false);
        }
        else
        {
            light_R.SetActive(false);
            light_L.SetActive(true);
        }
    }
}
