using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public Transform content;
    public Text UI_Attack;
    public Text UI_Defense;
    [SerializeField] private Slot[] slots;

    private int attack = 0;
    private int defense = 0;

    EquipmentContainer equipmentContainer;

    private void Start()
    {
        if (content != null) {
            slots = content.GetComponentsInChildren<Slot>();
        }
        equipmentContainer = GetComponent<EquipmentContainer>();
        equipmentContainer.OnInit += () => { FreshPlayerState(); };
        equipmentContainer.OnEquip += () => { FreshPlayerState(); };
        equipmentContainer.OnUnEquip += () => { FreshPlayerState(); };
    }

    public void FreshPlayerState() {
        attack = 0;
        defense = 0;
        foreach (Slot slot in slots) {
            var item = slot.m_ItemSO;
            if (item == null) {
                attack += 0;
                defense += 0;
                continue;
            }
            attack += slot.m_ItemSO.AttributeValue.Attack;
            defense += slot.m_ItemSO.AttributeValue.Defensive;
        }

        UI_Attack.text = "Attack:"+attack;
        UI_Defense.text = "Defense:"+defense;
    }


}
