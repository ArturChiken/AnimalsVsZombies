using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public bool freeze = false;

    private void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Labubu>(out Labubu labubu))
        {
            labubu.LabubuGetHit(damage, freeze);
            Destroy(gameObject);
        }
    }
}
