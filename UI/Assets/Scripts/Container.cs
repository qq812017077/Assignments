using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 在Content下的所有未被激活的Slot，都被看作是Free Slot
/// </summary>
public class Container : MonoBehaviour
{
    public enum ContainerType {
        Inventory,
        Store,
        Equipment
    }

    public ContainerType containerType;
    public Transform content;
    public GameObject slot;

    
    private void Start()
    {
        ShowContent();
    }

    /// <summary>
    /// 获取空闲的Slot：
    ///     1.若存在空闲的Slot，则直接使用
    ///     2.不存在空闲的Slot，实例化一个新的Slot
    /// </summary>
    /// <returns></returns>
    GameObject GetFreeSlot() {
        for (int i = 0; i < content.childCount; i++) {
            if (!content.GetChild(i).gameObject.activeSelf) {
                return content.GetChild(i).gameObject;
            }
        }
        GameObject slotGO = Instantiate(slot);
        slotGO.GetComponent<Slot>().BindEvent(OnClick);
        return slotGO;
    }


    void ShowSlot(Item item) {
        GameObject slotGO = GetFreeSlot();
        slotGO.transform.SetParent(content);
        slotGO.SetActive(true);
        Slot slot = slotGO.GetComponent<Slot>();
        slot.SetSlot(item, Database.instance.GetItemSODetail(item));
    }

    
    public void Clear() {
        for (int i = 0; i < content.childCount; i++) {
            content.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Refresh() {
        if (containerType != ContainerType.Equipment) {
            Clear();
        }
        ShowContent();
    }

    public virtual void Open() {
        gameObject.SetActive(true);
    }

    public virtual void Close() {
        gameObject.SetActive(false);
    }
    public void ShowContent()
    {
        switch (containerType)
        {
            case ContainerType.Inventory:
                var items = Database.instance.inventory.itemList;
                foreach (Item item in items)
                {
                    ShowSlot(item);
                }
                break;
            case ContainerType.Store:
                var items_store = Database.instance.store.itemList;
                foreach (Item item in items_store)
                {
                    ShowSlot(item);
                }
                break;
            case ContainerType.Equipment://固定槽位
                var equipments = Database.instance.equipment.itemList;
                for (int i = 0; i < content.transform.childCount; i++) {
                    var slot = content.GetChild(i).GetComponent<Slot>();
                    if (!FindEquialLocalToItem(equipments, slot)) {
                        slot.Clear();
                        switch (slot.m_LocalTo) {
                            case LocalTo.Left_Hand: slot.m_GOName.text = "Left Hand"; break;
                            case LocalTo.Right_Hand:slot.m_GOName.text = "Right Hand"; break;
                            case LocalTo.Head: slot.m_GOName.text = "Head"; break;
                            case LocalTo.Foot: slot.m_GOName.text = "Foot"; break;
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    public virtual void AddItem(ItemSO itemSO) {
        switch (containerType) {
            case ContainerType.Inventory:
                Database.instance.AddInventoryItem(itemSO);
                break;
            case ContainerType.Store:
                Database.instance.AddStoreItem(itemSO);
                break;
        }
    }

    public virtual void ReduceItem(ItemSO itemSO) {
        switch (containerType)
        {
            case ContainerType.Inventory:
                Database.instance.ReduceInventoryItem(itemSO);
                break;
            case ContainerType.Store:
                Database.instance.ReduceStoreItem(itemSO);
                break;
        }
    }

    //查询slot类型匹配情况
    bool FindEquialLocalToItem(List<Item> items, Slot slot) {
        foreach (var item in items) {
            var itemInfo = Database.instance.GetItemSODetail(item);
            if (itemInfo == null)
                continue;
            if (itemInfo.localTo == slot.m_LocalTo) {
                slot.SetSlot(item, itemInfo);
                return true;
            }
        }
        return false;
    }

    protected virtual void OnClick(BaseEventData pointData)
    {
        Debug.Log("基类事件");
    }
}
