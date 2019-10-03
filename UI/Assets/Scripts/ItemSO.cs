using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/ItemSO")]
public class ItemSO : ScriptableObject
{
    [Tooltip("名字")]
    public string Name;
    [Tooltip("图标")]
    public Sprite Icon;
    [Tooltip("预设")]
    public GameObject Prefab;
    [Tooltip("类型")]
    public Behaviour behaviour;
    [Tooltip("位置")]
    public LocalTo localTo;
    [Tooltip("属性")]
    public Attribute AttributeValue;
    [Tooltip("价格")]
    public int Price;
    [Tooltip("描述")]
    public string Detail;
    
    
}
[System.Serializable]
public class Attribute {
    public int Attack;
    public int Defensive;
}
public enum Behaviour {
    Eat,
    Throw,
    Equipment
}


public enum LocalTo {
    None,
    Head,
    Left_Hand,
    Right_Hand,
    Foot
}

