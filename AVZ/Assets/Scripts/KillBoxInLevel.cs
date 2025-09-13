using Unity.VisualScripting;
using UnityEngine;

public class KillBoxInLevel : MonoBehaviour
{
    private Gamemanager gameManager;
    private Lose lose;
    public GameObject blow;
    private bool isNeedRevive = false;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        lose = GameObject.Find("LoseTrigger").GetComponent<Lose>();
    }

    private void Update()
    {
        if (!gameManager.isGameStarted && lose.isGameFinish)
        {
            isNeedRevive = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isNeedRevive && collision.gameObject.layer == 7)
        {
            Invoke("KillTimer", 1f);
            collision.GetComponent<Labubu>().LabubuGetHit(999);
            BlowLabubu(collision);
        }
    }

    private void BlowLabubu(Collider2D collision)
    {
        GameObject myBlow = Instantiate(blow, collision.GetComponent<Labubu>().transform.position, Quaternion.identity);
    }

    public void KillTimer()
    {
        isNeedRevive = false;
    }
}
