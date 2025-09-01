using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Gamemanager : MonoBehaviour
{
    public static string coinPrefsName = "Coins_Player";

    public GameObject currentAnimal;
    public Sprite currentAnimalSprite;
    public Transform tiles;
    public LayerMask tileMask;
    public int coffees;
    public TextMeshProUGUI coffeeText;

    [SerializeField] int _sceneToLoadAfterPressedBack;

    public static int currentCoinAmount;
    public int preCurrentAmount = -1;
    public TMP_Text coinDisplay;

    public int cardAmount;
    public bool isGameStarted;

    private void Start()
    {
        currentCoinAmount = PlayerPrefs.GetInt(coinPrefsName);
        coinDisplay.SetText(currentCoinAmount + "");
    }
    public void Win(int starsAquired)
    {
        //setactive win ui screen pause game
        if (LevelMenuButtonManager.currLevel == LevelSelectorManager.UnlockedLevels)
        {
            LevelSelectorManager.UnlockedLevels++;
            PlayerPrefs.SetInt("UnlockedLevels", LevelSelectorManager.UnlockedLevels);
        }
        if (starsAquired > PlayerPrefs.GetInt("stars" + LevelMenuButtonManager.currLevel.ToString(), 0))
        {
            PlayerPrefs.SetInt("stars" + LevelMenuButtonManager.currLevel.ToString(), starsAquired);
        }
        SceneManager.LoadScene(1);
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

    public void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(coinPrefsName, currentCoinAmount);
    }
}
