using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LabubuSpawnerInfMode : MonoBehaviour
{
    AudioManager audioManager;

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
    public float wavePause;
    private bool isGameStarted;

    public int waveCount = 1;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        progressBar.maxValue = labubuMax;
        progressBar.value = labubuDead;

        if (!isGameStarted && gameManager.isGameStarted)
        {
            isGameStarted = true;
            StartCoroutine(FirstWave());
        }
    }

    void SpawnLabubu()
    {
        if (labubuSpawned >= labubuMax)
        {
            if (labubuDead >= labubuMax)
            {
                StopRepeating();
                WavePause();
            }
            return;
        }
        labubuSpawned++;

        int randomPoint = Random.Range(0, spawnPoints.Length);
        int randomType = Random.Range(0, labubuTypes.Length);
        GameObject myLabubu = Instantiate(labubu, spawnPoints[randomPoint].position, Quaternion.identity);
        myLabubu.GetComponent<Labubu>().type = labubuTypes[randomType];
    }

    private void WavePause()
    {
        if (wavePause <= 20f) wavePause *= 1.35f;
        else wavePause = 20f;

            Invoke("WaveTide", wavePause);
    }

    private void WaveTide()
    {
        waveCount++;

        labubuSpawned = 0;
        labubuDead = 0;
        labubuMax *= (int)(2f);

        gameManager.coffees += 25*waveCount;

        StartCoroutine(RandomRepeatLabubuSpawn());
    }

    private IEnumerator RandomRepeatLabubuSpawn()
    {
        while (true)
        {
            SpawnLabubu();

            float randomDelay = Random.Range(labubuSpawnTime - 3f, labubuSpawnTime + 3f);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    private IEnumerator FirstWave()
    {
        yield return new WaitForSeconds(labubuDelay);
        StartCoroutine(RandomRepeatLabubuSpawn());
    }

    public void StopRepeating()
    {
        StopAllCoroutines();
    }
}
