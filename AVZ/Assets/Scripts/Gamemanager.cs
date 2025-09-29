using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager _;

    public enum PauseScreenContainer { pause, mainmenu, restart, ru, en };
    public enum WinScreenContainer { mainmenu, nextlvl };
    public enum LoseScreenContainer { mainmenu, restart, revive };

    AudioManager audioManager;

    public GameObject currentAnimal;
    public GameObject currentCrocodile;
    public Sprite currentAnimalSprite;
    public Transform tiles;
    public LayerMask tileMask;
    public int coffees;
    public TextMeshProUGUI coffeeText;

    public int preCurrentAmount = -1;
    public TMP_Text coinDisplay;

    public int cardAmount;
    public int crocodileCount = 2;
    public bool isGameStarted;
    public bool isGamePaused;
    public bool gameWon;
    private bool _canRevive = true;

    [SerializeField] CanvasGroup _fadeCanvasGroup, Canvas;
    [SerializeField] GameObject _pauseScreen, _winScreen, _loseScreen, _blurFrameInGameGO, _pauseB, _homeB, _exitText;
    [SerializeField] int _sceneToLoadAfterPressedBack, _sceneToLoadAfterPressedRestartAndNextLvl;
    [SerializeField] float _fadeDuration = 1f;
    [SerializeField] Animator _PauseScreenA, _LoseScreenA, _WinScreenA;
    public GameObject _speechBubbleCS, _pauseButton, blurFrameInAnimalCardSelectorGO;

    private Lose lose;

    public void Awake()
    {
        if (_ == null)
            _ = this;
        else
            Debug.LogError("There are more than 1 Gamanager in the scene");
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        audioManager.PlaySFX(audioManager.buttonClicked2);
        StartCoroutine(Fade(1f, 0f));
        coinDisplay.SetText(YG2.saves.playerCoins + "");
        _pauseButton.SetActive(false);
        _blurFrameInGameGO.SetActive(false);
        blurFrameInAnimalCardSelectorGO.SetActive(true);

        _homeB.SetActive(false);
        _exitText.SetActive(false);

        lose = GameObject.Find("LoseTrigger").GetComponent<Lose>();

        foreach (string word in YG2.saves.consumableItems)
        {
            if (word == "crocodilo")
            {
                crocodileCount++;
            }
        }
    }
    public void Win(int starsAquired)
    {
        gameWon = true;
        audioManager.PlaySFX(audioManager.endOfLvl);
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

    public void BuyAnimal(GameObject animal, Sprite sprite = null)
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

        if (hit.collider && currentAnimal && !currentCrocodile)
        {
            hit.collider.GetComponent<SpriteRenderer>().sprite = currentAnimalSprite;
            hit.collider.GetComponent<SpriteRenderer>().enabled = true;

            if (Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>().hasAnimal)
            {
                Animal(hit.collider.gameObject);
            }
        }

        if (hit.collider && currentCrocodile && currentAnimal)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hit.collider.GetComponent<CrocoTile>().SpawnCrocodilo();
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
                audioManager.PlaySFX(audioManager.buttonClicked);
                isGamePaused = false;
                StartCoroutine(TransitionScene(0));
                break;
            case PauseScreenContainer.pause:
                audioManager.PlaySFX(audioManager.buttonClicked2);
                StartCoroutine(PlayPauseAnimation(isGamePaused));
                break;
            case PauseScreenContainer.restart:
                Time.timeScale = 1;
                audioManager.PlaySFX(audioManager.buttonClicked);
                isGamePaused = false;
                StartCoroutine(TransitionScene(1));
                break;
            case PauseScreenContainer.ru:
                audioManager.PlaySFX(audioManager.buttonClicked);
                YG2.SwitchLanguage("ru");
                break;
            case PauseScreenContainer.en:
                audioManager.PlaySFX(audioManager.buttonClicked);
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
                audioManager.PlaySFX(audioManager.buttonClicked);
                StartCoroutine(TransitionScene(0));
                break;
            case WinScreenContainer.nextlvl:
                Time.timeScale = 1;
                audioManager.PlaySFX(audioManager.buttonClicked);
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
                audioManager.PlaySFX(audioManager.buttonClicked);
                StartCoroutine(TransitionScene(0));
                break;
            case LoseScreenContainer.restart:
                Time.timeScale = 1;
                audioManager.PlaySFX(audioManager.buttonClicked);
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
                    audioManager.SwitchMusic(audioManager.inGameMusic);
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
            _homeB.SetActive(true);
            _pauseB.SetActive(false);
            _exitText.SetActive(true);
            yield return new WaitForSeconds(_PauseScreenA.GetCurrentAnimatorStateInfo(0).length);
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
            _pauseB.SetActive(true);
            _homeB.SetActive(false);
            _exitText.SetActive(false);
            yield return new WaitForSeconds(_PauseScreenA.GetCurrentAnimatorStateInfo(0).length);
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
        audioManager.StopMusic();
        //_LoseScreenA.Play("LoseScreenDown");
        audioManager.PlaySFX(audioManager.lose);
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
        audioManager.StopMusic();
        //_WinScreenA.Play("WinScreenDown");
        audioManager.PlaySFX(audioManager.win);
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
                audioManager.SwitchMusic(audioManager.mainMenuMusic);
                break;
            case 1:
                SceneManager.LoadScene(_sceneToLoadAfterPressedRestartAndNextLvl);
                audioManager.SwitchMusic(audioManager.mainMenuMusic);
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
