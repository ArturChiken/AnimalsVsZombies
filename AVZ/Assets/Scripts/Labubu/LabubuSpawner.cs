using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LabubuSpawner : MonoBehaviour
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
    private bool willWin = true;
    private bool isGameStarted = true;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();

        switch (DiffMenuButtonsManager.currDiff)
        {
            case 1:
                {
                    labubuDelay *= 1.5f;
                    labubuMax /= 2;
                    labubuSpawnTime *= 1.5f;
                    break;
                }
            case 3:
                {
                    labubuDelay /= 1.15f;
                    labubuMax *= 2;
                    labubuSpawnTime /= 1.25f;
                    break;
                }
        }
    }

    private void Update()
    {
        progressBar.maxValue = labubuMax;
        progressBar.value = labubuDead;

        if (isGameStarted && gameManager.isGameStarted)
        {
            isGameStarted = false;
            StartCoroutine(SpawnLabubuDelay());
        }

        if (labubuDead >= labubuMax)
        {
            if(willWin)
            {
                switch (DiffMenuButtonsManager.currDiff)
                {
                    case 1:
                        GameObject.Find("Gamemanager").GetComponent<Gamemanager>().Win(1);
                        willWin = false;
                        break;
                    case 2:
                        GameObject.Find("Gamemanager").GetComponent<Gamemanager>().Win(2);
                        willWin = false;
                        break;
                    case 3:
                        GameObject.Find("Gamemanager").GetComponent<Gamemanager>().Win(3);
                        willWin = false;
                        break;

                }
            }
        }
    }

    void SpawnLabubu()
    {
        if (labubuSpawned >= labubuMax) return;
        labubuSpawned++;

        int randomPoint = Random.Range(0, spawnPoints.Length);
        int randomType = Random.Range(0, labubuTypes.Length);
        GameObject myLabubu = Instantiate(labubu, spawnPoints[randomPoint].position, Quaternion.identity);
        myLabubu.GetComponent<Labubu>().type = labubuTypes[randomType];
    }

    //задержка епта
    IEnumerator SpawnLabubuDelay()
    {
        yield return new WaitForSeconds(0.0001f); 
        InvokeRepeating("SpawnLabubu", labubuDelay, labubuSpawnTime);
    }
}
