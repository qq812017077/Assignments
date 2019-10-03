using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryContainer : Container
{
    public HandleWindow equipmentWindow;

    public UnityEngine.UI.Text coinData;
    private int coins;
    public int Coins { get { return coins; } }
    private void Start()
    {
        ShowContent();
        FreshCoin();
        Open();
    }

    public void Use(GameObject slotGO) {
        ItemSO itemSO = slotGO.GetComponent<Slot>().m_ItemSO;
        switch (itemSO.behaviour) {
            case Behaviour.Eat:
                break;
            case Behaviour.Equipment:
                if (equipmentWindow == null) return;
                equipmentWindow.Open(itemSO);
                break;
            case Behaviour.Throw:
                break;
        }
    }

    public void BuyItem(ItemSO itemSO, Container targetContainer) {
        if (targetContainer is StoreContainer) {
            AddItem(itemSO);
            targetContainer.ReduceItem(itemSO);
            Database.instance.Consume(itemSO.Price);
            FreshCoin();
        }
    }

    protected override void OnClick(BaseEventData pointData)
    {
        Debug.Log(pointData.selectedObject);
        Slot slot = pointData.selectedObject.GetComponent<Slot>();
        Debug.Log(slot.m_GOName.text);
        Use(pointData.selectedObject);
    }

    void FreshCoin() {
        if (coinData == null) return;
        coins = Database.instance.playerCoin.coins;
        coinData.text = "Coin:"+ coins.ToString();
    }
}
