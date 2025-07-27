using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

        Destroy(gameObject, 3f);
    }
}
