using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Equipment equipment;

    protected PlayerController playerController;
    protected CharacterStats myStats;
    protected new Rigidbody rigidbody;

    [SerializeField] protected Pooler bulletPool;

    [SerializeField] protected Pooler muzzleFlashPool;
    float muzzleTime;

    WaitForSeconds CDTime;
    bool isCDOn;

    WaitForSeconds attackSpeed;
    [HideInInspector] public bool canShoot = true;

    void Start()
    {
        playerController = PlayerManager.instance.player.GetComponent<PlayerController>();
        myStats = playerController.GetComponent<PlayerStats>();
        rigidbody = playerController.GetComponent<Rigidbody>();

        CDTime = new WaitForSeconds(equipment.coolingDownTime);
        attackSpeed = new WaitForSeconds(equipment.attackSpeedModifier.GetValue());

        equipment.ResetStats();

        GameObject muzzle = muzzleFlashPool.GetObject();
        muzzleTime = muzzle.GetComponentInChildren<ParticleSystem>().main.startLifetime.constantMax;
        muzzleFlashPool.ReturnObject(muzzle);
    }

    public virtual void Shoot()
    {
        Heating();

        StartCoroutine(ICanShoot());
    }

    public void Heating()
    {
        equipment.currentOverheat += equipment.heat.GetValue();

        if (!equipment.isOverheated)
        {
            if (equipment.currentOverheat >= equipment.maxOverheat.GetValue())
            {
                StartCoroutine(nameof(IOverheated));
            }
            else if (!isCDOn)
            {
                StartCoroutine(nameof(ICoolingDown));
            }
        }
    }

    IEnumerator IOverheated()
    {
        equipment.isOverheated = true;

        StopCoroutine(nameof(ICoolingDown));

        yield return new WaitForSeconds(3f);

        CDTime = new WaitForSeconds(equipment.coolingDownTime * 0.2f);

        StartCoroutine(nameof(ICoolingDown));
    }

    IEnumerator ICoolingDown()
    {
        isCDOn = true;

        while (equipment.currentOverheat > 0)
        {
            equipment.currentOverheat--;
            yield return CDTime;
        }
        if (equipment.isOverheated)
        {
            equipment.isOverheated = false;

            CDTime = new WaitForSeconds(equipment.coolingDownTime);
        }
        isCDOn = false;
    }

    IEnumerator ICanShoot()
    {
        yield return attackSpeed;
        canShoot = true;
    }

    protected IEnumerator IMuzzleFlash(Transform barrelPoint)
    {
        GameObject muzzle = muzzleFlashPool.GetObject();
        muzzle.transform.position = barrelPoint.position;
        muzzle.transform.rotation = barrelPoint.rotation;
        muzzle.SetActive(true);
        yield return new WaitForSeconds(muzzleTime);
        muzzleFlashPool.ReturnObject(muzzle);
    }
}
