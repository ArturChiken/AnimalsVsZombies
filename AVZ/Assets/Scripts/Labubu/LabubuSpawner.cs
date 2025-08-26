using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LabubuSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject labubu;
    public LabubuType[] labubuTypes;
    public Slider progressBar;

    public float labubuDelay;
    public float labubuSpawnTime;
    public int labubuMax;
    public int labubuSpawned;

    private void Start()
    {
        switch (DiffMenuButtonsManager.currDiff)
        {
            case 1:
                {
                    labubuDelay *= 2;
                    labubuMax /= 2;
                    labubuSpawnTime *= 2;
                    break;
                }
            case 3:
                {
                    labubuDelay /= 2;
                    labubuMax *= 2;
                    labubuSpawnTime /= 2;
                    break;
                }
        }
        StartCoroutine(SpawnLabubuDelay());
    }

    private void Update()
    {
        progressBar.maxValue = labubuMax;
        progressBar.value = labubuSpawned;
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
