using UnityEngine;

[CreateAssetMenu(fileName = "New ZombieType", menuName = "Zombie")]
public class ZombieType : ScriptableObject
{
    public int health;
    public float speed;
    public int damage;
    public float range;
    public float eatCooldown;
    public Sprite sprite;
    public Sprite deathSprite;
}
