using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WinScreen : MonoBehaviour
{
    [SerializeField] Button _nextLvlButton;
    [SerializeField] GameObject _woodFrame;

    private int _existActs = 2;

    private void Awake()
    {

        if (LevelMenuButtonManager.currLevel % 10 == 0)
        {
            if (ActMenuButtonsManager.currAct < _existActs)
            {
                _nextLvlButton.interactable = false;
                _woodFrame.SetActive(true);
            }
            if (ActMenuButtonsManager.currAct == LevelSelectorManager.UnlockedActs)
            {
                LevelSelectorManager.UnlockedActs++;
                YG2.saves.unlockedActs = LevelSelectorManager.UnlockedActs;
                YG2.SaveProgress();
            }
        }
        else
        {
            _nextLvlButton.interactable = true;
            _woodFrame.SetActive(false);
        }
    }
}
