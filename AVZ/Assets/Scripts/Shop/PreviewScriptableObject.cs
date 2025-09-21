using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Preview", menuName = "Shop Preview")]
public class PreviewScriptableObject : ScriptableObject
{
    public string displayName;
    public string displayNameEn;
    public string description;
    public string descriptionEn;
    public Sprite icon;
    public int cost;
}
