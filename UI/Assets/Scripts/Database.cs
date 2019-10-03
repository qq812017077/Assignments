using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database :MonoBehaviour
{
    public static Database instance;
    public static Database GetInstance() { return instance; }
    private ItemSO[] itemSOs;
    [SerializeField]
    public ItemList inventory;
    public ItemList store;
    public ItemList equipment;
    public Data playerCoin;

    private void Awake()
    {
        if (instance != this) Destroy(this);
        instance = this;
        itemSOs = Resources.LoadAll<ItemSO>("Items");
    }

    public ItemSO GetItemSODetail(Item item) {
        for (int i = 0; i < itemSOs.Length; i++) {
            if (itemSOs[i].Name == item.ItemName) {
                return itemSOs[i];
            }
        }
        return null;
    }

    #region Inventory Methods
    public void AddInventoryItem(Item _item)
    {
        foreach (Item item in inventory.itemList)
        {
            if (item.ItemName == _item.ItemName)
            {
                item.stack += _item.stack;
                return;
            }
        }
        //没有类似的
        inventory.itemList.Add(_item);
        return;
    }

    public void AddInventoryItem(ItemSO _itemSO)
    {
        foreach (Item item in inventory.itemList)
        {
            if (item.ItemName == _itemSO.Name)
            {
                item.stack += 1;
                return;
            }
        }
        //没有类似的
        inventory.itemList.Add(new Item(_itemSO.Name, 1));
        return;
    }
    public void ReduceInventoryItem(Item _item)
    {
        foreach (Item item in inventory.itemList)
        {
            if (item.ItemName == _item.ItemName)
            {
                item.stack -= _item.stack;
                if (item.stack <= 0)
                    inventory.itemList.Remove(item);
                return;
            }
        }
        return;
    }

    
    public void ReduceInventoryItem(ItemSO _itemSO)
    {
        ReduceInventoryItem(new Item(_itemSO));
        return;
    }
    #endregion

    #region Store Methods
    public void AddStoreItem(Item _item)
    {
        foreach (Item item in store.itemList)
        {
            if (item.ItemName == _item.ItemName)
            {
                if(item.stack > 0)
                    item.stack += _item.stack;
                return;
            }
        }

        //没有类似的
        _item.stack = -1;
        inventory.itemList.Add(_item);
        return;
    }
    public void AddStoreItem(ItemSO _itemSO)
    {
        AddStoreItem(new Item(_itemSO));
        return;
    }
    public void ReduceStoreItem(Item _item) {
        foreach (Item item in store.itemList)
        {
            if (item.ItemName == _item.ItemName)
            {
                if (item.stack < 0)
                    return;
                item.stack -= _item.stack;
                if (item.stack <= 0)
                    store.itemList.Remove(item);
                return;
            }
        }
    }

    public void ReduceStoreItem(ItemSO _itemSO)
    {
        ReduceStoreItem(new Item(_itemSO));
        return;
    }
    #endregion Store Methods

    #region Equipment Methods
    public bool ReplaceEquipItem(ItemSO itemSO)
    {
        foreach (var equipmentItem in equipment.itemList)
        {
            ItemSO equipmentItemSO = GetItemSODetail(equipmentItem);
            if (equipmentItemSO == null) continue;
            if (equipmentItemSO.localTo == itemSO.localTo)
            {
                equipment.itemList.Remove(equipmentItem);
                equipment.itemList.Add(new Item(itemSO));
                AddInventoryItem(equipmentItem);
                ReduceInventoryItem(itemSO);
                return true;
            }
        }
        return false;
    }

    public void AddEquipItem(ItemSO itemSO)
    {
        for (int i = 0; i < equipment.itemList.Count; i++)
        {
            ItemSO equipmentItemSO = GetItemSODetail(equipment.itemList[i]);
            if (equipmentItemSO == null)
            {
                equipment.itemList[i] = new Item(itemSO);
                ReduceInventoryItem(new Item(itemSO));//同时背包内进行删减。
                return;//若存在null，说明有空位
            }
        }
        //没有空位则进行添加操作
        equipment.itemList.Add(new Item(itemSO));
        ReduceInventoryItem(new Item(itemSO));//同时背包内进行删减。
    }


    public void EquipItem(ItemSO itemSO)
    {
        if (!ReplaceEquipItem(itemSO))
        {
            AddEquipItem(itemSO);
        }
    }

    public void UnEquipItem(ItemSO itemSO)
    {
        //若背包内存在该物体，则只更改计数，否则插入
        for (int i = 0; i < inventory.itemList.Count; i++)
        {
            ItemSO inventoryItemSO = GetItemSODetail(inventory.itemList[i]);
            if (inventoryItemSO.Name == itemSO.Name)
            {
                //说明存在该物体。
                inventory.itemList[i].stack++;
                ReduceEquipmentItem(new Item(itemSO));
                return;
            }
        }
        //背包内没有同种物体
        inventory.itemList.Add(new Item(itemSO));
        ReduceEquipmentItem(new Item(itemSO));
    }

    public void ReduceEquipmentItem(Item _item)
    {
        for (int i = 0; i < equipment.itemList.Count; i++)
        {
            if (equipment.itemList[i].ItemName == _item.ItemName)
            {
                equipment.itemList[i].ItemName = "";
                equipment.itemList[i].stack = 0;
            }
        }
        return;
    }
    public void ReduceEquipmentItem(ItemSO _itemSO)
    {
        ReduceEquipmentItem(new Item(_itemSO));
        return;
    }
    #endregion

    #region Data Methods
    public void Consume(int val) {
        playerCoin.coins -= val;
    }
    #endregion
}
