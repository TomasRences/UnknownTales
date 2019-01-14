using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnGoldChanged(int gold);
    public OnGoldChanged onGoldChangedCallback;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public delegate void OnItemUsed();
    public OnItemUsed onItemUsedCallback;

    public delegate void OnItemSold(int gold);
    public OnItemSold onItemSoldCallback;

    public delegate void OnItemBuy(Item item);
    public OnItemBuy onItemBuyCallback;


    public int capacity;


    public static Inventory instance;
    public static Inventory Instance
    {
        get
        {
            return instance;
        }
    }

    void Start()
    {
        onItemSoldCallback += OnItemSoldCallback;
        onItemBuyCallback += OnItemBuyCallback;
    }

    void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Creating new Inventory");
            instance = this;
        }
    }

    public int Gold = 0;

    [SerializeField]
    public List<Item> items = new List<Item>();

    void OnItemBuyCallback(Item newItem)
    {
        if (Gold >= newItem.BuyPrice)
        {
            onGoldChangedCallback.Invoke(-1 * newItem.BuyPrice);

            ShopManager.Instance.Remove(newItem);
            Add(newItem);
            try
            {
                GameManager.instance.GetComponent<AudioSource>().Play();
            }
            catch
            {

            }
        }
        else
        {
            // not enought money
        }
    }

    void OnItemSoldCallback(int gold)
    {
        if (onGoldChangedCallback != null)
        {
            onGoldChangedCallback.Invoke(gold);
        }
    }

    public void Add(Item item)
    {

        if ((item.GetType() == typeof(Gold)))
        {
            if (onGoldChangedCallback != null)
            {
                onGoldChangedCallback.Invoke((item as Gold).value);
            }
        }
        else
        {
            if (!item.isDefaultItem)
            {
                items.Add(item);

                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();
                }
            }
        }
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
