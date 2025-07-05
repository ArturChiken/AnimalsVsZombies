using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed;
    public int Hp;

    private void FixedUpdate()
    {
        transform.position -= new Vector3(speed, 0, 0);
    }
}
