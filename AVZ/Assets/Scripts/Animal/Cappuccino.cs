using UnityEngine;

public class Cappuccino : MonoBehaviour
{
    public GameObject coffeeObject;
    public float cooldown;

    private void Start()
    {
        InvokeRepeating("SpawnCoffee", cooldown, cooldown);
    }

    void SpawnCoffee()
    {
        GameObject myCoffee = Instantiate(coffeeObject, new Vector3(transform.position.x + Random.Range(-.5f, .5f), transform.position.y + Random.Range(0f, .5f), 0), Quaternion.identity);
        myCoffee.GetComponent<Coffee>().dropToYPos = transform.position.y - 1;
    }
}
