using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int value;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnMouseDown()
    {
        Gamemanager.IncrementCoins(value);
        audioManager.PlaySFX(audioManager.coinCollect);
        Destroy(this.gameObject);
    }
}
