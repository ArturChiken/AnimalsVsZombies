using UnityEngine;
using System.Collections;
using YG;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager _;

    AudioManager audioManager;

    [SerializeField] int _sceneToLoadAfterPressedBack;
    [SerializeField] float _fadeDuration = 1f;
    [SerializeField] CanvasGroup _fadeCanvasGroup;

    public enum LeaderboardContainerButtons { back, tg };

    public void Awake()
    {
        if (_ == null)
            _ = this;
        else
            Debug.LogError("There are more than 1 ShopManager in the scene");
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        audioManager.PlaySFX(audioManager.leaderboardEntry);    
    }

    public void LeaderboardContainerButtonsClicked(LeaderboardContainerButtons buttonType)
    {
        string websiteLink = "";
        switch (buttonType)
        {
            case LeaderboardContainerButtons.back:
                YG2.InterstitialAdvShow();
                StartCoroutine(TransitionScene());
                audioManager.PlaySFX(audioManager.buttonClicked2);
                break;
            case LeaderboardContainerButtons.tg:
                websiteLink = "https://t.me/afterpartygames";
                if (websiteLink != "")
                {
                    Application.OpenURL(websiteLink);
                }
                break;
        }
    }
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
