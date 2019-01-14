using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.Instance.Equip(this);
        RemoveFromInventory();
    }

}

public class EquipmentProxy
{
    public EquipmentSlot equipSlot;
    public int armorModifier;
    public int damageModifier;
    public string name;

    [SerializeField]
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public int SellPrice = 0;
    public int BuyPrice = 10;

    public EquipmentProxy(Equipment eq)
    {
        this.equipSlot = eq.equipSlot;
        this.armorModifier = eq.armorModifier;
        this.damageModifier = eq.damageModifier;
        this.name = eq.name;
        this.icon = eq.icon;
        this.isDefaultItem = eq.isDefaultItem;
        this.SellPrice = eq.SellPrice;
        this.BuyPrice = eq.BuyPrice;
    }

	public Equipment ToEquipment()
    {
		Equipment eq=new Equipment();
        eq.equipSlot = equipSlot;
        eq.armorModifier = armorModifier;
        eq.damageModifier = damageModifier;
        eq.name = name;
        eq.icon = icon;
        eq.isDefaultItem = isDefaultItem;
        eq.SellPrice = SellPrice;
        eq.BuyPrice = BuyPrice;
		return eq;
    }
}

public enum EquipmentSlot { Head, Chest, Ring, Weapon, Shield, Amulet, Stone }