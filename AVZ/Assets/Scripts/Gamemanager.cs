using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Gamemanager : MonoBehaviour
{
    //ןנופû
    public static string coinPrefsName = "Coins_Player";

    public GameObject currentAnimal;
    public Sprite currentAnimalSprite;
    public Transform tiles;
    public LayerMask tileMask;
    public int coffees;
    public TextMeshProUGUI coffeeText;

    [SerializeField] CanvasGroup _fadeCanvasGroup;
    [SerializeField] int _sceneToLoadAfterPressedBack;
    [SerializeField] float _fadeDuration = 1f;

    public static int currentCoinAmount;
    public int preCurrentAmount = -1;
    public TMP_Text coinDisplay;

    public int cardAmount;

    private void Start()
    {
        StartCoroutine(Fade(1f, 0f));
        currentCoinAmount = PlayerPrefs.GetInt(coinPrefsName);
        coinDisplay.SetText(currentCoinAmount + "");
    }

    public void Win()
    {
        Debug.Log("HUII!");
    }

    public void BuyAnimal(GameObject animal, Sprite sprite)
    {
        currentAnimal = animal;
        currentAnimalSprite = sprite;
    }

    private void Update()
    {
        coffeeText.text = coffees.ToString();

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);

        if (preCurrentAmount != currentCoinAmount)
        {
            preCurrentAmount = currentCoinAmount;
            coinDisplay.SetText(currentCoinAmount + "");
        }

        foreach (Transform tile in tiles)
        {
            tile.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (hit.collider && currentAnimal)
        {
            hit.collider.GetComponent<SpriteRenderer>().sprite = currentAnimalSprite;
            hit.collider.GetComponent<SpriteRenderer>().enabled = true;

            if (Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>().hasAnimal)
            {
                Animal(hit.collider.gameObject);
            }
        }
    }

    void Animal(GameObject hit)
    {
        GameObject plant = Instantiate(currentAnimal, hit.transform.position, Quaternion.identity);
        hit.GetComponent<Tile>().hasAnimal = true;
        plant.GetComponent<Animal>().tile = hit.GetComponent<Tile>();
        currentAnimal = null;
        currentAnimalSprite = null;
    }

    public static void IncrementCoins(int value)
    {
        currentCoinAmount += value;
    }

    private IEnumerator TransitionScene()
    {
        yield return StartCoroutine(Fade(0f, 1f));
        SceneManager.LoadScene(_sceneToLoadAfterPressedBack);
    }

    private IEnumerator Fade(float startAlpha, float targetAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            _fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / _fadeDuration);
            yield return null;
        }

        _fadeCanvasGroup.alpha = targetAlpha;
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(coinPrefsName, currentCoinAmount);
    }
}
