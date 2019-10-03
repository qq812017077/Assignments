using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentContainer : Container
{
    public HandleWindow equipmentWindow;

    #region Event Methods
    public event Action OnEquip;
    public event Action OnUnEquip;
    public event Action OnInit;
    #endregion

    void Start()
    {
        ShowContent();
        Open();
        Slot[] slots = GetComponentsInChildren<Slot>();
        foreach (Slot s in slots)
        {
            s.BindEvent(OnClick);
        }
        OnInit?.Invoke();
    }

    public void EquipItem(ItemSO itemSO) {
        Database.instance.EquipItem(itemSO);//装备
        Refresh();

        OnEquip?.Invoke();
    }

    public void UnEquipItem(ItemSO itemSO) {
        Database.instance.UnEquipItem(itemSO);//装备
        Refresh();

        OnUnEquip?.Invoke();
    }

    public void Use(GameObject slotGO)
    {
        #region Error Check
        if (equipmentWindow == null) {  Debug.LogError("未绑定装备窗口");  return;}

        ItemSO itemSO = slotGO.GetComponent<Slot>().m_ItemSO;
        if (itemSO == null) { Debug.Log("插槽内无装备");  return; }
        #endregion Error Check

        equipmentWindow.Open(itemSO,HandleWindow.EOperationType.UnEquip);//打开移除窗口
    }

    protected override void OnClick(BaseEventData pointData)
    {
        Debug.Log(pointData.selectedObject);
        GameObject go = pointData.selectedObject;
        Debug.Log(go);
        if (go == null) return;
        Slot slot = go.GetComponent<Slot>();
        Debug.Log(slot.m_GOName.text);
        Use(go);
    }
}
