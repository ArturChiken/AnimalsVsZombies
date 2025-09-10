using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using YG;
using System.Linq;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager _;

    public enum PauseScreenContainer { pause, mainmenu, restart};
    public enum WinScreenContainer { mainmenu, nextlvl };
    public enum LoseScreenContainer { mainmenu, restart };

    public GameObject currentAnimal;
    public Sprite currentAnimalSprite;
    public Transform tiles;
    public LayerMask tileMask;
    public int coffees;
    public TextMeshProUGUI coffeeText;

    public int preCurrentAmount = -1;
    public TMP_Text coinDisplay;

    public int cardAmount;
    public bool isGameStarted;
    public bool isGamePaused;

    [SerializeField] CanvasGroup _fadeCanvasGroup;
    [SerializeField] GameObject _pauseScreen, _winScreen, _loseScreen, _blurFrameInGameGO;
    [SerializeField] int _sceneToLoadAfterPressedBack, _sceneToLoadAfterPressedRestartAndNextLvl;
    [SerializeField] float _fadeDuration = 1f;
    public GameObject _speechBubbleCS, _pauseButton, blurFrameInAnimalCardSelectorGO;

    public void Awake()
    {
        if (_ == null)
            _ = this;
        else
            Debug.LogError("There are more than 1 Gamanager in the scene");
    }

    private void Start()
    {
        StartCoroutine(Fade(1f, 0f));
        coinDisplay.SetText(YG2.saves.playerCoins + "");
        _pauseButton.SetActive(false);
        _blurFrameInGameGO.SetActive(false);
        blurFrameInAnimalCardSelectorGO.SetActive(true);
    }
    public void Win(int starsAquired)
    {
        StartCoroutine(DelayBeforeWinOrLose());
        isGameStarted = false;
        if (LevelMenuButtonManager.currLevel == LevelSelectorManager.UnlockedLevels)
        {
            LevelSelectorManager.UnlockedLevels++;
            YG2.saves.stars.Add(0);
            YG2.saves.unlockedLevels = LevelSelectorManager.UnlockedLevels;
        }
        if (starsAquired > YG2.saves.stars[LevelMenuButtonManager.currLevel])
        {
            YG2.saves.stars[LevelMenuButtonManager.currLevel] = starsAquired;
            YG2.saves.playerStars = 0;
            foreach (int k in YG2.saves.stars)
            {
                YG2.saves.playerStars += k;
            }
        }
        _pauseButton.SetActive(false);
        YG2.SaveProgress();
    }

    public void Lose()
    {
        isGameStarted = false;
        YG2.SaveProgress();
        _pauseButton.SetActive(false);
        _blurFrameInGameGO.SetActive(true);
        _loseScreen.SetActive(true);
        Time.timeScale = 0;
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                _blurFrameInGameGO.SetActive(true);
                _pauseScreen.SetActive(true);
                Time.timeScale = 0;
                isGamePaused = true;
                YG2.SaveProgress();
            }
            else
            {
                _blurFrameInGameGO.SetActive(false);
                _pauseScreen.SetActive(false);
                Time.timeScale = 1;
                isGamePaused = false;
                YG2.SaveProgress();
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

    public void PauseButtonClicked(PauseScreenContainer buttonClicked)
    {
        switch (buttonClicked)
        {
            case PauseScreenContainer.mainmenu:
                Time.timeScale = 1;
                isGamePaused = false;
                StartCoroutine(TransitionScene(0));
                break;
            case PauseScreenContainer.pause:
                if (!isGamePaused)
                {
                    _blurFrameInGameGO.SetActive(true);
                    _pauseScreen.SetActive(true);
                    Time.timeScale = 0;
                    isGamePaused = true;
                    YG2.SaveProgress();
                }
                else
                {
                    _blurFrameInGameGO.SetActive(false);
                    _pauseScreen.SetActive(false);
                    Time.timeScale = 1;
                    isGamePaused = false;
                    YG2.SaveProgress();
                }
                break;
            case PauseScreenContainer.restart:
                Time.timeScale = 1;
                isGamePaused = false;
                StartCoroutine(TransitionScene(1));
                break;
        }
    }

    public void WinButtonClicked(WinScreenContainer buttonClicked)
    {
        switch (buttonClicked)
        {
            case WinScreenContainer.mainmenu:
                StartCoroutine(TransitionScene(0));
                break;
            case WinScreenContainer.nextlvl:
                LevelMenuButtonManager.currLevel += 1;
                StartCoroutine(TransitionScene(1));
                break;
        }
    }

    public void LoseButtonClicked(LoseScreenContainer buttonClicked)
    {
        switch (buttonClicked)
        {
            case LoseScreenContainer.mainmenu:
                StartCoroutine(TransitionScene(0));
                break;
            case LoseScreenContainer.restart:
                StartCoroutine(TransitionScene(1));
                break;
        }
    }

    private IEnumerator TransitionScene(int numberOfButton)
    {
        yield return StartCoroutine(Fade(0f, 1f));

        switch (numberOfButton)
        {
            case 0:
                SceneManager.LoadScene(_sceneToLoadAfterPressedBack);
                break;
            case 1:
                SceneManager.LoadScene(_sceneToLoadAfterPressedRestartAndNextLvl);
                break;

        }
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

    IEnumerator DelayBeforeWinOrLose()
    {
        yield return new WaitForSeconds(5f);
        _blurFrameInGameGO.SetActive(true);
        _winScreen.SetActive(true);
    }

}
