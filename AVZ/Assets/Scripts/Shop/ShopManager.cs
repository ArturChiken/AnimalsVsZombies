using System.Collections;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using static LevelSelectorManager;

public class ShopManager : MonoBehaviour
{
    //синглтон паттерн постройки файла, иниц. инстанса этого класса
    public static ShopManager _;

    bool shopIsActive = true;
    bool previewIsActive = false;
    bool donateIsActive = false;

    public enum ShopContainerButtons { back, tg, shopitem, donate };
    public enum PreviewContainerButtons { back };
    public enum DonateContainerButtons { back };

    public int preCurrentAmount = -1;

    [SerializeField] CanvasGroup _fadeCanvasGroup, _ShopContainerButtons, _PreviewContainerButtons, _DonateContainerButtons;
    [SerializeField] GameObject _ShopContainer, _PreviewContainer, DonateSpeechbubble, NoLCSpeechBubble, AlreadyBoughtSpeechBubble, _DonateContainer;
    [SerializeField] Animator _ShopAnimator, _PreviewAnimator, _DonateAnimator;
    [SerializeField] int _sceneToLoadAfterPressedBack;
    [SerializeField] float _fadeDuration = 1f;

    [SerializeField] Button DonateButton1;
    [SerializeField] Button DonateButton2;

    public PreviewScriptableObject _activePreviewSO;

    public ShopItemScriptableObject _activeShopItemSOInPreview;

    public TMP_Text coinDisplay;

    public void Awake()
    {
        if (_ == null)
            _ = this;
        else
            Debug.LogError("There are more than 1 ShopManager in the scene");
    }

    public void Start()
    {
        StartCoroutine(Fade(1f, 0f));
        coinDisplay.SetText(YG2.saves.playerCoins + "");
    }

    public void Update()
    {
        if (preCurrentAmount != YG2.saves.playerCoins)
        {
            preCurrentAmount = YG2.saves.playerCoins;
            coinDisplay.SetText(YG2.saves.playerCoins + "");
        }
    }
    public void ShopContainerButtonsClicked(ShopContainerButtons buttonClicked)
    {
        string websiteLink = "";
        switch (buttonClicked)
        {
            case ShopContainerButtons.shopitem:
                StartCoroutine(PlaySwitchPreviewShopAnimation(previewIsActive));
                shopIsActive = false;
                break;
            case ShopContainerButtons.back:
                if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
                StartCoroutine(TransitionScene());
                break;
            case ShopContainerButtons.tg:
                websiteLink = "https://t.me/afterpartygames";
                if (websiteLink != "")
                {
                    Application.OpenURL(websiteLink);
                }
                break;
            case ShopContainerButtons.donate:
                if (shopIsActive)
                {
                    StartCoroutine(PlaySwitchDonateShopAnimation(donateIsActive));
                }
                else
                {
                    StartCoroutine(PlaySwitchDonatePreviewAnimation(donateIsActive));
                }
                DonateButton1.interactable = false;
                DonateButton2.interactable = false;
                break;
        }
    }

    public void PreviewContainerButtonsClicked(PreviewContainerButtons buttonClicked)
    {
        switch (buttonClicked)
        {
            case PreviewContainerButtons.back:
                StartCoroutine(PlaySwitchPreviewShopAnimation(previewIsActive));
                shopIsActive = true;
                break;
        }
    }

    public void DonateContainerButtonsClicked(DonateContainerButtons buttonClicked)
    {
        switch (buttonClicked)
        {
            case DonateContainerButtons.back:
                _DonateContainer.SetActive(false);
                DonateButton1.interactable = true;
                DonateButton2.interactable = true;
                break;
        }
    }

    //смена SO в превью
    public void ChangePreviewSO(PreviewScriptableObject _newPreviewSO)
    {
        _activePreviewSO = _newPreviewSO;
        PreviewCard._?.UpdateUI();
    }

    public void ChangeShopItemSOInPreview(ShopItemScriptableObject _newShopItemSOInPreview)
    {
        _activeShopItemSOInPreview = _newShopItemSOInPreview;
    }

    public static bool BuyInfiniteItem(ShopItemScriptableObject item)
    {
        if (YG2.saves.playerCoins < item.cost)
        {
            _.NoLCSpeechBubble.SetActive(true);
            _.DonateSpeechbubble.SetActive(true);
            return false;
        }

        if (SaveSystem.IsItemUnlocked(item.itemId))
        {
            _.AlreadyBoughtSpeechBubble.SetActive(true);
            return false;
        }

        YG2.saves.playerCoins -= item.cost;
        SaveSystem.UnlockItem(item.itemId);


        YG2.SaveProgress();
        if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
        return true;

    }

    public static bool BuyCrocodilo(ShopItemScriptableObject crocodilo)
    {
        if (YG2.saves.playerCoins < crocodilo.cost)
        {
            Debug.LogWarning("Not enough coins or already bought");
            if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
            return false;
        }
        YG2.saves.playerCoins -= crocodilo.cost;
        SaveSystem.UnlockItem(crocodilo.itemId);

        YG2.saves.crocodiloUses += 1;

        Debug.Log($"Item {crocodilo.name} purchased successfully!");
        YG2.SaveProgress();
        if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
        return true;
    }

