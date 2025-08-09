using UnityEngine;
using UnityEngine.UI;

public class LabubuSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject labubu;
    public LabubuType[] labubuTypes;
    public Slider progressBar;
    public LevelManager diff;

    public float labubuDelay;
    public float labubuSpawnTime;
    public int labubuMax;
    public int labubuSpawned;

    private void Start()
    {
        switch (diff.difOut)
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

        InvokeRepeating("SpawnLabubu", labubuDelay, labubuSpawnTime);
        progressBar.maxValue = labubuMax;
    }

    private void Update()
    {
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
}
