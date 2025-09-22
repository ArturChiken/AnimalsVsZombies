using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop Item")]
public class ShopItemScriptableObject : ScriptableObject
{
    public string itemId; // ID предмета
    public string displayName;
    public string displayNameEn;
    public int cost;
    public int useCount; // ставить -1 для перманентных и 0 для ограниченных
    public Sprite icon;
    public GameObject prefab; // префаб для использования в игре
}