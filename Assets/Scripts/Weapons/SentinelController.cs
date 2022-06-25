using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelController : MonoBehaviour
{
    #region Hash
    int openFontsHash = Animator.StringToHash("openFronts");
    int closeFontsHash = Animator.StringToHash("closeFronts");
    int openBacksHash = Animator.StringToHash("openBacks");
    int closeBacksHash = Animator.StringToHash("closeBacks");
    int isFrontsOpenHash = Animator.StringToHash("isFrontsOpen");
    int isBacksOpenHash = Animator.StringToHash("isBacksOpen");
    #endregion
    
    public bool isFrontsOpen;
    public bool isBacksOpen;

    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();

        isFrontsOpen = animator.GetBool(isFrontsOpenHash);
        isBacksOpen = animator.GetBool(isBacksOpenHash);
    }

    void SlowerLastFrameEvents(Slower info)
    {
        if (info.position == SlowerPosition.Fronts)
        {
            if (info.state == SlowerState.Open)
            {
                animator.SetBool(isFrontsOpenHash, true);
                isFrontsOpen = true;
            }
            else if (info.state == SlowerState.Close)
            {
                animator.SetBool(isFrontsOpenHash, false);
                isFrontsOpen = false;

                PlayerManager.instance.player.GetComponentInChildren<Animator>().SetTrigger("closeTail");
            }
        }
        else if (info.position == SlowerPosition.Backs)
        {
            if (info.state == SlowerState.Open)
            {
                animator.SetBool(isBacksOpenHash, true);
                isBacksOpen = true;
            }
            else if (info.state == SlowerState.Close)
            {
                animator.SetBool(isBacksOpenHash, false);
                isBacksOpen = false;
            }
        }
    }
}
