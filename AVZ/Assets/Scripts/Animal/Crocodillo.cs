using UnityEngine;

public class Crocodillo : MonoBehaviour
{
    public bool isMoving;
    public float speed;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.GetComponent<Labubu>().LabubuGetHit(damage);

            isMoving = true;
            Destroy(gameObject, 8);
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }
}
