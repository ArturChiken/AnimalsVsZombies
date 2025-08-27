using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using JetBrains.Annotations;

public class LevelSelectorManager : MonoBehaviour
{
    //синглтон паттерн постройки файла, иниц. инстанса этого класса
    public static LevelSelectorManager _;

    int whatActRN = 1;
    public enum ActContainerButtons { back, fstAct, sndAct, trdAct, right, left };
    public enum LevelContainerButtons { back, level };
    public enum DiffContainerButtons { back, easy, normal, hard };
    public enum OtherButtons { tg };

    public static int UnlockedLevels;
    public LevelObject[] levelObjects;

    [SerializeField] CanvasGroup _fadeCanvasGroup, _ActContainerButtons;
    [SerializeField] GameObject _ActContainer, _LevelsContainer, _DiffContainer;
    [SerializeField] Animator _Act1Animation, _Act2Animation, _Act3Animation;
    [SerializeField] int _sceneToLoadAfterPressedBack, _sceneToLoadAfterPressedLvl;
    [SerializeField] float _fadeDuration = 1f;

    public void Awake()
    {
        if (_ == null)
            _ = this;
        else
            Debug.LogError("There are more than 1 LevelSelectorManager in the scene");
    }

    private void Start()
    {
        StartCoroutine(Fade(1f, 0f));
        _ActContainer.SetActive(true);
        _LevelsContainer.SetActive(false);
        _DiffContainer.SetActive(false);
        UnlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);
        for (int i = 0; i < levelObjects.Length; i++)
        {
            if (UnlockedLevels >= i)
            {
                levelObjects[i].levelButton.interactable = true;
            }
        }
    }

    //актовые кнопки
    public void ActMenuButtonClicked(ActContainerButtons buttonClicked)
    {

        switch (buttonClicked)
        {
            case ActContainerButtons.back:
                StartCoroutine(TransitionScene(0));
                break;
            case ActContainerButtons.fstAct:
                _ActContainer.SetActive(false);
                _LevelsContainer.SetActive(true);
                break;
            case ActContainerButtons.sndAct:
                break;
            case ActContainerButtons.trdAct:
                break;
            case ActContainerButtons.right:
                switch (whatActRN)
                {
                    case 1:
                        _Act1Animation.Play("Act1LeftSlide");
                        _Act2Animation.Play("Act2LeftSlide2");
                        StartCoroutine(PlayActAnimation(_Act2Animation));
                        whatActRN = 2;
                        break;
                    case 2:
                        _Act2Animation.Play("Act2LeftSlide");
                        _Act3Animation.Play("Act3LeftSlide2");
                        StartCoroutine(PlayActAnimation(_Act3Animation));
                        whatActRN = 3;
                        break;
                    case 3:
                        _Act3Animation.Play("Act3LeftSlide");
                        _Act1Animation.Play("Act1LeftSlide2");
                        StartCoroutine(PlayActAnimation(_Act1Animation));
                        whatActRN = 1;
                        break;
                }
                break;
            case ActContainerButtons.left:
                switch (whatActRN)
                {
                    case 1:
                        _Act1Animation.Play("Act1RightSlide");
                        _Act3Animation.Play("Act3RightSlide2");
                        StartCoroutine(PlayActAnimation(_Act3Animation));
                        whatActRN = 3;
                        break;
                    case 2:
                        _Act2Animation.Play("Act2RightSlide");
                        _Act1Animation.Play("Act1RightSlide2");
                        StartCoroutine(PlayActAnimation(_Act1Animation));
                        whatActRN = 1;
                        break;
                    case 3:
                        _Act3Animation.Play("Act3RightSlide");
                        _Act2Animation.Play("Act2RightSlide2");
                        StartCoroutine(PlayActAnimation(_Act2Animation));
                        whatActRN = 2;
                        break;
                }
                break;
        }

    }
    //лвл кнопки
    public void LevelsMenuButtonClicked(LevelContainerButtons buttonClicked)
    {
        switch (buttonClicked)
        {
            case LevelContainerButtons.back:
                _LevelsContainer.SetActive(false);
                _ActContainer.SetActive(true);
                break;
            case LevelContainerButtons.level:
                _LevelsContainer.SetActive(false);
                _DiffContainer.SetActive(true);
                break;
        }
    }

    // дифф кнопки
    public void DiffMenuButtonClicked(DiffContainerButtons buttonClicked)
    {
        switch (buttonClicked)
        {
            case DiffContainerButtons.easy:
                StartCoroutine(TransitionScene(1));
                break;
            case DiffContainerButtons.normal:
                StartCoroutine(TransitionScene(1));
                break;
            case DiffContainerButtons.hard:
                StartCoroutine(TransitionScene(1));
                break;
            case DiffContainerButtons.back:
                _DiffContainer.SetActive(false);
                _LevelsContainer.SetActive(true);
                break;
        }
    }

    //кнопка тг
    public void OtherButtonsClicked(OtherButtons buttonClicked)
    {
        string websiteLink = "";
        switch (buttonClicked)
        {
            case OtherButtons.tg:
                websiteLink = "https://t.me/afterpartygames";
                if (websiteLink != "")
                {
                    Application.OpenURL(websiteLink);
                }
                break;
        }
    }

    //анимация перехода актов влево/вправо
    private IEnumerator PlayActAnimation(Animator whatAnimator)
    {
        _ActContainerButtons.interactable = false;
        _ActContainerButtons.blocksRaycasts = false;
        yield return new WaitForSeconds(whatAnimator.GetCurrentAnimatorStateInfo(0).length);
        _ActContainerButtons.interactable = true;
        _ActContainerButtons.blocksRaycasts = true;
    }

    // затемнение+переход на сцену
    private IEnumerator TransitionScene(int numberOfButton)
    {
        yield return StartCoroutine(Fade(0f, 1f));

        switch (numberOfButton)
        {
            case 0:
                SceneManager.LoadScene(_sceneToLoadAfterPressedBack);
                break;
            case 1:
                SceneManager.LoadScene(_sceneToLoadAfterPressedLvl);
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

