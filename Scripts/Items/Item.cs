using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    new public string name = "New Item";

    [SerializeField]
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public int SellPrice=0;
    public int BuyPrice=10;


    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.Instance.Remove(this);
    }

}