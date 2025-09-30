using System.Collections;
using UnityEngine;

public class CoffeeSpawner : MonoBehaviour
{
    public GameObject coffeeObject;
    private Gamemanager gameManager;

    private bool isGameStarted;

    private void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
    }

    private void Update()
    {
        if (!isGameStarted && gameManager.isGameStarted)
        {
            isGameStarted = true;
            StartCoroutine(DelayBeforeFirstSeed());
        }
    }

    void SpawnCoffee()
    {
        GameObject myCoffee = Instantiate(coffeeObject, new Vector3(Random.Range(-3.75f, 8.35f), 6, 0), Quaternion.identity);
        myCoffee.GetComponent<Coffee>().dropToYPos = Random.Range(2f, -3f);
        Invoke("SpawnCoffee", Random.Range(8, 14));
    }

    IEnumerator DelayBeforeFirstSeed()
    {
        yield return new WaitForSeconds(7f);
        SpawnCoffee();
    }
}
