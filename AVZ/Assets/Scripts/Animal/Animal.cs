using UnityEngine;

public class Animal : MonoBehaviour
{
    public int health;
    public bool isKami = false;

    public Tile tile;

    private void Start()
    {
        gameObject.layer = 9;
        if (isKami) Invoke("Kamikaze", 2f);
    }

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
