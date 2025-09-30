using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int value;

    private Gamemanager gameManager;
    public LayerMask coinMask;
    AudioManager audioManager;

    private void Awake()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, coinMask);

        if (hit.collider)
        {
            Debug.Log("1111111");
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("DSFGUIDFG");
                gameManager.IncrementCoins(value);
                audioManager.PlaySFX(audioManager.coinCollect);
                Destroy(this.gameObject);
            }
        }
    }
}