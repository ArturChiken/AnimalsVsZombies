using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed;
    public int health;
    public int damage;
    public float range;
    public float eatCooldown;
    private bool canEat = true;
    public LayerMask plantMask;
    public Plant targetPlant;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, range, plantMask);

        if (hit.collider)
        {
            targetPlant = hit.collider.GetComponent<Plant>();
            Eat();
        }
    }

    void Eat()
    {
        if(!canEat || !targetPlant)
        {
            return;
        }
        canEat = false;
        Invoke("ResetEatCooldown", eatCooldown);

        targetPlant.Hit(damage);
    }

    void ResetEatCooldown()
    {
        canEat = true;
    }

    private void FixedUpdate()
    {
        if (!targetPlant)
        {
            transform.position -= new Vector3(speed, 0, 0);
        }
    }

    public void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
