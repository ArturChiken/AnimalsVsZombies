using System.Runtime.CompilerServices;
using UnityEngine;

public class Dove : MonoBehaviour
{
    public int damage;
    private bool isAlive = true;

    public GameObject blow;
    public Transform blowOrigin;

    private void Update()
    {
        if (!isAlive)
        {
            Animal animal = GetComponent<Animal>();
            animal.AnimalGetHit(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Labubu>(out Labubu labubu))
        {
            GameObject myBullet = Instantiate(blow, blowOrigin.position, Quaternion.identity);
            labubu.LabubuGetHit(damage);
            isAlive = false;
        }
    }
}
