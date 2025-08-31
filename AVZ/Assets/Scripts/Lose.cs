using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public Animator deathScreen;
    private Gamemanager gameManager;
    public bool isGameFinish;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Time.timeScale = 0;
            GameLose();
        }
    }

    void GameLose()
    {
        isGameFinish = true;
        gameManager.Lose();
    }
}
