using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Item item;
    public Button removeButton;
    public Button sellButton;
    public Image icon;

    bool isSellItem;
    bool isBuyItem;


    public void AddItem(Item newItem, bool closeButton = true, bool sellItem = false, bool buyItem = false)
    {
        if (newItem == null)
            return;

        item = newItem;
        isSellItem = sellItem;

        isBuyItem = buyItem;

        icon.sprite = item.icon;
        icon.enabled = true;


        if (closeButton)
            removeButton.interactable = true;
        if (sellItem)
            sellButton.interactable = true;

    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;

        if(sellButton!=null)
            sellButton.interactable = false;
    }

    public void SellItem()
    {
        if (Inventory.Instance.onItemSoldCallback != null)
            Inventory.Instance.onItemSoldCallback.Invoke((int)(item.BuyPrice / 2.0f));

        try
        {
            GameManager.instance.GetComponent<AudioSource>().Play();
        }
        catch
        {

        }

        Inventory.Instance.Remove(item);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            var panelUI = CanvasManager.Instance.infoPanel.transform;
            var uiSize = panelUI.GetComponent<RectTransform>().sizeDelta;
            var iconSize = icon.GetComponent<RectTransform>().sizeDelta;
            var iconPosition = icon.GetComponent<RectTransform>().position;

            //panelUI.position = new Vector2((iconPosition.x + iconSize.x) + uiSize.x / 2, (iconPosition.y - iconSize.y) - uiSize.y / 2);

            CanvasManager.UITextBindings["infoDamage"].text = (item as Equipment).damageModifier.ToString();
            CanvasManager.UITextBindings["infoDefence"].text = (item as Equipment).armorModifier.ToString();
            CanvasManager.UITextBindings["itemName"].text = (item as Equipment).name.ToString();

            if (isBuyItem)
            {
                CanvasManager.UITextBindings["shopSellBuy"].text = "Buy for: ";
                CanvasManager.UITextBindings["price"].text = (item as Equipment).BuyPrice.ToString();
            }
            if (isSellItem || (!isSellItem && !isBuyItem))
            {
                CanvasManager.UITextBindings["shopSellBuy"].text = "Sell for: ";
                CanvasManager.UITextBindings["price"].text = ((int)((item as Equipment).BuyPrice / 2.0f)).ToString();
            }

            CanvasManager.Instance.infoPanel.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CanvasManager.Instance.infoPanel.SetActive(false);
    }

    public void UseItem()
    {
        if (isBuyItem)
        {
            if (Inventory.Instance.onItemBuyCallback != null)
                Inventory.Instance.onItemBuyCallback.Invoke(item);
            return;
        }

        if (isSellItem) return;
        if (item != null)
        {
            item.Use();
            if (Inventory.Instance.onItemUsedCallback != null)
                Inventory.Instance.onItemUsedCallback.Invoke();
        }
    }
    public void OnRemoveButton()
    {
        Inventory.Instance.Remove(item);
    }
}
