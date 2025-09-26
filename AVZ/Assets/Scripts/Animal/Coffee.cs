using UnityEngine;

public class Coffee : MonoBehaviour
{
    public int coffeeCost = 25;

    public float dropToYPos;
    private float speed = .45f;
    public LayerMask coffeeMask;
    private Gamemanager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        if (transform.position.y > dropToYPos)
        {
            transform.position -= new Vector3(0, speed * Time.fixedDeltaTime, 0);
        }
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, coffeeMask);

        if (hit.collider)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameManager.coffees += coffeeCost;
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
