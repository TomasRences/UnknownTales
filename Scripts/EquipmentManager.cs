using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    public static EquipmentManager instance;
    public static EquipmentManager Instance { get { return instance; } }


    void Awake()
    {
        Debug.Log("EquipmentManager awaked");
        if (instance == null)
        {
            instance = this;
        }

        inventory = Inventory.Instance;
        canvasManager = CanvasManager.Instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public Equipment[] GetCurrentEquipment()
    {
        return currentEquipment;
    }

    public void SetCurrentEquipment(Equipment[] equipment)
    {
        currentEquipment = equipment;
    }

    Equipment[] currentEquipment;

    Inventory inventory;
    CanvasManager canvasManager;

    private void Start()
    {


    }

    public void SetEquipmentSlot(Equipment item)
    {
        switch (item.equipSlot)
        {
            case EquipmentSlot.Head:
                {
                    CanvasManager.Instance.headSlot.GetComponent<InventorySlot>().AddItem(item, false);
                    break;
                }
            case EquipmentSlot.Weapon:
                {
                    CanvasManager.Instance.weaponSlot.GetComponent<InventorySlot>().AddItem(item, false);
                    break;
                }
            case EquipmentSlot.Chest:
                {
                    CanvasManager.Instance.armorSlot.GetComponent<InventorySlot>().AddItem(item, false);
                    break;
                }
            case EquipmentSlot.Ring:
                {
                    CanvasManager.Instance.ringSlot.GetComponent<InventorySlot>().AddItem(item, false);
                    break;
                }
            case EquipmentSlot.Amulet:
                {
                    CanvasManager.Instance.amuletSlot.GetComponent<InventorySlot>().AddItem(item, false);
                    break;
                }
            case EquipmentSlot.Shield:
                {
                    CanvasManager.Instance.shieldSlot.GetComponent<InventorySlot>().AddItem(item, false);
                    break;
                }
            case EquipmentSlot.Stone:
                {
                    CanvasManager.Instance.stoneSlot.GetComponent<InventorySlot>().AddItem(item, false);
                    break;
                }
            default: break;
        }
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment old = null;

        if (currentEquipment[slotIndex] != null)
        {
            //Debug.Log("inventory: "+inventory.capacity);
            old = currentEquipment[slotIndex];
            Inventory.Instance.Add(old);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, old);
        }

        currentEquipment[slotIndex] = newItem;
        SetEquipmentSlot(newItem);
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment old = currentEquipment[slotIndex];
            Inventory.Instance.Add(old);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, old);
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

        CanvasManager.Instance.headSlot.GetComponent<InventorySlot>().ClearSlot();
        CanvasManager.Instance.weaponSlot.GetComponent<InventorySlot>().ClearSlot();
        CanvasManager.Instance.armorSlot.GetComponent<InventorySlot>().ClearSlot();
        CanvasManager.Instance.ringSlot.GetComponent<InventorySlot>().ClearSlot();
        CanvasManager.Instance.amuletSlot.GetComponent<InventorySlot>().ClearSlot();
        CanvasManager.Instance.shieldSlot.GetComponent<InventorySlot>().ClearSlot();
        CanvasManager.Instance.stoneSlot.GetComponent<InventorySlot>().ClearSlot();

        if (PlayerManager.Instance.Player.GetComponent<PlayerStats>().onStatChangedCallback != null)
            PlayerManager.Instance.Player.GetComponent<PlayerStats>().onStatChangedCallback.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
