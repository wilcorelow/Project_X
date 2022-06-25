using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    
    void Awake()
    {
        instance = this;
    }
    #endregion

    Equipment[] currentEquipments;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipments = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;     // learn slot index of the new item

        Equipment oldItem = Unequip(slotIndex);

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, null);
        }

        currentEquipments[slotIndex] = newItem;     // finally equip the new item
    }

    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipments[slotIndex] != null)
        {
            Equipment oldItem = currentEquipments[slotIndex];
            inventory.Add(oldItem);

            currentEquipments[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            return oldItem;
        }
        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipments.Length; i++)
        {
            Unequip(i);
        }
    }
}
