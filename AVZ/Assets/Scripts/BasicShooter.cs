using UnityEngine;

public class BasicShooter : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootOrigin;
    public float cooldown;
    private bool canShoot;
    public float range;
    public LayerMask shootMask;
    private GameObject target;


    private void Start()
    {
        Invoke("ResetCooldown", cooldown);
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
        canShoot = false;
        Invoke("ResetCooldown", cooldown);

        GameObject muBullet = Instantiate(bullet, shootOrigin.position, Quaternion.identity);
    }
}
