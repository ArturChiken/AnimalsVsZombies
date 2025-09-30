using TMPro;
using UnityEngine;
using YG;

public class LeaderBoard : MonoBehaviour
{
    private LabubuSpawnerInfMode spawner;
    public TextMeshProUGUI wave;

    private void Start()
    {
        spawner = GameObject.Find("LabubuSpawner").GetComponent<LabubuSpawnerInfMode>();
    }

    private void Update()
    {
        wave.text = spawner.waveCount.ToString();

        LeaderBoardScoreSave();
    }

    private void LeaderBoardScoreSave()
    {
        if (YG2.player.auth)
        {
            if (spawner.waveCount > YG2.saves.maxWaves)
            {
                YG2.saves.maxWaves = spawner.waveCount;
                YG2.SetLeaderboard("EndlessModeWaves", spawner.waveCount);
                YG2.SaveProgress();
            }
        }
    }
}
