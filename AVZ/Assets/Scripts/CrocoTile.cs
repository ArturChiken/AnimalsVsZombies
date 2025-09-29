using UnityEngine;
using UnityEngine.Sprites;

public class CrocoTile : MonoBehaviour
{
    public GameObject crocodilo;
    private Gamemanager gamemanager;

    private void Start()
    {
        gamemanager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
    }

    private void Update()
    {
        if(gamemanager.currentCrocodile)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.7843f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void SpawnCrocodilo()
    {
        CrocoUse();
        GameObject muDillo = Instantiate(crocodilo, transform.position, Quaternion.identity);
    }

    public void CrocoUse()
    {

    }
}
