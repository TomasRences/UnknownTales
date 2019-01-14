using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Text playersGoldText;
    Inventory inventory;
    public GameObject inventoryUI;

    InventorySlot[] slots;

    // Use this for initialization
    void Start()
    {
        inventory = Inventory.Instance;
        inventory.onItemChangedCallback += UpdateUI;
        inventory.onGoldChangedCallback += UpdateGold;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        inventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Inventory"))
        {
            if (!inventoryUI.activeSelf)
            {
                CanvasManager.Instance.UiOpened = true;
                PauseGame();
            }
            else
            {
                CanvasManager.Instance.infoPanel.SetActive(false);
                CanvasManager.Instance.UiOpened = false;
                ContinueGame();
            }

            var stats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

            StatsCopy.armor = stats.armor.GetBaseValue();
            StatsCopy.damage = stats.damage.GetBaseValue();
            StatsCopy.maxHealth = stats.maxHealth.GetBaseValue();
            StatsCopy.skillPoints = stats.skillPoints;


            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
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
        int curr = int.Parse(playersGoldText.text);
        playersGoldText.text = (curr + gold).ToString();
        inventory.Gold = curr + gold;
        CanvasManager.UITextBindings["shopGold"].text = Inventory.Instance.Gold.ToString();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                try
                {
                    slots[i].AddItem(inventory.items[i]);
                }
                catch { }
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
