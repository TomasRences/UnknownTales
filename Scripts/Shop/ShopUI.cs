using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Transform playerItemsParent;
    public Transform npcItemsParent;
    public Text playersGoldText;
    Inventory inventory;
    ShopManager shop;
    public GameObject shopUI;

    InventorySlot[] playerSlots;
    InventorySlot[] npcSlots;

    // Use this for initialization
    void Start()
    {
        shopUI.SetActive(true);
        shopUI.SetActive(false);

        shop = ShopManager.Instance;
        shop.onItemChangedCallback += UpdateNpcUI;

        inventory = Inventory.Instance;
        inventory.onItemChangedCallback += UpdatePlayerUI;
        //inventory.onGoldChangedCallback += UpdateGold;

        playerSlots = playerItemsParent.GetComponentsInChildren<InventorySlot>();
        npcSlots = npcItemsParent.GetComponentsInChildren<InventorySlot>();

        CanvasManager.UITextBindings["shopGold"].text = inventory.Gold.ToString();
        //shopUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Shop"))
        {
            ToggleShop();
        }
    }

    public void ToggleShop()
    {
        if (!shopUI.activeSelf)
        {
            PauseGame();
            UpdatePlayerUI();
            LoadTradersGoods();
            CanvasManager.Instance.UiOpened=true;
        }
        else
        {
            CanvasManager.Instance.UiOpened=false;
            ContinueGame();
            CanvasManager.Instance.infoPanel.SetActive(false);
        }

        shopUI.SetActive(!shopUI.activeSelf);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        //Disable scripts that still work while timescale is set to 0
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
    }

    void UpdateGold(int gold)
    {
        Debug.Log("gold: " + gold);
        Debug.Log("inventory.Gold: " + inventory.Gold);

        CanvasManager.UITextBindings["shopGold"].text = (inventory.Gold).ToString();
    }

    void LoadTradersGoods()
    {
        int j = 0;
        shop.items.Clear();
        for (int i = 0; i < 7; i++)
        {
            int k = 0;
            while (k < 3)
            {
                npcSlots[j].ClearSlot();
                Equipment generatedItem = null;

                switch (i)
                {
                    case 0:
                        {
                            generatedItem = EquipmentGenerator.GenerateRandomAmulet();
                            break;
                        }
                    case 1:
                        {
                            generatedItem = EquipmentGenerator.GenerateRandomArmor();
                            break;
                        }
                    case 2:
                        {
                            generatedItem = EquipmentGenerator.GenerateRandomHelmet();
                            break;
                        }
                    case 3:
                        {
                            generatedItem = EquipmentGenerator.GenerateRandomRing();
                            break;
                        }
                    case 4:
                        {
                            generatedItem = EquipmentGenerator.GenerateRandomShield();
                            break;
                        }
                    case 5:
                        {
                            generatedItem = EquipmentGenerator.GenerateRandomStone();
                            break;
                        }
                    case 6:
                        {
                            generatedItem = EquipmentGenerator.GenerateRandomWeapon();
                            break;
                        }
                }

                generatedItem.BuyPrice = Utilities.ComputePrice(generatedItem);
                shop.Add(generatedItem);
                npcSlots[j].AddItem(generatedItem, closeButton: false, buyItem: true);
                k++;
                j++;
            }
        }
    }

    void UpdateNpcUI()
    {
        for (int i = 0; i < npcSlots.Length; i++)
        {
            if (i < shop.items.Count)
            {
                npcSlots[i].AddItem(shop.items[i], closeButton: false, buyItem: true);
            }
            else
            {
                npcSlots[i].ClearSlot();
            }
        }
    }

    void UpdatePlayerUI()
    {
        for (int i = 0; i < playerSlots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                playerSlots[i].AddItem(inventory.items[i], closeButton: false, sellItem: true);
            }
            else
            {
                playerSlots[i].ClearSlot();
            }
        }
    }
}
