using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventsController : MonoBehaviour
{
    Animator animator;
    PlayerController playerController;

    int isArmLOpenHash = Animator.StringToHash("isArmLOpen");
    int isArmROpenHash = Animator.StringToHash("isArmROpen");
    int isTailOpenHash = Animator.StringToHash("isTailOpen");

    bool isArmLOpen;
    bool isArmROpen;
    bool isTailOpen;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = PlayerManager.Instance.player.GetComponent<PlayerController>();
    }

    void Start()
    {
        isArmLOpen = animator.GetBool(isArmLOpenHash);
        isArmROpen = animator.GetBool(isArmROpenHash);
        isTailOpen = animator.GetBool(isTailOpenHash);
    }

    /// <summary>
    /// Events in the last frame of the arm animations.
    /// </summary>
    public void EventsArmsLastFrame(TurretArm info)
    {
        if (info.position == ArmPosition.left && info.state == ArmState.open && isArmLOpen == false)
        {
            animator.SetBool(isArmLOpenHash, true);
            isArmLOpen = true;
            playerController.isArmLOpen = true;
        }
        else if (info.position == ArmPosition.left && info.state == ArmState.close && isArmLOpen == true)
        {
            animator.SetBool(isArmLOpenHash, false);
            isArmLOpen = false;
            playerController.isArmLOpen = false;
        }
        else if (info.position == ArmPosition.right && info.state == ArmState.open && isArmROpen == false)
        {
            animator.SetBool(isArmROpenHash, true);
            isArmROpen = true;
            playerController.isArmROpen = true;
        }
        else if (info.position == ArmPosition.right && info.state == ArmState.close && isArmROpen == true)
        {
            animator.SetBool(isArmROpenHash, false);
            isArmROpen = false;
            playerController.isArmROpen = false;
        }
        else if (info.position == ArmPosition.back && info.state == ArmState.open && isTailOpen == false)
        {
            animator.SetBool(isTailOpenHash, true);
            isTailOpen = true;
            playerController.isTailOpen = true;
        }
        else if (info.position == ArmPosition.back && info.state == ArmState.close && isTailOpen == true)
        {
            animator.SetBool(isTailOpenHash, false);
            isTailOpen = false;
            playerController.isTailOpen = false;
        }
    }

    public void ChangeArmsAnimBusyState(TurretArm info)
    {
        if (info.position == ArmPosition.left)
        {
            playerController.isArmLAnimBusy = false;
        }
        else if (info.position == ArmPosition.right)
        {
            playerController.isArmRAnimBusy = false;
        }
        else if (info.position == ArmPosition.back)
        {
            playerController.isTailAnimBusy = false;
        }
    }

    /// <summary>
    /// The events at the end/beginning of the Arms
    /// </summary>
    /// <param name="info"></param>
    public void ArmEvents (TurretArm info)
    {
        if (info.position == ArmPosition.left && info.state == ArmState.open)       //Opening left arm event at the end
        {
            playerController.PrimaryWeaponHolder_L.gameObject.SetActive(true);
            Animator selectedWeaponAnimator = playerController.PrimaryWeaponHolder_L.GetChild(playerController.selectedWeaponIndex).gameObject.GetComponent<Animator>();
            selectedWeaponAnimator.SetTrigger("open");
        }
        else if (info.position == ArmPosition.left && info.state == ArmState.close) //Closing left arm event at the beginning
        {
            playerController.PrimaryWeaponHolder_L.gameObject.SetActive(false);
        }
        else if (info.position == ArmPosition.right && info.state == ArmState.open) //Opening right arm event at the end
        {
            playerController.PrimaryWeaponHolder_R.gameObject.SetActive(true);
            Animator selectedWeaponAnimator = playerController.PrimaryWeaponHolder_R.GetChild(playerController.selectedWeaponIndex).gameObject.GetComponent<Animator>();
            selectedWeaponAnimator.SetTrigger("open");
        }
        else if (info.position == ArmPosition.right && info.state == ArmState.close)//Closing right arm event at the beginning
        {
            playerController.PrimaryWeaponHolder_R.gameObject.SetActive(false);
        }
        else if (info.position == ArmPosition.back && info.state == ArmState.open) //Opening back event at the end
        {
            playerController.SecondayWeaponHolder.gameObject.SetActive(true);
            Animator selectedWeaponAnimator = playerController.SecondayWeaponHolder.GetChild(0).gameObject.GetComponent<Animator>();
            selectedWeaponAnimator.SetTrigger("openFronts");
        }
        else if (info.position == ArmPosition.back && info.state == ArmState.close)//Closing back event at the beginning
        {
            playerController.SecondayWeaponHolder.gameObject.SetActive(false);
        }
    }
}
