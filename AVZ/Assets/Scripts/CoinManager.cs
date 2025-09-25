using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int value;

    public void OnMouseDown()
    {
        Gamemanager.IncrementCoins(value);
        Destroy(this.gameObject);
    }
}
