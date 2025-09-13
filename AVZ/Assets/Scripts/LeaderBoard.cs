using TMPro;
using UnityEngine;
using YG;

public class LeaderBoard : MonoBehaviour
{
    private Gamemanager gameManager;
    private Lose lose;
    public TextMeshProUGUI timer;
    private bool isGameStarted;
    private float score;
    private float time = 0f;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        lose = GameObject.Find("LoseTrigger").GetComponent<Lose>();
    }

    private void Update()
    {
        timer.text = time.ToString("F2");

        if (isGameStarted && !gameManager.isGameStarted && lose.isGameFinish) isGameStarted = false;

        if (!isGameStarted && gameManager.isGameStarted) isGameStarted = true;

        if (isGameStarted && gameManager.isGameStarted && !lose.isGameFinish)
        {
            time += Time.deltaTime;
        }

        if (!gameManager.isGameStarted && lose.isGameFinish)
        {
            score = time;
            LeaderBoardScoreSave();
        }
    }

    private void LeaderBoardScoreSave()
    {
        if (score > YG2.saves.score)
        {
            YG2.saves.score = score;
            YG2.SetLBTimeConvert("EndlessMode", score);
        }
        YG2.SaveProgress();
    }
}
