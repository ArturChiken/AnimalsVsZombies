using UnityEngine;

public class Labubu : MonoBehaviour
{
    private int health;
    private float speed;
    private int damage;
    private float range;
    private float eatCooldown;
    private bool canEat = true;

    public LabubuType type;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public LayerMask plantMask;
    //public Plant targetPlant;

    private void Start()
    {
        health = type.health;
        speed = type.speed;
        damage = type.damage;
        range = type.range;
        eatCooldown = type.eatCooldown;

        // Установка компонента пустого спрайта, для рендера анимации
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = type.sprite;
        spriteRenderer.sortingOrder = 1;

        // Установка компонента анимки и самой анимки
        animator = gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = type.animatorController;
        animator.Play(type.animatorController.name, 0);
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(speed, 0, 0);
    }
}
