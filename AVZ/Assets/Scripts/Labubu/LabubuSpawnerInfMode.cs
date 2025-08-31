using UnityEngine;
using UnityEngine.UI;

public class LabubuSpawnerInfMode : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject labubu;
    public LabubuType[] labubuTypes;
    public Slider progressBar;
    private Gamemanager gameManager;

    public float labubuDelay;
    public float labubuSpawnTime;
    public int labubuMax;
    public int labubuSpawned;
    public int labubuDead;
    private bool isGameStarted;

    private int waveCount;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
    }

    private void Update()
    {
        progressBar.maxValue = labubuMax;
        progressBar.value = labubuDead;

        if (!isGameStarted && gameManager.isGameStarted)
        {
            isGameStarted = true;
            InvokeRepeating("SpawnLabubu", labubuDelay, labubuSpawnTime);
        }
    }

    void SpawnLabubu()
    {
        if (labubuSpawned >= labubuMax)
        {
            CancelInvoke("SpawnLabubu");
            WavePause();
        }
        labubuSpawned++;

        int randomPoint = Random.Range(0, spawnPoints.Length);
        int randomType = Random.Range(0, labubuTypes.Length);
        GameObject myLabubu = Instantiate(labubu, spawnPoints[randomPoint].position, Quaternion.identity);
        myLabubu.GetComponent<Labubu>().type = labubuTypes[randomType];
    }

    private void WavePause()
    {
        Invoke("WaveTide", 10f);
        waveCount++;
    }

    private void WaveTide()
    {
        labubuSpawned = 0;
        labubuDead = 0;
        labubuMax *= (int)(1.35f);

        gameManager.coffees += 15*waveCount;

        InvokeRepeating("SpawnLabubu", labubuDelay, labubuSpawnTime);
    }
}
