using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class HandleWindow : MonoBehaviour
{
    public enum EOperationType
    {
        Equip,
        UnEquip,
        Buy,
        Sell
    }

    public EOperationType curOperationType = EOperationType.Equip;
    public Image equipmentItemIcon;
    public Text equipmentItemName;
    public InventoryContainer inventoryContainer;
    public EquipmentContainer equipmentContainer;
    public StoreContainer storeContainer;
    public ItemSO currentItem;

    [SerializeField] private Button ctrlBtn;
    public void Open(ItemSO selectedItemSO , EOperationType targetOperation = EOperationType.Equip) {
        if (ctrlBtn == null)
        {
            ctrlBtn = GetComponentInChildren<Button>();
            if (ctrlBtn == null) {
                Debug.LogError("There is no Button in Target!");
                return;
            }
        }

        curOperationType = targetOperation;
        currentItem = selectedItemSO;
        gameObject.SetActive(true);
        if (currentItem != null) {
            equipmentItemIcon.sprite = currentItem.Icon;
            equipmentItemName.text = currentItem.Name;
        }
        ctrlBtn.enabled = true;
        switch (curOperationType) {
            case EOperationType.Equip:
                ctrlBtn.GetComponentInChildren<Text>().text = "Confirm";
                break;
            case EOperationType.UnEquip:
                ctrlBtn.GetComponentInChildren<Text>().text = "Remove";
                break;
            case EOperationType.Buy:
                ctrlBtn.GetComponentInChildren<Text>().text = "Buy";
                if (inventoryContainer.Coins < currentItem.Price) {
                    ctrlBtn.enabled = false;
                }
                break;
            case EOperationType.Sell:
                ctrlBtn.GetComponentInChildren<Text>().text = "Sell";
                break;
        }
    }

    public void Use() {
        switch (curOperationType)
        {
            case EOperationType.Equip:
                equipmentContainer.EquipItem(currentItem);
                break;
            case EOperationType.UnEquip:
                equipmentContainer.UnEquipItem(currentItem);
                break;
            case EOperationType.Buy:
                inventoryContainer.BuyItem(currentItem, storeContainer);
                storeContainer.Refresh();
                break;
            case EOperationType.Sell:
                break;
        }
        inventoryContainer.Refresh();
        Close();
    }

    public void Close() {
        gameObject.SetActive(false);
    }
}
