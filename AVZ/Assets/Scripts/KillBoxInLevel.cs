using Unity.VisualScripting;
using UnityEngine;

public class KillBoxInLevel : MonoBehaviour
{
    private Gamemanager gameManager;
    public GameObject kill;
    private bool isNeedRevive = false;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
    }

    private void Update()
    {
        if (!gameManager.isGameStarted)
        {
            isNeedRevive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isNeedRevive)
        {
            isNeedRevive = false;
            DropBomb(collision);
            collision.GetComponent<Labubu>().LabubuGetHit(999);
        }
    }

    private void DropBomb(Collider2D collision)
    {
        GameObject myKill = Instantiate(kill, collision.GetComponent<Labubu>().transform.position, Quaternion.identity);
    }
}
