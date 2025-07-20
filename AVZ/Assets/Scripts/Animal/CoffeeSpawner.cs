using Unity.VisualScripting;
using UnityEngine;

public class CoffeeSpawner : MonoBehaviour
{
    public GameObject coffeeObject;

    private void Start()
    {
        SpawnCoffee();
    }

    void SpawnCoffee()
    {
        GameObject myCoffee = Instantiate(coffeeObject, new Vector3(Random.Range(-3.75f, 8.35f), 6, 0), Quaternion.identity);
        myCoffee.GetComponent<Coffee>().dropToYPos = Random.Range(2f, -3f);
        Invoke("SpawnCoffee", Random.Range(4, 10));
    }
}
