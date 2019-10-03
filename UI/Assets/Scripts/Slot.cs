using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    
    public Behaviour m_Behaviour;
    public Attribute m_Attribute;
    public LocalTo m_LocalTo;
    public Text m_GOName;
    public Image m_Icon;
    public Text m_Stack;
    public Text m_Price;
    [HideInInspector]
    public ItemSO m_ItemSO;

    [SerializeField]
    private GameObject DetailPanel;
    [SerializeField]
    private GameObject detailInstaince;

    private bool m_Follow = false;
    public void SetSlot(Item item, ItemSO itemSO) {
        if (m_GOName != null) m_GOName.text = item.ItemName;
        if (m_Icon != null) m_Icon.sprite = itemSO.Icon ;
        if (m_Price != null) m_Price.text = "$"+itemSO.Price.ToString();
        if (m_Stack != null) m_Stack.text = (item.stack == -1)? "" : item.stack.ToString();
        m_ItemSO = itemSO;
    }


    private void Update()
    {
        if (m_Follow && detailInstaince != null) {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            detailInstaince.transform.position += new Vector3(x,y,0) * 20f;
        }
    }
    public void Clear() {
        if (m_GOName != null) m_GOName.text = "";
        if (m_Icon != null) m_Icon.sprite = null;
        if (m_Stack != null) m_Stack.text = "";
        if (m_Price != null) m_Price.text = "";
        if (m_ItemSO != null) m_ItemSO = null;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        
        if (DetailPanel != null && m_ItemSO !=null)
        {
            detailInstaince = Instantiate(DetailPanel);
            detailInstaince.transform.SetParent(this.transform.root);
            Detail detail = detailInstaince.GetComponent<Detail>();
            detail.SetDetail(m_ItemSO);
            detailInstaince.transform.position = eventData.position + new Vector2(600,0);
        }
        m_Follow = true;
        return;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (detailInstaince != null)
            Destroy(detailInstaince);
        m_Follow = false;
        return;
    }


    public void BindEvent(UnityEngine.Events.UnityAction<BaseEventData> action) {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;// 鼠标点击事件
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }
}
