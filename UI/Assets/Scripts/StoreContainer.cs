using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoreContainer : Container
{
    public HandleWindow dealWindow;

    void Start()
    {
        ShowContent();
        Open();
        Slot[] slots = GetComponentsInChildren<Slot>();
        foreach (Slot s in slots)
        {
            s.BindEvent(OnClick);
        }
    }

    public void Use(GameObject slotGO)
    {
        #region Error Check
        if (dealWindow == null) { Debug.LogError("未绑定小窗口"); return; }

        ItemSO itemSO = slotGO.GetComponent<Slot>().m_ItemSO;
        if (itemSO == null) { Debug.Log("空内容"); return; }
        #endregion Error Check

        dealWindow.Open(itemSO, HandleWindow.EOperationType.Buy);//打开购买窗口
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
