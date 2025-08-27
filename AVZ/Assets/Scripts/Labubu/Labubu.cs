using UnityEngine;

public class Labubu : MonoBehaviour
{

    private int health;
    private float speed;
    private int damage;
    private float range;
    private float eatCooldown;
    private bool canEat = true;

    public GameObject coinPrefab;

    public LabubuType type;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public LayerMask animalMask;
    public Animal targetAnimal;

    private void Start()
    {
        health = type.health;
        speed = type.speed;
        damage = type.damage;
        range = type.range;
        eatCooldown = type.eatCooldown;

        // Установка компонента анимки и самой анимки
        animator = gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = type.animatorController;
        animator.Play(type.animatorController.name, 0);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, range, animalMask);

        if (hit.collider)
        {
            targetAnimal = hit.collider.GetComponent<Animal>();
            Eat();
        }
    }

    void Eat()
    {
        if (!canEat || !targetAnimal) return;

        canEat = false;
        Invoke("ResetCooldown", eatCooldown);

        targetAnimal.AnimalGetHit(damage);
    }

    private void FixedUpdate()
    {
        if (!targetAnimal)
        {
            transform.position -= new Vector3(speed, 0, 0);
        }
    }

    void ResetCooldown()
    {
        canEat = true;
    }

    public void LabubuGetHit(int damage, bool freeze = false)
    {
        health -= damage;
        if (freeze)
        {
            Freeze();
        }
        if (health <= 0)
        {
            GameObject.Find("LabubuSpawner").GetComponent<LabubuSpawner>().labubuDead++;
            Destroy(gameObject);
            int randomInt = Random.Range(0, 1);

            if (randomInt <= type.coinDropPercent)
            {
                GameObject coinObj = Instantiate(coinPrefab, transform.position, Quaternion.identity);

                coinObj.transform.SetParent(null);
            }
        }
    }
    void Freeze()
    {
        CancelInvoke("UnFreeze");
        GetComponent<SpriteRenderer>().color = Color.blue;
        speed = type.speed / 2;
        Invoke("UnFreeze", 5);
    }

    void UnFreeze()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        speed = type.speed;
    }
}
