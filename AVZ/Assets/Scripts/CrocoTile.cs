using UnityEngine;

public class CrocoTile : MonoBehaviour
{
    public GameObject crocodilo;

    public void SpawnCrocodilo()
    {
        CrocoUse();
        GameObject muDillo = Instantiate(crocodilo, transform.position, Quaternion.identity);
    }

    public void CrocoUse()
    {

    }
}
