using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using YG;

public class LeaderBoard : MonoBehaviour
{
    private Gamemanager gameManager;
    private Lose loseTrigger;
    private bool isGameStarted;
    private float score;
    private float time;
    private bool scoreWrited;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        loseTrigger = GameObject.Find("LoseTrigger").GetComponent<Lose>();
        time = 0f;
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
            //timer starts
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
        //Проверка в учетке пользователя, что новый рекорд больше предыдущего (ЭТО ВАЖНО)
    }
}
