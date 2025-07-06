using UnityEngine;

public class SunSpawner : MonoBehaviour
{
    public GameObject sunObject;

    private void Start()
    {
        SpawnSun();
    }

    void SpawnSun()
    {
        Instantiate(sunObject);
        Invoke("SpawnSun", Random.Range(4, 10));
    }
}
