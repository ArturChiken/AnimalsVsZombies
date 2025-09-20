using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using YG;

public class LevelSelectorManager : MonoBehaviour
{
    //синглтон паттерн постройки файла, иниц. инстанса этого класса
    public static LevelSelectorManager _;

    bool LevelIsActive = false;
    bool DiffIsActive = false;

    int whatActRN = 1;
    public enum ActContainerButtons { back, fstAct, sndAct, trdAct, right, left };
    public enum LevelContainerButtons { back, level };
    public enum DiffContainerButtons { back, easy, normal, hard };
    public enum OtherButtons { tg };

    public static int UnlockedLevels;
    public static int UnlockedActs;
    public LevelObject[] levelObjects;
    public ActObject[] actObjects;
    public Sprite goldenStarSprite;

    [SerializeField] CanvasGroup _fadeCanvasGroup, _ActContainerButtons, _LevelContainerButtons, _DiffContainerButtons;
    [SerializeField] GameObject _ActContainer, _LevelsContainer, _DiffContainer, _Act1Levels, _Act2Levels;
    [SerializeField] Animator _Act1Animation, _Act2Animation, _Act3Animation, _ActAnimation, _LevelAnimation, _DiffAnimation, _RightArrowAnimation, _LeftArrowAnimation;
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
        UnlockedLevels = YG2.saves.unlockedLevels;
        UnlockedActs = YG2.saves.unlockedActs;
        for (int k = 1; k < actObjects.Length + 1; k++)
        {
            if (UnlockedActs >= k)
            {
                actObjects[k].actButton.interactable = true;
            }
        }
        for (int i = 1; i < levelObjects.Length + 1; i++)
        {
            if (UnlockedLevels >= i)
            {
                levelObjects[i].levelButton.interactable = true;
                int stars = YG2.saves.stars[i];
                for (int j = 0; j < stars; j++)
                {
                    levelObjects[i].stars[j].sprite = goldenStarSprite;
                    levelObjects[i].stars[j].color = new UnityEngine.Color(255f, 255f, 255f);
                }
            }
        }
    }

    //актовые кнопки
    public void ActMenuButtonClicked(ActContainerButtons buttonClicked)
    {
        switch (buttonClicked)
        {
            case ActContainerButtons.back:
                if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
                StartCoroutine(TransitionScene(0));
                break;
            case ActContainerButtons.fstAct:
                StartCoroutine(PlaySwitchLevelActAnimation(LevelIsActive));
                _Act1Levels.SetActive(true);
                _Act2Levels.SetActive(false);
                break;
            case ActContainerButtons.sndAct:
                StartCoroutine(PlaySwitchLevelActAnimation(LevelIsActive));
                _Act2Levels.SetActive(true);
                _Act1Levels.SetActive(false);
                break;
            case ActContainerButtons.trdAct:
                break;
            case ActContainerButtons.right:
                if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
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
                if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
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
                if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
                StartCoroutine(PlaySwitchLevelActAnimation(LevelIsActive));
                break;
            case LevelContainerButtons.level:
                StartCoroutine(PlaySwitchLevelDiffAnimation(DiffIsActive));
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
                if (Random.Range(0f, 1f) <= .35f) YG2.InterstitialAdvShow();
                StartCoroutine(PlaySwitchLevelDiffAnimation(DiffIsActive));
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

    private IEnumerator PlaySwitchLevelActAnimation(bool show)
    {
        if (show)
        {
            _LevelContainerButtons.interactable = false;
            _LevelContainerButtons.blocksRaycasts = false;
            _LevelAnimation.Play("LevelUp");
            yield return new WaitForSeconds(_LevelAnimation.GetCurrentAnimatorStateInfo(0).length);
            _LevelsContainer.SetActive(false);
            _ActContainer.SetActive(true);
            _ActAnimation.Play("ActDown");
            _RightArrowAnimation.Play("RightToLeft");
            _LeftArrowAnimation.Play("LeftToRight");
            yield return new WaitForSeconds(_ActAnimation.GetCurrentAnimatorStateInfo(0).length);
            _ActContainerButtons.interactable = true;
            _ActContainerButtons.blocksRaycasts = true;
        }
        else
        {
            _ActContainerButtons.interactable = false;
            _ActContainerButtons.blocksRaycasts = false;
            _ActAnimation.Play("ActUp");
            _RightArrowAnimation.Play("RightToRight");
            _LeftArrowAnimation.Play("LeftToLeft");
            yield return new WaitForSeconds(_ActAnimation.GetCurrentAnimatorStateInfo(0).length);
            _ActContainer.SetActive(false);
            _LevelsContainer.SetActive(true);
            _LevelAnimation.Play("LevelDown");
            yield return new WaitForSeconds(_LevelAnimation.GetCurrentAnimatorStateInfo(0).length);
            _LevelContainerButtons.interactable = true;
            _LevelContainerButtons.blocksRaycasts = true;
;
        }
        LevelIsActive = !LevelIsActive;
    }

    public IEnumerator PlaySwitchLevelDiffAnimation(bool show)
    {
        if (show)
        {
            _DiffContainerButtons.interactable = false;
            _DiffContainerButtons.blocksRaycasts = false;
            _DiffAnimation.Play("DiffUp");
            yield return new WaitForSeconds(_DiffAnimation.GetCurrentAnimatorStateInfo(0).length);
            _DiffContainer.SetActive(false);
            _LevelsContainer.SetActive(true);
            _LevelAnimation.Play("LevelDown");
            yield return new WaitForSeconds(_ActAnimation.GetCurrentAnimatorStateInfo(0).length);
            _LevelContainerButtons.interactable = true;
            _LevelContainerButtons.blocksRaycasts = true;
        }
        else
        {
            _LevelContainerButtons.interactable = false;
            _LevelContainerButtons.blocksRaycasts = false;
            _LevelAnimation.Play("LevelUp");
            yield return new WaitForSeconds(_LevelAnimation.GetCurrentAnimatorStateInfo(0).length);
            _LevelsContainer.SetActive(false);
            _DiffContainer.SetActive(true);
            _DiffAnimation.Play("DiffDown");
            yield return new WaitForSeconds(_LevelAnimation.GetCurrentAnimatorStateInfo(0).length);
            _DiffContainerButtons.interactable = true;
            _DiffContainerButtons.blocksRaycasts = true;
        }
        DiffIsActive = !DiffIsActive;
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

