using UnityEngine;
using YG;

public class LeaderBoard : MonoBehaviour
{
    private Gamemanager gameManager;
    private Lose loseTrigger;
    private bool isGameStarted;
    private float score;
    private float time = 0f;
    private bool scoreWrited;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        loseTrigger = GameObject.Find("LoseTrigger").GetComponent<Lose>();
    }

    private void Update()
    {
        if (isGameStarted && gameManager.isGameStarted)
        {
            time += Time.deltaTime;
        }
        if (!isGameStarted && gameManager.isGameStarted)
        {
            isGameStarted = true;
        }
        if (!gameManager.isGameStarted && loseTrigger.isGameFinish)
        {
            score = time;
        }
        if (!scoreWrited && !gameManager.isGameStarted && loseTrigger.isGameFinish)
        {
            LeaderBoardScoreSave();
        }
    }

    private void LeaderBoardScoreSave()
    {
        scoreWrited = true;
        if (score > YG2.saves.score) YG2.SetLBTimeConvert("InfinityModeLB", score);
    }
}
