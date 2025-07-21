using UnityEngine;

public class CoffeeBomb : MonoBehaviour
{
    public float timer;
    public int damage;

    private void Start()
    {
        Invoke("Kamikaze", timer);
    }

    void Kamikaze()
    {
        Animal animal = GetComponent<Animal>();
        animal.AnimalGetHit(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Labubu>(out Labubu labubu))
        {
            labubu.LabubuGetHit(damage);
        }
    }
}
