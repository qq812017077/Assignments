using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detail : MonoBehaviour
{
    public Text m_GOName;
    public Image m_Icon;
    public Text m_Detail;
    public Text m_Price;


    public void SetDetail(ItemSO itemSO)
    {

        if (itemSO == null) { Debug.LogWarning("itemSO为空！！！");  return; }
        if (m_GOName != null) m_GOName.text = itemSO.Name;
        if (m_Icon != null) m_Icon.sprite = itemSO.Icon;
        if (m_Detail != null) m_Detail.text = itemSO.Detail;
        if (m_Price != null) m_Price.text ="$" + itemSO.Price.ToString();
    }

    public void Clear()
    {
        if (m_GOName != null) m_GOName.text = "";
        if (m_Icon != null) m_Icon.sprite = null;
        if (m_Detail != null) m_Detail.text = "";
        if (m_Price != null) m_Price.text = "";
    }
}
