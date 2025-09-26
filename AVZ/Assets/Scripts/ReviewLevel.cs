using UnityEngine;
using YG;

public class ReviewLevel : MonoBehaviour
{
    private Gamemanager gameManager;
    private Lose lose;

    private bool isShowed = false;

    private void Awake()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        lose = GameObject.Find("LoseTrigger").GetComponent<Lose>();
    }

    private void Update()
    {
        if (!gameManager.isGameStarted && lose.isGameFinish && !isShowed && YG2.reviewCanShow)
        {
            isShowed = true;
            YG2.ReviewShow();
        }
    }
}
