using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    private void Start()
    {
        //Debug.Log(item.name);
    }
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        try
        {
            GetComponent<AudioSource>().Play();
        }
        catch
        {

        }
        
        //Debug.Log("PickUp " + item.name);

        if (item.GetType() == typeof(Gold))
        {
            Inventory.Instance.Add(item);
            Destroy(gameObject);
        }
        else
        {

            if (Inventory.Instance.items.Count + 1 <= Inventory.Instance.capacity)
            {
                Inventory.Instance.Add(item);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory is full");
            }
        }
    }

}

