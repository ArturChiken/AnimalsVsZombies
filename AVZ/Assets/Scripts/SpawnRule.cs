using UnityEngine;

public class SpawnRule : MonoBehaviour
{
    public float labubuDelay;
    public float labubuSpawnTime;
    public int labubuMax;
    public int labubuSpawned = 0;

    private int actNum;
    private int lvlNum;

    public Transform[] spawnPoints;
    public LabubuType[] labubuTypes;
    public LabubuSpawner spawner;

    private void Start()
    {
        LevelSwitcher levelSwitcher = GetComponent<LevelSwitcher>();

        actNum = levelSwitcher.actNum;
        lvlNum = levelSwitcher.lvlNum;

        if (actNum == ActMenuButtonsManager.currAct)
        {
            if (lvlNum == LevelMenuButtonManager.currLevel)
            {
                spawner.labubuDelay = labubuDelay;
                spawner.labubuSpawnTime = labubuSpawnTime;
                spawner.labubuMax = labubuMax;

                spawner.spawnPoints = spawnPoints;
                spawner.labubuTypes = labubuTypes;
            }
        }
        Debug.Log($"{actNum} and {lvlNum}");
    }
}
