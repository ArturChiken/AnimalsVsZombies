using UnityEngine;

public class Animal : MonoBehaviour
{
    public int health;

    public Tile tile;

    public void AnimalGetHit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            tile.hasAnimal = false;
            Destroy(gameObject);
        }
    }

    void Kamikaze()
    {
        Destroy(gameObject);
    }
}
