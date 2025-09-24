using UnityEngine;

[CreateAssetMenu(fileName = "New LabubuType", menuName = "Labubu")]
public class LabubuType : ScriptableObject
{
    public int health;
    public float speed;
    public int damage;
    public float range;
    public float eatCooldown;

    public Sprite sprite = null;
    public AnimationClip animation;
    public RuntimeAnimatorController animatorController;
}