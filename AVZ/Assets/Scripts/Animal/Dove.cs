using System.Runtime.CompilerServices;
using UnityEngine;

public class Dove : MonoBehaviour
{
    AudioManager audioManager;
    
    public int damage;
    private bool isAlive = true;

    public GameObject blow;
    public Transform blowOrigin;

    public AudioClip soundToPlay;

    private void Update()
    {
        if (!isAlive)
        {
            Animal animal = GetComponent<Animal>();
            animal.AnimalGetHit(0);
        }
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Labubu>(out Labubu labubu))
        {
            audioManager.PlaySFX(soundToPlay);
            GameObject myBullet = Instantiate(blow, blowOrigin.position, Quaternion.identity);
            labubu.LabubuGetHit(damage);
            isAlive = false;
        }
    }
}
