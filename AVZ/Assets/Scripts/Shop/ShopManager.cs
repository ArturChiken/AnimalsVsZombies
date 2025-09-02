using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using YG;

public class ShopManager : MonoBehaviour
{
    //синглтон паттерн постройки файла, иниц. инстанса этого класса
    public static ShopManager _;

    //префы

    //только back и tg
    public enum ShopContainerButtons { back, tg, shopitem };
    public enum PreviewContainerButtons { back };

    public int preCurrentAmount = -1;

    [SerializeField] CanvasGroup _fadeCanvasGroup;
    [SerializeField] GameObject _ShopContainer, _PreviewContainer;
    [SerializeField] int _sceneToLoadAfterPressedBack;
    [SerializeField] float _fadeDuration = 1f;

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
                _ShopContainer.SetActive(false);
                _PreviewContainer.SetActive(true);
                break;
            case ShopContainerButtons.back:
                StartCoroutine(TransitionScene());
                break;
            case ShopContainerButtons.tg:
                websiteLink = "https://t.me/afterpartygames";
                if (websiteLink != "")
                {
                    Application.OpenURL(websiteLink);
                }
                break;
        }
    }

    public void PreviewContainerButtonsClicked(PreviewContainerButtons buttonClicked)
    {
        switch (buttonClicked)
        {
            case PreviewContainerButtons.back:
                _PreviewContainer.SetActive(false);
                _ShopContainer.SetActive(true);
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
        if (YG2.saves.playerCoins < item.cost || SaveSystem.IsItemUnlocked(item.itemId))
        {
            Debug.LogWarning("Not enough coins or already bought");
            return false;
        }

        YG2.saves.playerCoins -= item.cost;
        SaveSystem.UnlockItem(item.itemId);


        Debug.Log($"Item {item.name} purchased successfully!");
        return true;

    }

    public static bool BuyCrocodilo(ShopItemScriptableObject crocodilo)
    {
        if (YG2.saves.playerCoins < crocodilo.cost)
        {
            Debug.LogWarning("Not enough coins or already bought");
            return false;
        }
        YG2.saves.playerCoins -= crocodilo.cost;
        SaveSystem.UnlockItem(crocodilo.itemId);

        YG2.saves.crocodiloUses += 1;

        Debug.Log($"Item {crocodilo.name} purchased successfully!");
        return true;
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
