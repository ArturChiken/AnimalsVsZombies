using UnityEngine;

public class BobritoShooter : MonoBehaviour
{
    AudioManager audioManager;

    public float cooldown;
    public float cldwn;
    private bool canShoot;
    public float range;

    public GameObject bullet;
    public Transform shootOrigin;
    public LayerMask shootMask;
    private GameObject target;
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
            Invoke("Shoot", cldwn);
            Invoke("Shoot", cldwn*2);
            canShoot = false;
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
        Invoke("RealShoot", cldwn);
        Invoke("RealShoot", cldwn * 2);
        Invoke("RealShoot", cldwn * 3);
    }

    void RealShoot()
    {
        anim.Play("Attack");
        GameObject muBullet = Instantiate(bullet, shootOrigin.position, Quaternion.identity);
        anim.Play("Idle");
    }
}
