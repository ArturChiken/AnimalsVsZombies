using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using static MainMenuManager;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager _;

    public enum PauseScreenContainer { pause, mainmenu, restart, ru, en };
    public enum WinScreenContainer { mainmenu, nextlvl };
    public enum LoseScreenContainer { mainmenu, restart, revive };

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
    private bool _canRevive = true;
    public Sprite resumeButtonSprite;
    public Sprite pauseButtonSprite;

    [SerializeField] CanvasGroup _fadeCanvasGroup, Canvas;
    [SerializeField] GameObject _pauseScreen, _winScreen, _loseScreen, _blurFrameInGameGO;
    [SerializeField] int _sceneToLoadAfterPressedBack, _sceneToLoadAfterPressedRestartAndNextLvl;
    [SerializeField] float _fadeDuration = 1f;
    [SerializeField] Animator _PauseScreenA, _LoseScreenA, _WinScreenA;
    public GameObject _speechBubbleCS, _pauseButton, blurFrameInAnimalCardSelectorGO;

    public Button pauseButton;

    private Lose lose;

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

        lose = GameObject.Find("LoseTrigger").GetComponent<Lose>();
    }
    public void Win(int starsAquired)
    {
        StartCoroutine(DelayBeforeWin());
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
        StartCoroutine(PlayLoseAnimation());
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

    public void PauseButtonClicked(PauseScreenContainer buttonClicked)
    {
        Image pauseButtonImage = pauseButton.GetComponent<Image>();
        switch (buttonClicked)
        {
            case PauseScreenContainer.mainmenu:
                Time.timeScale = 1;
                isGamePaused = false;
                StartCoroutine(TransitionScene(0));
                break;
            case PauseScreenContainer.pause:
                StartCoroutine(PlayPauseAnimation(isGamePaused));
                break;
            case PauseScreenContainer.restart:
                Time.timeScale = 1;
                isGamePaused = false;
                StartCoroutine(TransitionScene(1));
                break;
            case PauseScreenContainer.ru:
                YG2.SwitchLanguage("ru");
                break;
            case PauseScreenContainer.en:
                YG2.SwitchLanguage("en");
                break;
        }
    }

    public void WinButtonClicked(WinScreenContainer buttonClicked)
    {
        switch (buttonClicked)
        {
            case WinScreenContainer.mainmenu:
                Time.timeScale = 1;
                StartCoroutine(TransitionScene(0));
                break;
            case WinScreenContainer.nextlvl:
                Time.timeScale = 1;
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
                Time.timeScale = 1;
                StartCoroutine(TransitionScene(0));
                break;
            case LoseScreenContainer.restart:
                Time.timeScale = 1;
                StartCoroutine(TransitionScene(1));
                break;
            case LoseScreenContainer.revive:
                if (_canRevive)
                {
                    _canRevive = false;
                    YG2.RewardedAdvShow("extraLife");
                    isGameStarted = true;
                    lose.isGameFinish = false;
                    YG2.SaveProgress();
                    _pauseButton.SetActive(true);
                    _blurFrameInGameGO.SetActive(false);
                    _loseScreen.SetActive(false);
                    Time.timeScale = 1;
                    Invoke("RestartAdv", 100f);
                }
                else
                {
                    CancelInvoke("RestartAdv");
                    _canRevive = false;
                    Debug.Log("You can't be revived");
                }
                    break;
        }
    }

    public IEnumerator PlayPauseAnimation(bool isPaused)
    {
        if (!isPaused)
        {
            Canvas.interactable = false;
            Canvas.blocksRaycasts = false;
            _blurFrameInGameGO.SetActive(true);
            _pauseScreen.SetActive(true);
            _PauseScreenA.Play("PauseScreenDown");
            yield return new WaitForSeconds(_PauseScreenA.GetCurrentAnimatorStateInfo(0).length);
            pauseButton.image.sprite = resumeButtonSprite;
            Time.timeScale = 0;
            Canvas.interactable = true;
            Canvas.blocksRaycasts = true;
            
        }
        else
        {
            Time.timeScale = 1;
            Canvas.interactable = false;
            Canvas.blocksRaycasts = false;
            _PauseScreenA.Play("PauseScreenUp");
            _blurFrameInGameGO.SetActive(false);
            yield return new WaitForSeconds(_PauseScreenA.GetCurrentAnimatorStateInfo(0).length);
            pauseButton.image.sprite = pauseButtonSprite;
            _pauseScreen.SetActive(false);
            Canvas.interactable = true;
            Canvas.blocksRaycasts = true;
        }
        YG2.SaveProgress();
        isGamePaused = !isGamePaused;
    }

    public IEnumerator PlayLoseAnimation()
    {
        Canvas.interactable = false;
        Canvas.blocksRaycasts = false;
        YG2.SaveProgress();
        _pauseButton.SetActive(false);
        _blurFrameInGameGO.SetActive(true);
        _loseScreen.SetActive(true);
        //_LoseScreenA.Play("LoseScreenDown");
        yield return new WaitForSeconds(_LoseScreenA.GetCurrentAnimatorStateInfo(0).length);
        Canvas.interactable = true;
        Canvas.blocksRaycasts = true;
        Time.timeScale = 0;
    }

    IEnumerator DelayBeforeWin()
    {
        Canvas.interactable = false;
        Canvas.blocksRaycasts = false;
        yield return new WaitForSeconds(5f);
        _blurFrameInGameGO.SetActive(true);
        _winScreen.SetActive(true);
        //_WinScreenA.Play("WinScreenDown");
        yield return new WaitForSeconds(_WinScreenA.GetCurrentAnimatorStateInfo(0).length);
        if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
        Canvas.interactable = true;
        Canvas.blocksRaycasts = true;
        Time.timeScale = 0;
    }

    public void RestartAdv()
    {
        Debug.Log("now You can be revived");
        _canRevive = true;
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


}
