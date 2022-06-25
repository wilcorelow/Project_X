using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Variables
    public Transform camPosition;

    public Transform PrimaryWeaponHolder_L;
    public Transform PrimaryWeaponHolder_R;
    public Transform SecondayWeaponHolder;

    public Interactable focus;
    
    PlayerActions playerActions;

    PlayerInput playerInput;

    InputAction shootAction;

    CharacterStats myStats;
    
    Camera cam;

    Inventory inventory;

    Animator animator;

    WeaponAnimationController selectedWeaponController_L;
    WeaponAnimationController selectedWeaponController_R;

    public WeaponManager selectedWeaponManager;

    WeaponManager phaserManager;
    WeaponManager scatterManager;

    Equipment phaserEquipment;
    Equipment scatterEquipment;

    /// <summary>
    /// It will be used to rotate.
    /// </summary>
    public GameObject body;

    [HideInInspector] public int selectedWeaponIndex = 0;
    //int previousSelectedWeaponIndex;
    int oldSelected;

    float movement;

    float mWhell;
    
    public bool areWeaponsReady = false;

    public bool isheld = false;

    bool isSwitchFinished = true;

    //WaitForSeconds myAttackSpeed;

    [HideInInspector] public bool isArmLAnimBusy = false;
    [HideInInspector] public bool isArmRAnimBusy = false;
    [HideInInspector] public bool isTailAnimBusy = false;

    [HideInInspector] public bool isArmLOpen;
    [HideInInspector] public bool isArmROpen;
    [HideInInspector] public bool isTailOpen;

    [HideInInspector] public bool isTurretLAnimBusy = false;
    [HideInInspector] public bool isTurretRAnimBusy = false;

    Vector2 rotateValue;

    #endregion

    #region AnimatorHash
    int openArmLHash = Animator.StringToHash("openArmL");
    int openArmRHash = Animator.StringToHash("openArmR");
    int closeArmLHash = Animator.StringToHash("closeArmL");
    int closeArmRHash = Animator.StringToHash("closeArmR");
    int isArmLOpenHash = Animator.StringToHash("isArmLOpen");
    int isArmROpenHash = Animator.StringToHash("isArmROpen");
    int openTailHash = Animator.StringToHash("openTail");
    int closeTailHash = Animator.StringToHash("closeTail");
    int isTailOpenHash = Animator.StringToHash("isTailOpen");
    #endregion
    
    void Awake()
    {
        playerActions = new PlayerActions();

        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];

        cam = Camera.main;

        myStats = GetComponent<CharacterStats>();

        animator = GetComponentInChildren<Animator>();

        //myAttackSpeed = new WaitForSeconds(myStats.attackSpeed.GetValue());

        phaserManager = GetComponent<PhaserManager>();
        scatterManager = GetComponent<ScatterManager>();

        phaserEquipment = phaserManager.equipment;
        scatterEquipment = scatterManager.equipment;
    }
    void OnEnable()
    {
        playerActions.Enable();
    }
    void OnDisable()
    {
        playerActions.Disable();
    }
    void Start()
    {
        shootAction.started += _ => { isheld = true; PlayerShoot(); };
        //shootAction.performed += _ => { isheld = true; PlayerShoot(); };
        shootAction.canceled += _ => isheld = false;

        #region Inputs
        //playerActions.Collapsed.Shoot.started += _ => { isheld = true; PlayerShoot(); };
        //playerActions.Collapsed.Shoot.performed += _ => { isheld = true; IPlayerShoot(); };
        //playerActions.Collapsed.Shoot.canceled += _ => { isheld = false; };
        playerActions.Collapsed.Interact.started += _ => StartedClicked();
        playerActions.Collapsed.Interact.performed += _ => EndedClicked();
        playerActions.Collapsed.Inventory.performed += _ => OpenTheInventory();
        playerActions.Collapsed.TempUnequipItems.performed += _ => { EquipmentManager.instance.UnequipAll(); };
        playerActions.Collapsed.TempArmL.performed += _ => ArmLControl();
        playerActions.Collapsed.TempArmR.performed += _ => ArmRControl();
        playerActions.Collapsed.WeaponSwitchingWhell.performed += _ => WeaponSwichWhellControl();
        playerActions.Collapsed.WeaponSelectFirst.performed += _ => SelectFirstWeapon();
        playerActions.Collapsed.WeaponSelectSecond.performed += _ => SelectSecondWeapon();
        playerActions.Collapsed.WeaponSelectThird.performed += _ => SelectThirdWeapon();
        #endregion

        inventory = Inventory.instance;

        isArmLOpen = animator.GetBool(isArmLOpenHash);
        isArmROpen = animator.GetBool(isArmROpenHash);
        isTailOpen = animator.GetBool(isTailOpenHash);

        oldSelected = selectedWeaponIndex;

        Invoke("AddWeaponsToInventory", .1f);
        
        ArmLControl();
        ArmRControl();
        
        Invoke("ActivateSelectedWeapons", .1f);
    }

    void Update()
    {
        movement = playerActions.Collapsed.Movement.ReadValue<float>();

        mWhell = playerActions.Collapsed.WeaponSwitchingWhell.ReadValue<float>();
    }
    void FixedUpdate()
    {
        CollapsedMovement();
    }

    void PlayerShoot()
    {
        //Debug.Log(EventSystem.current.IsPointerOverGameObject());
        //Debug.Log(".");
        if (areWeaponsReady && !EventSystem.current.IsPointerOverGameObject())
        {
            if (selectedWeaponManager != null)
            {
                selectedWeaponManager.Shoot();
            }
        }
    }

    void CollapsedMovement()
    {
        rotateValue.y = movement;
        body.transform.Rotate(rotateValue * myStats.rotateSpeed.GetValue() * Time.deltaTime, Space.World);
    }

    void StartedClicked()
    {
        CursorManager.Instance.ChangeCursor(CursorManager.Instance.cursorClicked);
    }

    void EndedClicked()
    {
        CursorManager.Instance.ChangeCursor(CursorManager.Instance.cursor);
        DetectObject();
    }

    void OpenTheInventory()
    {
        inventory.inventoryUI.SetActive(!inventory.inventoryUI.activeSelf);
    }

    void AddWeaponsToInventory()
    {
        Inventory.instance.Add(phaserEquipment);

        Inventory.instance.Add(scatterEquipment);
    }
    
    void ActivateSelectedWeapons()
    {
        switch (selectedWeaponIndex)
        {
            case 0:
                PrimaryWeaponHolder_L.GetChild(0).gameObject.SetActive(true);
                PrimaryWeaponHolder_R.GetChild(0).gameObject.SetActive(true);

                phaserEquipment.Use();

                selectedWeaponManager = phaserManager;

                //myAttackSpeed = new WaitForSeconds(myStats.attackSpeed.GetValue());

                break;

            case 1:
                PrimaryWeaponHolder_L.GetChild(1).gameObject.SetActive(true);
                PrimaryWeaponHolder_R.GetChild(1).gameObject.SetActive(true);

                scatterEquipment.Use();

                selectedWeaponManager = scatterManager;

                //myAttackSpeed = new WaitForSeconds(myStats.attackSpeed.GetValue());

                break;
        }

    }

    void WeaponSwichWhellControl()
    {
        if (isSwitchFinished && !IsArmsAnimBusy())
        {
            oldSelected = selectedWeaponIndex;
            if (mWhell > 0f)
            {
                if (selectedWeaponIndex >= PrimaryWeaponHolder_R.childCount - 1)
                {
                    selectedWeaponIndex = 0;
                }
                else
                {
                    selectedWeaponIndex++;
                }
            }
            if (mWhell < 0f)
            {
                if (selectedWeaponIndex == 0)
                {
                    selectedWeaponIndex = PrimaryWeaponHolder_R.childCount - 1;
                }
                else
                {
                    selectedWeaponIndex--;
                }
            }
            if (oldSelected != selectedWeaponIndex)
            {
                StartCoroutine(ISwitchSelectedWeapon(oldSelected, selectedWeaponIndex));
            }
        }
    }

    void SelectFirstWeapon()
    {
        if (isSwitchFinished && !IsArmsAnimBusy())
        {
            if (PrimaryWeaponHolder_L.childCount >= 1 && PrimaryWeaponHolder_R.childCount >= 1)
            {
                oldSelected = selectedWeaponIndex;
                selectedWeaponIndex = 0;
                if (oldSelected != selectedWeaponIndex)
                {
                    StartCoroutine(ISwitchSelectedWeapon(oldSelected, selectedWeaponIndex));
                }
            }
        }
    }

    void SelectSecondWeapon()
    {
        if (isSwitchFinished && !IsArmsAnimBusy())
        {
            if (PrimaryWeaponHolder_L.childCount >= 2 && PrimaryWeaponHolder_R.childCount >= 2)
            {
                oldSelected = selectedWeaponIndex;
                selectedWeaponIndex = 1;
                if (oldSelected != selectedWeaponIndex)
                {
                    StartCoroutine(ISwitchSelectedWeapon(oldSelected, selectedWeaponIndex));
                }
            }
        }
    }

    void SelectThirdWeapon()
    {
        if (isSwitchFinished && !IsArmsAnimBusy())
        {
            if (PrimaryWeaponHolder_L.childCount >= 3 && PrimaryWeaponHolder_R.childCount >= 3)
            {
                oldSelected = selectedWeaponIndex;
                selectedWeaponIndex = 2;
                if (oldSelected != selectedWeaponIndex)
                {
                    StartCoroutine(ISwitchSelectedWeapon(oldSelected, selectedWeaponIndex));
                }
            }
        }
    }

    IEnumerator ISwitchSelectedWeapon(int oldIndex, int newIndex)
    {
        isSwitchFinished = false;

        isTurretLAnimBusy = true;
        isTurretRAnimBusy = true;
        Animator oldAnimatorL = PrimaryWeaponHolder_L.GetChild(oldIndex).GetComponent<Animator>();
        Animator oldAnimatorR = PrimaryWeaponHolder_R.GetChild(oldIndex).GetComponent<Animator>();
        oldAnimatorL.SetTrigger("close");
        oldAnimatorR.SetTrigger("close");
        areWeaponsReady = false;
        yield return new WaitWhile(() => oldAnimatorL.GetBool("isOpen") && oldAnimatorR.GetBool("isOpen"));

        ActivateSelectedWeapons();

        isTurretLAnimBusy = true;
        isTurretRAnimBusy = true;
        Animator newAnimatorL = PrimaryWeaponHolder_L.GetChild(newIndex).GetComponent<Animator>();
        Animator newAnimatorR = PrimaryWeaponHolder_R.GetChild(newIndex).GetComponent<Animator>();
        newAnimatorL.SetTrigger("open");
        newAnimatorR.SetTrigger("open");
        yield return new WaitUntil(() => newAnimatorL.GetBool("isOpen") && newAnimatorR.GetBool("isOpen"));

        isSwitchFinished = true;
    }

    bool IsArmsAnimBusy()
    {
        if (isArmLAnimBusy && isArmRAnimBusy)
        {
            return true;
        }
        else
            return false;
    }

    void ArmLControl()
    {
        if (!isArmLAnimBusy)
        {
            isArmLAnimBusy = true;
            if (!isArmLOpen)
            {
                animator.SetTrigger(openArmLHash);
            }
            else if (isArmLOpen)
            {
                animator.SetTrigger(closeArmLHash);
            }
        }
    }

    void ArmRControl()
    {
        if (!isArmRAnimBusy)
        {
            isArmRAnimBusy = true;
            if (!isArmROpen)
            {
                animator.SetTrigger(openArmRHash);
            }
            else if (isArmROpen)
            {
                animator.SetTrigger(closeArmRHash);
            }
        }
    }

    public void SentinelControl()
    {
        if (!isTailAnimBusy)
        {
            isTailAnimBusy = true;
            if (!isTailOpen)
            {
                animator.SetTrigger(openTailHash);
            }
            else if (isTailOpen)
            {
                //animator.SetTrigger(closeTailHash);
                SecondayWeaponHolder.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("closeFronts");
            }
        }
    }

    void DetectObject()
    {
        Ray ray = cam.ScreenPointToRay(playerActions.Collapsed.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                SetFocus(interactable);
                Debug.DrawLine(camPosition.position, hit.point, Color.red, 2f);
            }
            else
            {
                RemoveFocus();
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus) // If different focus..
        {
            if(focus != null)
                focus.onDefocused(); // Defocused the old focus
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
        focus.CheckDistanceAndInteract();
    }

    void RemoveFocus()
    {
        if(focus != null)
            focus.onDefocused();
        focus = null;
    }
}
