using UnityEngine;

public class BlowDove : MonoBehaviour
{
    public float speed;
    private void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }
}
