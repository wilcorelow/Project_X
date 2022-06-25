using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationController : MonoBehaviour
{
    Animator animator;
    PlayerController playerController;
    PlayerStats myStats;

    int isOpenHash = Animator.StringToHash("isOpen");

    protected bool isOpen;

    void Start()
    {
        myStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        UpdateAnimSpeed();
        playerController = PlayerManager.Instance.player.GetComponent<PlayerController>();
        isOpen = animator.GetBool("isOpen");
    }

    void LastFrameEvents(WeaponInfo info)
    {
        if (info.state == WeaponInfoState.Open && info.position == WeaponInfoPosition.Left)
        {
            animator.SetBool(isOpenHash, true);
            isOpen = true;
            playerController.isTurretLAnimBusy = false;
            if(!playerController.areWeaponsReady) playerController.areWeaponsReady = true;
        }
        else if (info.state == WeaponInfoState.Close && info.position == WeaponInfoPosition.Left)
        {
            animator.SetBool(isOpenHash, false);
            isOpen = false;
            playerController.isTurretLAnimBusy = false;
            gameObject.SetActive(false);
        }
        else if (info.state == WeaponInfoState.Open && info.position == WeaponInfoPosition.Right)
        {
            animator.SetBool(isOpenHash, true);
            isOpen = true;
            playerController.isTurretRAnimBusy = false;
            if(!playerController.areWeaponsReady) playerController.areWeaponsReady = true;
        }
        else if (info.state == WeaponInfoState.Close && info.position == WeaponInfoPosition.Right)
        {
            animator.SetBool(isOpenHash, false);
            isOpen = false;
            playerController.isTurretRAnimBusy = false;
            gameObject.SetActive(false);
        }
    }

    public void UpdateAnimSpeed()
    {
        animator.speed = myStats.swapWeaponSpeed.GetValue();
    }
}