    private IEnumerator PlaySwitchPreviewShopAnimation(bool show)
    {
        if (show)
        {
            DonateButton1.interactable = false;
            DonateButton2.interactable = false;
            _PreviewContainerButtons.interactable = false;
            _PreviewContainerButtons.blocksRaycasts = false;
            _PreviewAnimator.Play("PreviewUp");
            yield return new WaitForSeconds(_PreviewAnimator.GetCurrentAnimatorStateInfo(0).length);
            _PreviewContainer.SetActive(false);
            NoLCSpeechBubble.SetActive(false);
            AlreadyBoughtSpeechBubble.SetActive(false);
            DonateSpeechbubble.SetActive(false);
            _ShopContainer.SetActive(true);
            _ShopAnimator.Play("ShopDown");
            yield return new WaitForSeconds(_ShopAnimator.GetCurrentAnimatorStateInfo(0).length);
            _ShopContainerButtons.interactable = true;
            _ShopContainerButtons.blocksRaycasts = true;
            DonateButton1.interactable = true;
            DonateButton2.interactable = true;
        }
        else
        {
            DonateButton1.interactable = false;
            DonateButton2.interactable = false;
            _ShopContainerButtons.interactable = false;
            _ShopContainerButtons.blocksRaycasts = false;
            _ShopAnimator.Play("ShopUp");
            yield return new WaitForSeconds(_ShopAnimator.GetCurrentAnimatorStateInfo(0).length);
            _ShopContainer.SetActive(false);
            _PreviewContainer.SetActive(true);
            _PreviewAnimator.Play("PreviewDown");
            yield return new WaitForSeconds(_PreviewAnimator.GetCurrentAnimatorStateInfo(0).length);
            _PreviewContainerButtons.interactable = true;
            _PreviewContainerButtons.blocksRaycasts = true;
            DonateButton1.interactable = true;
            DonateButton2.interactable = true;
        }
        previewIsActive = !previewIsActive;
    }

    private IEnumerator PlaySwitchDonatePreviewAnimation(bool show)
    {
        if (show)
        {
            DonateButton1.interactable = false;
            DonateButton2.interactable = false;
            _DonateContainerButtons.interactable = false;
            _DonateContainerButtons.blocksRaycasts = false;
            _ShopAnimator.Play("DonateUp");
            yield return new WaitForSeconds(_DonateAnimator.GetCurrentAnimatorStateInfo(0).length);
            _DonateContainer.SetActive(false);
            _PreviewContainer.SetActive(true);
            _PreviewAnimator.Play("PreviewDown");
            yield return new WaitForSeconds(_PreviewAnimator.GetCurrentAnimatorStateInfo(0).length);
            _PreviewContainerButtons.interactable = true;
            _PreviewContainerButtons.blocksRaycasts = true;
            DonateButton1.interactable = true;
            DonateButton2.interactable = true;
        }
        else
        {
            DonateButton1.interactable = false;
            DonateButton2.interactable = false;
            _PreviewContainerButtons.interactable = false;
            _PreviewContainerButtons.blocksRaycasts = false;
            _PreviewAnimator.Play("PreviewUp");
            yield return new WaitForSeconds(_PreviewAnimator.GetCurrentAnimatorStateInfo(0).length);
            _PreviewContainer.SetActive(false);
            NoLCSpeechBubble.SetActive(false);
            AlreadyBoughtSpeechBubble.SetActive(false);
            DonateSpeechbubble.SetActive(false);
            _DonateContainer.SetActive(true);
            _DonateAnimator.Play("DonateDown");
            yield return new WaitForSeconds(_DonateAnimator.GetCurrentAnimatorStateInfo(0).length);
            _DonateContainerButtons.interactable = true;
            _DonateContainerButtons.blocksRaycasts = true;
            DonateButton1.interactable = true;
            DonateButton2.interactable = true;
        }
        donateIsActive = !donateIsActive;
    }

    private IEnumerator PlaySwitchDonateShopAnimation(bool show)
    {
        if (show)
        {
            DonateButton1.interactable = false;
            DonateButton2.interactable = false;
            _DonateContainerButtons.interactable = false;
            _DonateContainerButtons.blocksRaycasts = false;
            _ShopAnimator.Play("DonateUp");
            yield return new WaitForSeconds(_DonateAnimator.GetCurrentAnimatorStateInfo(0).length);
            _DonateContainer.SetActive(false);
            _ShopContainer.SetActive(true);
            _ShopAnimator.Play("ShopDown");
            yield return new WaitForSeconds(_ShopAnimator.GetCurrentAnimatorStateInfo(0).length);
            _ShopContainerButtons.interactable = true;
            _ShopContainerButtons.blocksRaycasts = true;
            DonateButton1.interactable = true;
            DonateButton2.interactable = true;
        }
        else
        {
            DonateButton1.interactable = false;
            DonateButton2.interactable = false;
            _ShopContainerButtons.interactable = false;
            _ShopContainerButtons.blocksRaycasts = false;
            _ShopAnimator.Play("ShopUp");
            yield return new WaitForSeconds(_ShopAnimator.GetCurrentAnimatorStateInfo(0).length);
            _ShopContainer.SetActive(false);
            _DonateContainer.SetActive(true);
            _DonateAnimator.Play("DonateDown");
            yield return new WaitForSeconds(_DonateAnimator.GetCurrentAnimatorStateInfo(0).length);
            _DonateContainerButtons.interactable = true;
            _DonateContainerButtons.blocksRaycasts = true;
            DonateButton1.interactable = true;
            DonateButton2.interactable = true;
        }
        donateIsActive = !donateIsActive;
    }



    //переход
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

}
