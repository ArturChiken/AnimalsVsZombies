using UnityEngine;

public class Sun : MonoBehaviour
{
    public float dropToYPos;
    private float speed = .15f;
    public LayerMask sunMask;
    private Gamemanager gms;

    private void Start()
    {
        gms = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        if (transform.position.y > dropToYPos)
        {
            transform.position -= new Vector3(0, speed * Time.fixedDeltaTime, 0);
        }
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, sunMask);

        if (hit.collider)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gms.suns += 25;
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
