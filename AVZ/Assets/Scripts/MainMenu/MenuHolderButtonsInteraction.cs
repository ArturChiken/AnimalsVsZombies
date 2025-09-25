using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuHolderButtonsInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    AudioManager audioManager;

    [Header("Tooltip Settings")]
    public CanvasGroup[] tooltipCanvasGroup;
    public float fadeDuration = 0.3f;
    public float targetAlpha = 1f; 

    private Coroutine fadeCoroutine;
    private bool isShowing = false;

    void Start()
    {
        // Гарантируем, что подсказка полностью прозрачна в начале
        if (tooltipCanvasGroup != null)
        {
            foreach (CanvasGroup tooltip in tooltipCanvasGroup)
            {
                tooltip.alpha = 0f;
                tooltip.blocksRaycasts = false; // Важно!
            }
        }
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isShowing = true;
        FadeTo(targetAlpha);
        audioManager.PlaySFX(audioManager.writing);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isShowing = false;
        FadeTo(0f);
    }

    private void FadeTo(float targetAlpha)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeAnimation(targetAlpha));
    }

    private IEnumerator FadeAnimation(float targetAlpha)
    {
        foreach(CanvasGroup tooltip in tooltipCanvasGroup)
        {
            float startAlpha = tooltip.alpha;
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                if ((targetAlpha > 0 && !isShowing) || (targetAlpha == 0 && isShowing))
                {
                    yield break;
                }

                elapsedTime += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(elapsedTime / fadeDuration);

                tooltip.alpha = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);

                yield return null;
            }
            tooltip.alpha = targetAlpha;

        }

    }

}