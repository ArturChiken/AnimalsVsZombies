using UnityEngine;

public class Labubu : MonoBehaviour
{
    AudioManager audioManager;

    private int health;
    private float speed;
    private int damage;
    private float range;
    private float eatCooldown;
    private bool canEat = true;

    public bool infMode;

    public GameObject coinPrefab;
    public GameObject infCoinPrefab;

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

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
        audioManager.PlaySFX(audioManager.labubu1);
        if (freeze)
        {
            Freeze();
        }
        if (health <= 0)
        {
            if (infMode)
            {
                GameObject.Find("LabubuSpawner").GetComponent<LabubuSpawnerInfMode>().labubuDead++;
                GameObject coinObj = Instantiate(infCoinPrefab, transform.position, Quaternion.identity);
                audioManager.PlaySFX(audioManager.coinDropped);
                coinObj.transform.SetParent(null);
            }
            else if (!infMode)
            {
                GameObject.Find("LabubuSpawner").GetComponent<LabubuSpawner>().labubuDead++;
                if (Random.Range(0, 100) < 50)
                {
                    GameObject coinObj = Instantiate(coinPrefab, transform.position, Quaternion.identity);
                    audioManager.PlaySFX(audioManager.coinDropped);
                    coinObj.transform.SetParent(null);
                }
            }
            if (Random.Range(0f, 1f) < .1f) audioManager.PlaySFX(audioManager.labubu2);
            Destroy(gameObject);
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
