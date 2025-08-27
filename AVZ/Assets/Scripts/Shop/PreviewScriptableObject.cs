using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Preview", menuName = "Shop Preview")]
public class PreviewScriptableObject : ScriptableObject
{
    public string name;
    public string description;

    public Sprite icon;

    public int cost;
}
