using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class MainMenuManager : MonoBehaviour
{
    //синглтон паттерн постройки файла, иниц. инстанса этого класса
    public static MainMenuManager _;

    bool nameholderIsActive = false;
    public enum MenuButtons { playAdv, playInf, shop };
    public enum OtherButtons { tg, nameholder, leaderboard, options };
    public enum CreditsButtons { back, artur, renat, dmitriy };
    public enum OptionsButtons { back, credits };

    [SerializeField] CanvasGroup _MainMenuCanvasGroup, _fadeCanvasGroup;
    [SerializeField] GameObject _BlurFrame, _MainMenuContainer, _OptionsContainer, _CreditsContainer, _LeaderboardFrame;
    [SerializeField] Animator _nameholderAnimator;
    [SerializeField] int _sceneToLoadAfterPlayAdvPressed, _sceneToLoadAfterShopPressed, _sceneToLoadAfterPlayInfPressed, _sceneToLoadAfterLeaderboardPressed;
    [SerializeField] float _fadeDuration = 1f;

    public void Awake()
    {
        if (_ == null)
            _ = this;
        else
            Debug.LogError("There are more than 1 MainMenuManager's in the scene");
    }

    private void Start()
    {
        StartCoroutine(Fade(1f, 0f));
        _MainMenuContainer.SetActive(_MainMenuContainer);
        _LeaderboardFrame.SetActive(false);
        _OptionsContainer.SetActive(false);
        _CreditsContainer.SetActive(false);
        _BlurFrame.SetActive(false);
    }
    //кнопки на меню
    public void MenuButtonClicked(MenuButtons buttonClicked)
    {
        switch (buttonClicked)
        {
            case MenuButtons.playAdv:
                if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
                PlayAdvClicked();
                break;
            case MenuButtons.playInf:
                if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
                PlayInfClicked();
                break;
            case MenuButtons.shop:
                if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
                ShopClicked();
                break;
            default:
                Debug.Log("Button has no use");
                break;
        }
    }

    //кнопки не на меню
    public void OtherButtonsClicked(OtherButtons buttonClicked)
    {
        string websiteLink = "";
        switch (buttonClicked)
        {
            case OtherButtons.options:
                if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
                _MainMenuCanvasGroup.interactable = false;
                _MainMenuCanvasGroup.blocksRaycasts = false;
                _OptionsContainer.SetActive(true);
                _BlurFrame.SetActive(true);
                break;
            case OtherButtons.nameholder:
                if (nameholderIsActive)
                {
                    StartCoroutine(PlayNameholderAnimation(false));
                }
                else
                {
                    StartCoroutine(PlayNameholderAnimation(true));
                }
                break;
            case OtherButtons.leaderboard:
                {
                    if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
                    LeaderboardClicked();
                }
                break;
            case OtherButtons.tg:
                websiteLink = "https://t.me/afterpartygames";
                if (websiteLink != "")
                {
                    Application.OpenURL(websiteLink);
                }
                break;
        }

    }

    // анимации неймхолдера
    private IEnumerator PlayNameholderAnimation(bool show)
    {
        if (show)
        {
            _LeaderboardFrame.SetActive(true);
            _nameholderAnimator.Play("LeaderboardSlideDown");
        }
        else
        {
            _nameholderAnimator.Play("LeaderboardSlideUp");
            yield return new WaitForSeconds(_nameholderAnimator.GetCurrentAnimatorStateInfo(0).length);
            _LeaderboardFrame.SetActive(false);
        }
        nameholderIsActive = !nameholderIsActive;
    }


    public void OptionsButtonClicked(OptionsButtons buttonClicked)
    {
        switch (buttonClicked)
        {
            case OptionsButtons.credits:
                _CreditsContainer.SetActive(true);
                StartCoroutine(DelayedCreditsAction());
                break;
            case OptionsButtons.back:
                StartCoroutine(DelayedOptionsBackAction());
                break;
        }
    }
    //код https://t.me/bburda1
    // дилей для загрузки анимации при нажатии Заслуги в Опшнс

    private IEnumerator DelayedCreditsAction()
    {
        yield return new WaitForSeconds(1.2f);
        _OptionsContainer.SetActive(false);
    }

    // дилей для загрузки анимации при нажатии принять в Опшнс

    private IEnumerator DelayedOptionsBackAction()
    {
        yield return new WaitForSeconds(1.1f);
        _MainMenuCanvasGroup.interactable = true;
        _MainMenuCanvasGroup.blocksRaycasts = true;
        _OptionsContainer.SetActive(false);
        _BlurFrame.SetActive(false);
    }


    public void CreditsButtonClicked(CreditsButtons buttonClicked)
    {
        string websiteLink = "";
        switch (buttonClicked)
        {
            case CreditsButtons.back:
                StartCoroutine(DelayedCreditsBackAction());
                break;
            case CreditsButtons.artur:
                websiteLink = "https://t.me/ArturChiken";
                {
                    Application.OpenURL(websiteLink);
                }
                break;
            case CreditsButtons.dmitriy:
                websiteLink = "https://t.me/Jimmywest";
                if (websiteLink != "")
                {
                    Application.OpenURL(websiteLink);
                }
                break;
            case CreditsButtons.renat:
                websiteLink = "https://t.me/bburda1";
                if (websiteLink != "")
                {
                    Application.OpenURL(websiteLink);
                }
                break;
        }
    }

    // дилей для загрузки анимации при нажатии ОК в Заслугах
    private IEnumerator DelayedCreditsBackAction()
    {
        yield return new WaitForSeconds(1.1f);
        _MainMenuCanvasGroup.interactable = true;
        _MainMenuCanvasGroup.blocksRaycasts = true;
        _CreditsContainer.SetActive(false);
        _BlurFrame.SetActive(false);
    }

    // кнопки меню
    public void PlayAdvClicked()
    {
        StartCoroutine(TransitionScene(1));
    }
    public void PlayInfClicked()
    {
        StartCoroutine(TransitionScene(2));
    }
    //код https://t.me/bburda1
    public void ShopClicked()
    {
        StartCoroutine(TransitionScene(3));
    }

    public void LeaderboardClicked()
    {
        StartCoroutine(TransitionScene(4));
    }

    // затемнение+переход на сцену
    private IEnumerator TransitionScene(int numberOfButton)
    {
        yield return StartCoroutine(Fade(0f, 1f));

        switch (numberOfButton)
        {
            case 1:
                SceneManager.LoadScene(_sceneToLoadAfterPlayAdvPressed);
                break;
            case 2:
                SceneManager.LoadScene(_sceneToLoadAfterPlayInfPressed);
                break;
            case 3:
                SceneManager.LoadScene(_sceneToLoadAfterShopPressed);
                break;
            case 4:
                SceneManager.LoadScene(_sceneToLoadAfterLeaderboardPressed);
                break;
        }

    }

    // функция затемнения
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