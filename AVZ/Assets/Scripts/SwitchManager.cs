using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchManager : MonoBehaviour
{
    public static SwitchManager instance;

    [SerializeField] int MainMenuIndex = 0;
    [SerializeField] int LevelSelectorIndex = 1;
    [SerializeField] int InfLevelIndex = 10;

    public float timeForWait = 2;

    bool isSwitching;

    public void AdvPlayButton()
    {
        SceneSwitch(LevelSelectorIndex);
    }

    public void InfPlayButton()
    {
        SceneSwitch(InfLevelIndex);
    }

    public void MainMenuButton()
    {
        SceneSwitch(MainMenuIndex);
    }
    public void SceneSwitch(int index)
    {
        if (isSwitching)
        {
            return;
        }
        isSwitching = true;

        StartCoroutine(SceneSwitchTimer(index));
    }

    IEnumerator SceneSwitchTimer(int index)
    {
        yield return new WaitForSeconds(timeForWait);
        isSwitching = false;
        SceneManager.LoadScene(index);
    }
}
