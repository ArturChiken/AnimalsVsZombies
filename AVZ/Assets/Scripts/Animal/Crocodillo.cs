using UnityEngine;

public class Crocodillo : MonoBehaviour
{
    public bool isMoving;
    public float speed;
    public int damage;
    public float cooldown;
    private float yPos;
    private float yPos2;
    private bool isDropping = false;

    public GameObject bomb;
    public Transform shootOrigin;
    private BoxCollider2D boxCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.GetComponent<Labubu>().LabubuGetHit(damage);

            isMoving = true;
            Destroy(gameObject, 8);
        }
    }

    private void Start()
    {
        yPos = transform.position.y;
        yPos2 = yPos + 1;

        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            {
                if (yPos <= yPos2)
                {
                    yPos = transform.position.y;
                    transform.position += new Vector3(0, 1f * Time.deltaTime, 0);
                    boxCollider.offset -= new Vector2(0f, 1f * Time.deltaTime);
                }
                if (!isDropping)
                {
                    InvokeRepeating("DropBombs", 0.25f, cooldown);
                    isDropping = true;
                }
            }
        }

    }

    void DropBombs()
    {
        GameObject myBomb = Instantiate(bomb, shootOrigin.position, Quaternion.identity);
    }
}
