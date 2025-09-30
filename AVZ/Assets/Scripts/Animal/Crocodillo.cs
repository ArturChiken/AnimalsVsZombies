using UnityEngine;

public class Crocodillo : MonoBehaviour
{
    AudioManager audioManager;

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
    private Collider2D collision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.GetComponent<Labubu>().LabubuGetHit(damage);
        }
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if (Random.Range(0f, 1f) <= .5f)
        {
            audioManager.PlaySFX(audioManager.bombordiro_attack);
        }
        else
        {
            audioManager.PlaySFX(audioManager.bombordiro_attack2);
        }
        isMoving = true;
        yPos = transform.position.y;
        yPos2 = yPos + 1;
        Destroy(gameObject, 8);
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
                    boxCollider.offset -= new Vector2(2f * Time.deltaTime, 1f * Time.deltaTime);
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
