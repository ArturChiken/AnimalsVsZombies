using UnityEngine;

public class CoffeeBomb : MonoBehaviour
{   
    AudioManager audioManager;
    public float timer;
    public int damage;
    public AudioClip soundToPlay;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        Invoke("Kamikaze", timer);
        audioManager.PlaySFX(soundToPlay);
    }

    void Kamikaze()
    {
        Animal animal = GetComponent<Animal>();
        animal.AnimalGetHit(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Labubu>(out Labubu labubu))
        {
            labubu.LabubuGetHit(damage);
        }
    }
}
