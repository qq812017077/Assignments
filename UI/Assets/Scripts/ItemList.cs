using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Custom/ItemList")]
public class ItemList : ScriptableObject
{
    public List<Item> itemList;
    
}

[System.Serializable]
public class Item {
    public string ItemName;
    public int stack;
    public Item(ItemSO itemSO,int count = 1) {
        ItemName = itemSO.Name;
        stack = count;
    }
    public Item(string name, int count = 1)
    {
        ItemName = name;
        stack = count;
    }
}
