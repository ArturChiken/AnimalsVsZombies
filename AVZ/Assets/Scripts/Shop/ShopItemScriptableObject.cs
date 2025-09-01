using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop Item")]
public class ShopItemScriptableObject : ScriptableObject
{
    public string itemId; // ID ��������
    public string displayName;
    public int cost;
    public int useCount; // ������� -1 ��� ������������ � 0 ��� ������������
    public Sprite icon;
    public GameObject prefab; // ������ ��� ������������� � ����
}