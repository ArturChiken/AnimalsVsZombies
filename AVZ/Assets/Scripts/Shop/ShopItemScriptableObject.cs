using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop Item")]
public class ShopItemScriptableObject : ScriptableObject
{
    public string name;
    public string description;

    public Sprite icon;

    public int cost;

    public bool isPermanent;
    public int useCount;

    public bool isBought;
}
