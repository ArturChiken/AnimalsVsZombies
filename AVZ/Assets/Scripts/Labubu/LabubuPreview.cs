using UnityEngine;

public class LabubuPreview : MonoBehaviour
{
    private Gamemanager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
    }

    private void Update()
    {
        if (gameManager.isGameStarted)
        {
            gameObject.SetActive(false);
        }
    }
}
