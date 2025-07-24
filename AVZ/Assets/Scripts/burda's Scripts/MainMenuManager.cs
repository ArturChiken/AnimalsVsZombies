using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager _;
    bool nameholderIsActive = false;
    public enum MenuButtons { playAdv, playInf, shop };
    public enum OtherButtons { tg, nameholder, options };
    public enum CreditsButtons { back, devteam, artist };
    public enum OptionsButtons { back, credits };

    [SerializeField] CanvasGroup _MainMenuCanvasGroup;
    [SerializeField] GameObject _MainMenuContainer;
    [SerializeField] GameObject _OptionsContainer;
    [SerializeField] GameObject _CreditsContainer;
    [SerializeField] GameObject _LeaderboardFrame;
    [SerializeField] GameObject _AchievementsFrame;
    [SerializeField] int _sceneToLoadAfterPlayAdvPressed;
    [SerializeField] int _sceneToLoadAfterPlayInfPressed;
    [SerializeField] int _sceneToLoadAfterShopPressed;

    public void Awake()
    {
        if (_ == null)
            _ = this;
        else
            Debug.LogError("There are more than 1 MainMenuManager's in the scene");
    }

    private void Start()
    {
        _MainMenuContainer.SetActive(_MainMenuContainer);
        _LeaderboardFrame.SetActive(false);
        _AchievementsFrame.SetActive(false);
        _OptionsContainer.SetActive(false);
        _CreditsContainer.SetActive(false);
    }

    public void MenuButtonClicked(MenuButtons buttonClicked)
    {
        switch (buttonClicked)
        {
            case MenuButtons.playAdv:
                PlayAdvClicked();
                break;
            case MenuButtons.playInf:
                PlayInfClicked();
                break;
            case MenuButtons.shop:
                ShopClicked();
                break;
            default:
                Debug.Log("Button has no use");
                break;
        }
    }

    public void OtherButtonsClicked(OtherButtons buttonClicked)
    {
        string websiteLink = "";
        switch (buttonClicked)
        {
            case OtherButtons.options:
                _MainMenuCanvasGroup.interactable = false;
                _MainMenuCanvasGroup.blocksRaycasts = false;
                _OptionsContainer.SetActive(true);
                
                break;
            case OtherButtons.nameholder:
                switch (nameholderIsActive)
                {
                    case false:
                        _LeaderboardFrame.SetActive(true);
                        _AchievementsFrame.SetActive(true);
                        nameholderIsActive = true;
                        break;
                    case true:
                        _LeaderboardFrame.SetActive(false);
                        _AchievementsFrame.SetActive(false);
                        nameholderIsActive = false;
                        break;
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
    

    public void OptionsButtonClicked(OptionsButtons buttonClicked)
    {
        switch (buttonClicked)
        {
            case OptionsButtons.credits:
                _CreditsContainer.SetActive(true);
                _OptionsContainer.SetActive(false);
                break;
            case OptionsButtons.back:
                _MainMenuCanvasGroup.interactable = true;
                _MainMenuCanvasGroup.blocksRaycasts = true;
                _OptionsContainer.SetActive(false);
                break;
        }
    }
    public void CreditsButtonClicked(CreditsButtons buttonClicked)
    {
        string websiteLink = "";
        switch (buttonClicked)
        {
            case CreditsButtons.back:
                _MainMenuCanvasGroup.interactable = true;
                _MainMenuCanvasGroup.blocksRaycasts = true;
                _CreditsContainer.SetActive(false);
                break;
            case CreditsButtons.devteam:
                websiteLink = "https://t.me/afterpartygames";
                if (websiteLink != "")
                {
                    Application.OpenURL(websiteLink);
                }
                break;
            case CreditsButtons.artist:
                websiteLink = "https://t.me/Jimmywest";
                if (websiteLink != "")
                {
                    Application.OpenURL(websiteLink);
                }
                break;
        }
    }

    public void PlayAdvClicked()
    {
        SceneManager.LoadScene(_sceneToLoadAfterPlayAdvPressed);
    }
    public void PlayInfClicked()
    {
        SceneManager.LoadScene(_sceneToLoadAfterPlayInfPressed);
    }
    public void ShopClicked()
    {
        SceneManager.LoadScene(_sceneToLoadAfterShopPressed);
    }
}