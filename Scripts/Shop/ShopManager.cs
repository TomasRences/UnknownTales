using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    public List<Item> items = new List<Item>();

    public static ShopManager instance;
    public static ShopManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Creating new ShopManager");
            instance = this;
        }
    }

    public void Add(Item item)
    {
        items.Add(item);

        /*/if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }*/
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
