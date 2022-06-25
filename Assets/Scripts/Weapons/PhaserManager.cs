using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserManager : WeaponManager
{
    [SerializeField] Transform barrelPoint_L;
    [SerializeField] Transform barrelPoint_R;

    [HideInInspector] public Phaser weapon;

    void Awake()
    {
        weapon = (Phaser)equipment;
    }

    public override void Shoot()
    {
        if (!canShoot) return;
        
        StartCoroutine(IPhaserShoot());
    }

    IEnumerator IPhaserShoot()
    {
        while (playerController.isheld && canShoot)
        {
            if (equipment.isOverheated)
            {
                Debug.Log(equipment.name + " is overheated!");
                break;
            }

            #region accuracy
            barrelPoint_L.localRotation = Quaternion.Euler(0, 0, 0);
            barrelPoint_R.localRotation = Quaternion.Euler(0, 0, 0);
            barrelPoint_L.Rotate(0, weapon.accuracy, 0);
            barrelPoint_R.Rotate(0, weapon.accuracy, 0);
            #endregion

            GameObject bullet = bulletPool.GetObject();
            bullet.GetComponent<BulletController>().Setup(barrelPoint_L, equipment, myStats);
            StartCoroutine(IMuzzleFlash(barrelPoint_L));
            
            GameObject bullet2 = bulletPool.GetObject();
            bullet2.GetComponent<BulletController>().Setup(barrelPoint_R, equipment, myStats);
            StartCoroutine(IMuzzleFlash(barrelPoint_R));

            canShoot = false;
            base.Shoot();
            
            yield return new WaitUntil(() => canShoot);
        }
    }
}
