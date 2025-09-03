using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using YG;

public class Gamemanager : MonoBehaviour
{

    public GameObject currentAnimal;
    public Sprite currentAnimalSprite;
    public Transform tiles;
    public LayerMask tileMask;
    public int coffees;
    public TextMeshProUGUI coffeeText;

    [SerializeField] int _sceneToLoadAfterPressedBack;

    public int preCurrentAmount = -1;
    public TMP_Text coinDisplay;

    public int cardAmount;
    public bool isGameStarted;

    private void Start()
    {
        coinDisplay.SetText(YG2.saves.playerCoins + "");
    }
    public void Win(int starsAquired)
    {
        YG2.SaveProgress();
        isGameStarted = false;
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

        YG2.SaveProgress();

        SceneManager.LoadScene(1);
    }

    public void Lose()
    {
        YG2.SaveProgress();

        isGameStarted = false;
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

        if (preCurrentAmount != YG2.saves.playerCoins)
        {
            preCurrentAmount = YG2.saves.playerCoins;
            coinDisplay.SetText(YG2.saves.playerCoins + "");
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
        YG2.saves.playerCoins += value;
    }

}
