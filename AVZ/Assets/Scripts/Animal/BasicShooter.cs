using UnityEngine;

public class BasicShooter : MonoBehaviour
{
    AudioManager audioManager;

    public float cooldown;
    public bool canShoot;
    public float range;

    public GameObject bullet;
    public Transform shootOrigin;
    private GameObject target;
    public LayerMask shootMask;
    public Animator anim;

    public AudioClip soundToPlay;

    private void Start()
    {
        Invoke("ResetCooldown", cooldown);
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, range, shootMask);

        if (hit.collider)
        {
            target = hit.collider.gameObject;
            Shoot();
        }
    }

    void ResetCooldown()
    {
        canShoot = true;
    }

    void Shoot()
    {
        if (!canShoot) return;

        audioManager.PlaySFX(soundToPlay);
        
        canShoot = false;
        Invoke("ResetCooldown", cooldown);

        anim.Play("Attack");
        GameObject myBullet = Instantiate(bullet, shootOrigin.position, Quaternion.identity);
        anim.Play("Idle");
    }
}
