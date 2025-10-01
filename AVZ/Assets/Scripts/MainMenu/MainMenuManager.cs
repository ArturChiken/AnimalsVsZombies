using System.Collections;
using System.IO.IsolatedStorage;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenuManager : MonoBehaviour
{
    //�������� ������� ��������� �����, ����. �������� ����� ������
    public static MainMenuManager _;

    AudioManager audioManager;

    bool nameholderIsActive = false;
    public enum MenuButtons { playAdv, playInf, shop };
    public enum OtherButtons { tg, nameholder, leaderboard, options, review };
    public enum CreditsButtons { back, dmitriy };
    public enum OptionsButtons { back, credits, ru, en };

    [SerializeField] CanvasGroup _MainMenuCanvasGroup, _fadeCanvasGroup;
    [SerializeField] GameObject _BlurFrame, _MainMenuContainer, _OptionsContainer, _CreditsContainer, _LeaderboardFrame, _Speechbubbles, _ClickOnMeSB, _YouCanOpenInfLevelSB, _AuthorizeToOpenLeaderboardSB, _SBs, _CheckLBSB, _reviewB;
    [SerializeField] TMP_Text _NameholderText;
    [SerializeField] Animator _nameholderAnimator;
    [SerializeField] int _sceneToLoadAfterPlayAdvPressed, _sceneToLoadAfterShopPressed, _sceneToLoadAfterPlayInfPressed, _sceneToLoadAfterLeaderboardPressed;
    [SerializeField] float _fadeDuration = 1f;
    [SerializeField] Button _infLevelB, _lbButton;
    [SerializeField] TMP_Text _leaderboardText, _clickOnMeText;

    public void Awake()
    {
        if (_ == null)
            _ = this;
        else
            Debug.LogError("There are more than 1 MainMenuManager's in the scene");
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        StartCoroutine(Fade(1f, 0f));
        _MainMenuContainer.SetActive(_MainMenuContainer);
        _LeaderboardFrame.SetActive(false);
        _OptionsContainer.SetActive(false);
        _CreditsContainer.SetActive(false);
        _BlurFrame.SetActive(false);

        if (YG2.player.auth) _NameholderText.SetText(YG2.player.name);
        else
        {
            switch (YG2.lang)
            {
                case "ru":
                    _NameholderText.SetText("�� �����������");
                    break;
                case "en":
                    _NameholderText.SetText("Unauthorized");
                    break;
            }
        }

        _SBs.SetActive(true);

        if (Random.Range(0f, 1f) < .3f && YG2.reviewCanShow) _reviewB.SetActive(true);

        if (YG2.saves.isFirstEntry)
        {
            _ClickOnMeSB.SetActive(true);
        }
        else
        {
            _ClickOnMeSB.SetActive(false);
            _AuthorizeToOpenLeaderboardSB.SetActive(false);
        }

        if (YG2.saves.unlockedShopItems.Count(c => c == ',') < 2)
        {
            _infLevelB.interactable = false;
            _YouCanOpenInfLevelSB.SetActive(true);
        }
        else
        {
            _infLevelB.interactable = true;
            _YouCanOpenInfLevelSB.SetActive(false);
            if (YG2.player.auth)
            {
                if (Random.Range(0f, 1f) <= .25f)
                {
                    _CheckLBSB.SetActive(true);
                }
            }
            else
            {
                if (Random.Range(0f, 1f) <= .25f)
                {
                    _ClickOnMeSB.SetActive(true);
                }
            }
        }
    }
    //������ �� ����
    public void MenuButtonClicked(MenuButtons buttonClicked)
    {
        switch (buttonClicked)
        {
            case MenuButtons.playAdv:
                YG2.InterstitialAdvShow();
                PlayAdvClicked();
                break;
            case MenuButtons.playInf:
                YG2.InterstitialAdvShow();
                PlayInfClicked();
                break;
            case MenuButtons.shop:
                YG2.InterstitialAdvShow();
                ShopClicked();
                break;
            default:
                Debug.Log("Button has no use");
                break;
        }
    }

    //������ �� �� ����
    public void OtherButtonsClicked(OtherButtons buttonClicked)
    {
        string websiteLink = "";
        switch (buttonClicked)
        {
            case OtherButtons.options:
                YG2.InterstitialAdvShow();
                audioManager.PlaySFX(audioManager.buttonClicked2);
                _MainMenuCanvasGroup.interactable = false;
                _MainMenuCanvasGroup.blocksRaycasts = false;
                _OptionsContainer.SetActive(true);
                _BlurFrame.SetActive(true);
                break;
            case OtherButtons.nameholder:
                    audioManager.PlaySFX(audioManager.woodtable);
                    YG2.InterstitialAdvShow();
                    if (nameholderIsActive)
                    {
                        StartCoroutine(PlayNameholderAnimation(!nameholderIsActive));
                        _AuthorizeToOpenLeaderboardSB.SetActive(false);
                    }
                    else
                    {
                        StartCoroutine(PlayNameholderAnimation(!nameholderIsActive));
                        _ClickOnMeSB.SetActive(false);
                        if (!YG2.player.auth)
                        {
                            switch (YG2.lang)
                            {
                            case "ru":
                                _leaderboardText.SetText("��������������");
                                break;
                            case "en":
                                _leaderboardText.SetText("Log in");
                                break;
                            }
                             _AuthorizeToOpenLeaderboardSB.SetActive(true);
                        }
                        else
                        {
                             _AuthorizeToOpenLeaderboardSB.SetActive(false);
                        }

                        if (YG2.saves.isFirstEntry)
                        {
                            YG2.saves.isFirstEntry = false;
                            YG2.SaveProgress();
                        }
                    }
                break;  
            case OtherButtons.leaderboard:
                {
                    if (YG2.player.auth)
                    {
                        YG2.InterstitialAdvShow();
                        LeaderboardClicked();
                    }
                    else
                    {
                        YG2.OpenAuthDialog();
                        _AuthorizeToOpenLeaderboardSB.SetActive(false);
                        StartCoroutine(PlayNameholderAnimation(!nameholderIsActive));
                        audioManager.PlaySFX(audioManager.woodtable);
                        YG2.SaveProgress();
                    }
                }
                break;
            case OtherButtons.tg:
                websiteLink = "https://t.me/afterpartygames";
                if (websiteLink != "")
                {
                    Application.OpenURL(websiteLink);
                }
                break;
            case OtherButtons.review:
                audioManager.PlaySFX(audioManager.buttonClicked);
                YG2.ReviewShow();
                _reviewB.SetActive(false);
                break;
        }

    }

    // �������� �����������
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
                YG2.InterstitialAdvShow();
                _CreditsContainer.SetActive(true);
                StartCoroutine(DelayedCreditsAction());
                audioManager.PlaySFX(audioManager.buttonClicked2);
                break;
            case OptionsButtons.ru:
                YG2.InterstitialAdvShow();
                audioManager.PlaySFX(audioManager.buttonClicked);
                YG2.SwitchLanguage("ru");
                if (!YG2.player.auth) _NameholderText.SetText("�� �����������");
                break;
            case OptionsButtons.en:
                YG2.InterstitialAdvShow();
                audioManager.PlaySFX(audioManager.buttonClicked);
                YG2.SwitchLanguage("en");
                if (!YG2.player.auth) _NameholderText.SetText("Unauthorized");
                break;
            case OptionsButtons.back:
                YG2.InterstitialAdvShow();
                audioManager.PlaySFX(audioManager.buttonClicked);
                StartCoroutine(DelayedOptionsBackAction());
                break;
        }
    }
    //��� https://t.me/bburda1
    // ����� ��� �������� �������� ��� ������� ������� � �����

    private IEnumerator DelayedCreditsAction()
    {
        yield return new WaitForSeconds(1.2f);
        _OptionsContainer.SetActive(false);
    }

    // ����� ��� �������� �������� ��� ������� ������� � �����

    private IEnumerator DelayedOptionsBackAction()
    {
        yield return new WaitForSeconds(1.1f);
        _MainMenuCanvasGroup.interactable = true;
        _MainMenuCanvasGroup.blocksRaycasts = true;
        _OptionsContainer.SetActive(false);
        _BlurFrame.SetActive(false);
    }


    //  ��������: https://t.me/Jimmywest
    public void CreditsButtonClicked(CreditsButtons buttonClicked)
    {
        string websiteLink = "";
        switch (buttonClicked)
        {
            case CreditsButtons.back:
                YG2.InterstitialAdvShow();
                StartCoroutine(DelayedCreditsBackAction());
                audioManager.PlaySFX(audioManager.buttonClicked);
                break;
            case CreditsButtons.dmitriy:
                websiteLink = "https://t.me/afterpartygames/20";
                if (websiteLink != "")
                {
                    audioManager.PlaySFX(audioManager.buttonClicked);
                    Application.OpenURL(websiteLink);
                }
                break;
        }
    }

    // ����� ��� �������� �������� ��� ������� �� � ��������
    private IEnumerator DelayedCreditsBackAction()
    {
        yield return new WaitForSeconds(1.1f);
        _MainMenuCanvasGroup.interactable = true;
        _MainMenuCanvasGroup.blocksRaycasts = true;
        _CreditsContainer.SetActive(false);
        _BlurFrame.SetActive(false);
    }

    // ������ ����
    public void PlayAdvClicked()
    {
        //����� ��� https://t.me/ArturChiken
        StartCoroutine(TransitionScene(1));
    }
    public void PlayInfClicked()
    {
        StartCoroutine(TransitionScene(2));
    }
    //��� https://t.me/bburda1
    public void ShopClicked() // ��� ����� Z����, ����� ���, ���� �������� �������� ����
    {
        StartCoroutine(TransitionScene(3));
    }

    public void LeaderboardClicked()
    {
        StartCoroutine(TransitionScene(4));
    }

    // ����������+������� �� �����
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

    // ������� ����������
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